using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Room m_currentRoom;

    public Room CurrentRoom => m_currentRoom;


    public void OnStateChange(bool inConversation)
    {
        if (inConversation)
            CurrentRoom.OnTalk();
        else
            CurrentRoom.OnExit();
    }
}
