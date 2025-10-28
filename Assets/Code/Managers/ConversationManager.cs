using System;
using UnityEngine;

public class ConversationManager : MonoBehaviour
{
    [SerializeField] private Conversation m_currentConversation;

    private int m_eventIndex;

    public bool ConversationFinished { get; set; }

    private ConversationEvent CurrentEvent => m_currentConversation.Events[m_eventIndex];

    private DialogueManager m_dialogueManager;

    public Action OnEventFinished;

    private void Awake()
    {
        m_dialogueManager = GetComponent<DialogueManager>();
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
                    m_dialogueManager.End();
                    EventFinished();
                }
                else
                    m_dialogueManager.Proceed();
            }
        }
    }

    public void EventFinished()
    {
        CurrentEvent.EventFinished = true;
        if (m_eventIndex >= m_currentConversation.Events.Length - 1)
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
