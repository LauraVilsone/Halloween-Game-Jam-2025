using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptables/Conversation", order = 2)]
public class Conversation : ScriptableObject
{
    public ConversationEvent[] Events;
}
