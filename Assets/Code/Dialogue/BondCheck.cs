using System;
using UnityEngine;

[CreateAssetMenu(fileName = "BondDecision", menuName = "Scriptables/BondDecision", order = 3)]
public class BondCheck : ConversationEvent
{
    public int BondReq = 0;

    public Conversation m_successConversation;
    public Conversation m_failConversation;

    public override void Execute(params object[] objects)
    {
        base.Execute(objects);

    }

    public Conversation RollConversation(int bond)
    {
        if (bond > BondReq)
        {
            return m_successConversation;
        }
        else
        {
            return m_failConversation;
        }
    }
}
