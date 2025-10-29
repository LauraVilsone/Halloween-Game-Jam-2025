using UnityEngine;

[CreateAssetMenu(fileName = "BeginNext", menuName = "Scriptables/Begin Next", order = 4)]
public class BeginNextConversation : ConversationEvent
{
    public Conversation NextConversation;
}
