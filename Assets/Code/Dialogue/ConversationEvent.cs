using UnityEngine;

public abstract class ConversationEvent : ScriptableObject
{
    public bool EventFinished { get; set; }

    public virtual void Execute(params object[] objects)
    {
        for (int i = 0; i < objects.Length; i++)
        {

        }
    }
}
