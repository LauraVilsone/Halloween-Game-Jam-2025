using UnityEngine;

[CreateAssetMenu(fileName = "Toggle Illustration", menuName = "Scriptables/Event/Toggle Illustration", order = 3)]
public class ToggleIllustration : ConversationEvent
{
    public bool Show;
    public bool Quick;
    public bool Fade;
    public Sprite Illustration;
}
