using UnityEngine;

[System.Serializable]
public class Line
{
    public string Dialogue;
    public int Actor;
    public Emotions Emotion;
}

public enum Emotions { Neutral, Happy, Sad, Angry, Pensive, Depresso }

[CreateAssetMenu(fileName = "Dialogue", menuName = "Scriptables/Dialogue", order = 1)]
public class Dialogue : ConversationEvent
{
    public Actor[] Actors;

    public Line[] Line;
}