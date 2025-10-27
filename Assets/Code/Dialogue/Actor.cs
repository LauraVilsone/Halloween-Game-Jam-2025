using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Scriptables/Actor", order = 0)]
public class Actor : ScriptableObject
{
    public SpriteRenderer Portrait;
    public SpriteRenderer[] Emotions;
    public string Name;
}