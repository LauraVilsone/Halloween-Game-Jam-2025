using UnityEngine;

[System.Serializable]
public class Line
{
    public string Dialogue;
    public Actors Actor;
    public Emotions Emotion;
}

public enum Actors { Narrator, Elara, Liam }
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
        FinishedReading = Line.Length == 1 ? true : false;
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

    public override void Execute(params object[] objects)
    {

    }
}