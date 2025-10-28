using UnityEngine;


[System.Serializable]
public class Decision
{
    public string m_name;
    public Dialogue m_dialogue;
    public int m_bond;
}

[System.Serializable]
public class Interaction
{
    public Interactable m_interactable;
    public string m_comment;
}

[CreateAssetMenu(fileName = "Keyword", menuName = "Scriptables/Keyword", order = 4)]
public class KeywordData : ScriptableObject
{
    public string m_name = "";
    [Space]
    public Decision[] m_decision;
    public Interaction[] m_interactions;
}
