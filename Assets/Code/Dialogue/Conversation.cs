using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptables/Conversation", order = 2)]
public class Conversation : ScriptableObject
{
    public bool SkipChoices;
    [Space]
    public ConversationEvent[] Events;
    [Space]
    public HintMessage HintAtEnd;
}
