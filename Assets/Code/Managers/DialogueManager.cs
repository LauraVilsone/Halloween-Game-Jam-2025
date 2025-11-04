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
    public bool SkipTyping { get; set; }
    private Coroutine m_typingCoroutine;

    private void Awake()
    {
        HUD = FindFirstObjectByType<HUDManager>();
        m_portrait = FindFirstObjectByType<Portrait>();
    }

    private bool m_skipRecording;
    public void Begin(Dialogue dialogue, bool skipRecording)
    {
        HUD.Box.Show();
        LinesFinished = false;

        m_skipRecording = skipRecording;
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
        if (!m_skipRecording)
            HUD.Log.Add(m_currentDialogue.CurrentLine);

        IsTyping = false;
        if (m_currentDialogue.FinishedReading)
            LinesFinished = true;
        else
            m_currentDialogue.Next();
    }

    private void CompleteTyping()
    {
        HUD.Box.Fill(m_currentDialogue.CurrentLine.Dialogue);
        //IsTyping = false;
        SkipTyping = true;
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

        SFXManager.PlayNextDialogueSFX();
        HUD.Box.Name(m_currentDialogue.CurrentLine.Actor);
        m_typedText = m_currentDialogue.CurrentLine.Dialogue;

        m_actor = m_currentDialogue.CurrentLine.Actor;
        if (m_actor == Actors.Elara)
        {
            m_emotion = m_currentDialogue.CurrentLine.Emotion;

            Actor actor = null;
            foreach (var act in m_currentDialogue.Actors)
            {
                if (act.Name == m_actor.ToString())
                {
                    actor = act;
                    break;
                }
            }

            //var actor = m_currentDialogue.Actors[(int)m_actor];
            var emotion = actor.GetEmotion(m_currentDialogue.CurrentLine.Emotion);
            m_portrait.ChangePortrait(emotion);
        }

        m_typingCoroutine = StartCoroutine(Typing());
    }

    private string m_typedText;
    private int m_stringIndex = -1;
    private StringBuilder m_stringBuilder;
    public IEnumerator Typing()
    {
        IsTyping = true;
        SkipTyping = false;

        string line = "";
        int stringLength = m_typedText.Length;
        m_stringIndex = -1;

        float typingDelay = m_typingDelaySeconds;

        m_stringBuilder = new StringBuilder(line, 200);

        while (IsTyping)
        {
            HUD.Box.Fill(m_stringBuilder.ToString());

            if (m_stringIndex < stringLength - 1)
            {
                m_stringIndex++;
                var character = m_typedText[m_stringIndex];
                if (character == '<')
                {
                    bool skip = true;
                    while (skip)
                    {
                        m_stringBuilder.Append(m_typedText[m_stringIndex]);
                        if (character == '>')
                            skip = false;
                        else
                        {
                            m_stringIndex++;
                            character = m_typedText[m_stringIndex];
                        }
                    }
                }
                else
                {
                    m_stringBuilder.Append(m_typedText[m_stringIndex]);
                }
                if (!SkipTyping)
                {
                    //SFXManager.PlayTypingSFX();
                    yield return new WaitForSeconds(typingDelay);
                }
            }
            else
            {
                IsTyping = false;
            }

            if (!SkipTyping)
                yield return null;
        }

        OnFinishedTyping();
    }

    public void RewriteWithoutTags()
    {
        string text = m_stringBuilder.ToString();
        int stringLength = text.Length;
        int stringIndex = -1;
        StringBuilder stringBuilder = new StringBuilder("", 200);

        while (stringIndex < stringLength - 1)
        {
            stringIndex++;
            var character = text[stringIndex];
            if (character == '<')
            {
                bool skip = true;
                while (skip)
                {
                    if (character == '>' || stringIndex >= stringLength - 1)
                        skip = false;
                    else
                    {
                        stringIndex++;
                        character = text[stringIndex];
                    }
                }
            }
            else
            {
                stringBuilder.Append(text[stringIndex]);
            }
        }

        HUD.Box.Fill(stringBuilder.ToString());
        m_stringBuilder.Clear();
        m_stringBuilder.Append(stringBuilder.ToString());
    }
}
