using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    private PlayerManager m_player;
    private RoomManager m_roomManager;
    private InventoryManager m_inventory;
    private ConversationManager m_conversationManager;
    private BondManager m_bondManager;
    private HUDManager m_HUDManager;

    private GameState m_gameState;
    [Space]
    [SerializeField] private int m_startState = 0;
    [SerializeField] private GameState[] m_states;


    public PlayerManager Player => m_player;
    public RoomManager Rooms => m_roomManager;
    public InventoryManager Inventory => m_inventory;
    public ConversationManager Conversation => m_conversationManager;
    public BondManager Bond => m_bondManager;
    public HUDManager HUD => m_HUDManager;

    private void Awake()
    {
        m_player = new PlayerManager();
        m_roomManager = GetComponent<RoomManager>();
        m_inventory = GetComponent<InventoryManager>();
        m_conversationManager = GetComponent<ConversationManager>();
        m_bondManager = GetComponent<BondManager>();

        m_HUDManager = FindFirstObjectByType<HUDManager>();

        foreach (var state in m_states)
        {
            state.Initialize(this, m_player, m_roomManager);
        }
    }

    private void Start()
    {
        m_gameState = m_states[m_startState];
        m_gameState.Enter();
        ListenToRoom();
    }

    private void ListenToRoom()
    {
        foreach (var interactable in m_roomManager.CurrentRoom.GetComponentsInChildren<Interactable>())
        {
            interactable.OnHoverStart += OnHoverStart;
            interactable.OnHoverEnd += OnHoverEnd;
            interactable.OnClick += OnClick;
            //interactable.OnRelease += OnRelease;
        }
        Inventory.Map.OnKeywordDropped += OnKeywordDropped;
        Inventory.Choice.OnDecisionMade += OnDecisionMade;
    }

    private void OnDecisionMade(Decision data)
    {
        if (m_gameState is ConversationState)
        {
            if (data.m_conversation == null)
            {
                Debug.LogWarning("No specified dialogue for choice " + data);
                return;
            }

            Bond.Level += data.m_bond;
            
            m_conversationManager.ChangeConversation(data.m_conversation);
            ChangeState(0);
        }
    }

    private void OnKeywordDropped(KeywordData data)
    {
        if (m_gameState is ConversationState)
        {
            if (m_HUDManager.ChoiceBox.HoveredOver)
            {
                m_HUDManager.ChoiceBox.StartDecision(data);
            }
            //else
            //    m_HUDManager.ChoiceBox.Empty();
        }
        else
        {
            if (m_inventory.SelectedKeyword != null && InteractableData != null)
            {
                var conversation = m_inventory.SelectedKeyword.GetConversationByInteractable(InteractableData);
                if (conversation == null)
                {
                    Debug.LogWarning("No specified interaction between " + InteractableData.name + " and " + m_inventory.SelectedKeyword.name + "!");
                    return;
                }
                m_conversationManager.ChangeConversation(conversation);
                ChangeState(0);
            }
        }
    }

    public InteractableData InteractableData { get; set; }
    private void OnHoverStart(InteractableData data)
    {
        InteractableData = data;
        /*if (m_gameState is ConversationState) return;
        if (m_inventory.SelectedKeyword != null)
        {
            var conversation = m_inventory.SelectedKeyword.GetConversationByInteractable(data);
            if (conversation == null)
            {
                Debug.LogWarning("No specified interaction between " + data.name + " and " + m_inventory.SelectedKeyword.name + "!");
                return;
            }
            m_conversationManager.ChangeConversation(conversation);
            ChangeState(0);
        }*/
    }

    private void OnClick(InteractableData data)
    {
        if (m_gameState is ConversationState) return;

        if (m_inventory.SelectedKeyword == null) {
            m_conversationManager.ChangeConversation(data.Remark);
            ChangeState(0);
        }
    }

    private void OnHoverEnd()
    {
        InteractableData = null;
        //m_player.MouseLock = false;
    }
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
