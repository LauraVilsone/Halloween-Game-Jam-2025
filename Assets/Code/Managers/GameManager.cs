using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerManager m_player;
    private RoomManager m_roomManager;

    private HUDManager m_HUDManager;

    private GameState m_gameState;
    [Space]
    [SerializeField] private GameState[] m_states;


    private void Awake()
    {
        m_player = new PlayerManager();
        m_roomManager = GetComponent<RoomManager>();

        m_gameState = m_states[0];

        foreach (var state in m_states)
        {
            state.Initialize(m_player, m_roomManager);
        }

        //ListenToRoom();
    }

    private void ListenToRoom()
    {
        foreach (var interactable in m_roomManager.CurrentRoom.GetComponentsInChildren<Interactable>())
        {
            interactable.OnHoverStart += OnHoverStart;
            interactable.OnHoverEnd += OnHoverEnd;
        }
    }

    private void OnHoverEnd() => m_player.MouseLock = false;
    private void OnHoverStart() => m_player.MouseLock = true;

    private void Update()
    {
        m_gameState.Tick();

        m_player.Tick();
        m_roomManager.Tick();
        //Debug.Log(m_player.MouseDelta);
    }
}
