using UnityEngine;

[CreateAssetMenu(fileName = "Hint Message", menuName = "Scriptables/Hint")]
public class HintMessage : ScriptableObject
{
    public string m_message;
    public Sprite m_icon;
    public bool m_tutorial;
}
