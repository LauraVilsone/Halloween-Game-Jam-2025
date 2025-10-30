using UnityEngine;

[CreateAssetMenu(fileName = "Actor", menuName = "Scriptables/Actor", order = 0)]
public class Actor : ScriptableObject
{
    [System.Serializable]
    public struct EmotionSprite
    {
        public Emotions Emotion;
        public Sprite Sprite;
    }

    public Sprite Portrait;
    public EmotionSprite[] EmotionSprites;
    public string Name;

    public Sprite GetEmotion(Emotions emotion)
    {
        foreach (var emotionsprite in EmotionSprites)
        {
            if ( emotionsprite.Emotion == emotion)
            {
                return emotionsprite.Sprite;
            }
        }

        return null;
    }
}