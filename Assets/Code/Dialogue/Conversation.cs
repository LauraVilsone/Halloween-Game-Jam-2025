using UnityEngine;

[CreateAssetMenu(fileName = "Cutscene", menuName = "Scriptables/Cutscene", order = 2)]
public class Conversation : ScriptableObject
{
    public ConversationEvent[] Events;
}
