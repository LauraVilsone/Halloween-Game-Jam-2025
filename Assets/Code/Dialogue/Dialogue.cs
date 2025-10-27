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


    private int m_lineIndex = 0;
    public Line CurrentLine => Line[m_lineIndex];
    public bool FinishedReading { get; set; }

    public Line BeginRead()
    {
        m_lineIndex = 0;
        FinishedReading = false;
        return Line[m_lineIndex];
    }

    public Line Next()
    {
        m_lineIndex++;
        if (m_lineIndex >= Line.Length - 1)
        {
            m_lineIndex = Line.Length - 1;
            FinishedReading = true;
        }
        return Line[m_lineIndex];
    }
}