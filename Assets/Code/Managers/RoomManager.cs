using UnityEngine;

public class RoomManager : MonoBehaviour
{
    [SerializeField] private Room m_currentRoom;

    public Room CurrentRoom => m_currentRoom;

    public void Tick()
    {
        
    }
}
