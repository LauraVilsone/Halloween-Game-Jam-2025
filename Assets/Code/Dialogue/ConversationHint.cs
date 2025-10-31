using UnityEngine;

[CreateAssetMenu(fileName = "Conversation Hint", menuName = "Scriptables/Event/Conversation Hint", order = 3)]
public class ConversationHint : ConversationEvent
{
    public HintMessage Message;
}
