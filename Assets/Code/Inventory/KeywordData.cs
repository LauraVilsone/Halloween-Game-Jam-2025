using UnityEngine;


[System.Serializable]
public class Decision
{
    public string m_name;
    public Conversation m_conversation;
    public int m_bond;
}

[System.Serializable]
public class Interaction
{
    public InteractableData m_interactable;
    public Conversation m_conversation;
}

[CreateAssetMenu(fileName = "Keyword", menuName = "Scriptables/Keyword", order = 4)]
public class KeywordData : ScriptableObject
{
    public string m_name = "";
    [Space]
    public Decision[] m_decision;
    public Interaction[] m_interactions;

    public Conversation GetConversationByInteractable(InteractableData data)
    {
        foreach (var interaction in m_interactions)
        {
            if (interaction.m_interactable == data)
            {
                return interaction.m_conversation;
            }
        }
        return null;
    }
}
