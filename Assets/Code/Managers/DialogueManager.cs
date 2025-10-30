using System;
using System.Collections;
using System.Text;
using UnityEngine;

public class DialogueManager : MonoBehaviour
{
    [SerializeField] private Dialogue m_currentDialogue;
    [SerializeField] private float m_typingDelaySeconds = .125f;
    [Space]
    [SerializeField] private Portrait m_portrait;

    private HUDManager HUD;

    public bool ConversationFinished => LinesFinished && !IsTyping;
    public bool LinesFinished { get; set; }
    public bool IsTyping { get; set; }

    private Coroutine m_typingCoroutine;

    private void Awake()
    {
        HUD = FindFirstObjectByType<HUDManager>();
        m_portrait = FindFirstObjectByType<Portrait>();
    }

    public void Begin(Dialogue dialogue)
    {
        HUD.Box.Show();
        LinesFinished = false;

        m_currentDialogue = dialogue;

        m_currentDialogue.BeginRead();
        StartTyping();

    }

    public void End()
    {
        HUD.Box.Empty();
    }

    public void Proceed()
    {
        //if (LineFinished)
        //{
            if (!IsTyping)
                StartTyping();
            else
                CompleteTyping();
        //}
    }

    private void OnFinishedTyping()
    {
        IsTyping = false;
        if (m_currentDialogue.FinishedReading)
            LinesFinished = true;
        else
            m_currentDialogue.Next();
    }

    private void CompleteTyping()
    {
        HUD.Box.Fill(m_currentDialogue.CurrentLine.Dialogue);
        IsTyping = false;
    }

    private Emotions m_emotion;
    private Actors m_actor;
    private void StartTyping()
    {
        if (m_typingCoroutine != null)
        {
            StopCoroutine(m_typingCoroutine);
            m_typingCoroutine = null;
        }

        HUD.Box.Name(m_currentDialogue.CurrentLine.Actor);

        m_actor = m_currentDialogue.CurrentLine.Actor;
        if (m_actor == Actors.Elara)
        {
            m_emotion = m_currentDialogue.CurrentLine.Emotion;

            var actor = m_currentDialogue.Actors[(int)m_actor];
            var emotion = actor.GetEmotion(m_currentDialogue.CurrentLine.Emotion);
            m_portrait.ChangePortrait(emotion);
        }

        m_typingCoroutine = StartCoroutine(Typing());
    }

    public IEnumerator Typing()
    {
        IsTyping = true;

        string line = "";
        int stringLength = m_currentDialogue.CurrentLine.Dialogue.Length;
        int stringIndex = -1;

        float typingDelay = m_typingDelaySeconds;

        StringBuilder stringBuilder = new StringBuilder(line, 200);

        while (IsTyping)
        {
            HUD.Box.Fill(stringBuilder.ToString());

            if (stringIndex < stringLength - 1)
            {
                stringIndex++;
                var character = m_currentDialogue.CurrentLine.Dialogue[stringIndex];
                if (character == '<')
                {
                    bool skip = true;
                    while (skip)
                    {
                        stringBuilder.Append(m_currentDialogue.CurrentLine.Dialogue[stringIndex]);
                        if (character == '>')
                            skip = false;
                        else
                        {
                            stringIndex++;
                            character = m_currentDialogue.CurrentLine.Dialogue[stringIndex];
                        }
                    }
                }
                else
                {
                    stringBuilder.Append(m_currentDialogue.CurrentLine.Dialogue[stringIndex]);
                }
                yield return new WaitForSeconds(typingDelay);
            }
            else
            {
                IsTyping = false;
            }

            yield return null;
        }

        OnFinishedTyping();
    }
}
