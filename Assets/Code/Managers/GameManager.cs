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


    public PlayerManager Player => m_player;
    public RoomManager Rooms => m_roomManager;
    public HUDManager HUD => m_HUDManager;

    private void Awake()
    {
        m_player = new PlayerManager();
        m_roomManager = GetComponent<RoomManager>();
        foreach (var state in m_states)
        {
            state.Initialize(this, m_player, m_roomManager);
        }
    }

    private void Start()
    {
        m_gameState = m_states[0];
        m_gameState.Enter();
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

    public void ChangeState(int i)
    {
        if (m_gameState != null)
            m_gameState.Exit();

        m_gameState = m_states[i];

        m_gameState.Enter();
    }
}
