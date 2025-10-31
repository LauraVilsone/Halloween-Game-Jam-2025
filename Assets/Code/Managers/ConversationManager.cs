using System;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private Conversation m_currentConversation;

    private int m_eventIndex;

    public bool ConversationFinished { get; set; }
    public bool OnFinalEvent => m_eventIndex >= m_currentConversation.Events.Length - 1;
    public bool SkipChoices => m_currentConversation.SkipChoices;

    private ConversationEvent CurrentEvent => m_currentConversation.Events[m_eventIndex];

    private HUDManager m_hudManager;
    private DialogueManager m_dialogueManager;
    private BondManager m_bondManager;
    private IllustrationManager m_illustrationManager;

    public Action OnEventFinished;

    private void Awake()
    {
        m_dialogueManager = GetComponent<DialogueManager>();
        
        m_bondManager = GetComponent<BondManager>();

        m_hudManager = FindFirstObjectByType<HUDManager>();
        OnEventFinished += m_hudManager.ShowChoiceBox;

        m_illustrationManager = FindFirstObjectByType<IllustrationManager>();
        m_illustrationManager.OnAnimationFinished += OnIllustrationChangeFinished;
    }

    private void OnIllustrationChangeFinished()
    {
        EventFinished();
    }

    public void ChangeConversation(Conversation conversation)
    {
        m_currentConversation = conversation;
        m_eventIndex = 0;
    }

    public void Proceed()
    {
        if (!CurrentEvent.EventFinished)
        {
            if (CurrentEvent is Dialogue dialogue)
            {
                if (m_dialogueManager.ConversationFinished)
                {
                    if (!OnFinalEvent)
                    {
                        m_dialogueManager.End();
                        EventFinished();
                    }
                    else
                    {
                        if (m_currentConversation.SkipChoices)
                        {
                            m_dialogueManager.End();
                            EventFinished();
                        }
                        else
                        {
                            OnEventFinished?.Invoke();
                        }
                        if (m_currentConversation.HintAtEnd != null)
                            m_hudManager.Hint.SendMessage(m_currentConversation.HintAtEnd);
                    }
                }
                else
                    m_dialogueManager.Proceed();
            }
        }
    }

    public void ManualConversationFinish()
    {
        if (m_dialogueManager.ConversationFinished)
        {
            m_dialogueManager.End();
            EventFinished();

            m_hudManager.HideChoiceBox();
        }
    }

    public void EventFinished()
    {
        CurrentEvent.EventFinished = true;
        if (OnFinalEvent)
        {
            ConversationFinished = true;
            return;
        }
        else
        {
            m_eventIndex++;
            Execute();
        }
    }

    public void Execute()
    {
        if (CurrentEvent is Dialogue dialogue)
        {
            m_dialogueManager.Begin(dialogue);
            //CurrentEvent.Execute(this, CurrentEvent);
        }
        else if (CurrentEvent is BondCheck bondCheck)
        {
            var convo = bondCheck.RollConversation(m_bondManager.Level);
            ChangeConversation(convo);
            Begin();
            return;
        }
        else if (CurrentEvent is BeginNextConversation beginNext)
        {
            ChangeConversation(beginNext.NextConversation);
            Begin();
            return;
        }
        else if (CurrentEvent is ToggleIllustration illustration)
        {
            if (illustration.Fade)
                m_illustrationManager.Fade();
            else if (illustration.Show)
                m_illustrationManager.ShowIllustration(illustration.Illustration, illustration.Quick);
            else
                m_illustrationManager.HideIllustration();
        }

        CurrentEvent.EventFinished = false;
    }

    public void Begin()
    {
        ConversationFinished = false;
        m_eventIndex = 0;
        Execute();
    }

    public void End()
    {

    }
}
