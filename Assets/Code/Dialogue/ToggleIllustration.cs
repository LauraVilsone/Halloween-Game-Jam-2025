using UnityEngine;

[CreateAssetMenu(fileName = "Toggle Illustration", menuName = "Scriptables/Event/Toggle Illustration", order = 3)]
public class ToggleIllustration : ConversationEvent
{
    public bool Show;
    public bool Fade;
    [Space]
    public bool Quick;
    public bool Intro;
    [Space] 
    public Sprite Illustration;
}
