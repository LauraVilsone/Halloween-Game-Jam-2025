using UnityEngine;

[System.Serializable]
public class Decision
{
    public string Line;
    public Dialogue m_dialogue;
}

[CreateAssetMenu(fileName = "Decision", menuName = "Scriptables/Decision", order = 3)]
public class CutsceneDecision : ConversationEvent
{
    public Decision[] m_decision;
}
