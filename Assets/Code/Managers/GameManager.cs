using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerManager m_player;

    [SerializeField] private Room m_currentRoom;

    private void Awake()
    {
        m_player = new PlayerManager();

        //ListenToRoom();
    }

    private void ListenToRoom()
    {
        foreach (var interactable in m_currentRoom.GetComponentsInChildren<Interactable>())
        {
            interactable.OnHoverStart += OnHoverStart;
            interactable.OnHoverEnd += OnHoverEnd;
        }
    }

    private void OnHoverEnd() => m_player.MouseLock = false;
    private void OnHoverStart() => m_player.MouseLock = true;

    private void Update()
    {
        m_player.Tick();

        if (m_player.MouseLeftDown)
        {
            m_currentRoom.MouseMove(m_player.MouseDelta);
        }
        //Debug.Log(m_player.MouseDelta);
    }
}
