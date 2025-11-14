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
    [Space]
    [SerializeField] private HintMessage m_messageCare;
    [SerializeField] private HintMessage m_messageWorry;


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
        foreach (var interactable in m_roomManager.CurrentRoom.GetComponentsInChildren<Interactable>(true))
        {
            interactable.OnHoverStart += OnHoverStart;
            interactable.OnHoverEnd += OnHoverEnd;
            interactable.OnClick += OnClick;
            //interactable.OnRelease += OnRelease;
        }
        Inventory.Map.OnKeywordDropped += OnKeywordDropped;
        Inventory.Choice.OnDecisionMade += OnDecisionMade;
    }

    private void OnDecisionMade(Decision data, bool modifyBond)
    {
        if (m_gameState is ConversationState)
        {
            if (data.m_conversation == null)
            {
                Debug.LogWarning("No specified dialogue for choice " + data);
                return;
            }

            if (modifyBond)
            {
                var prevLevel = Bond.Level;
                Bond.Level += data.m_bond;

                if (Bond.Level < prevLevel)
                    HUD.Hint.SendMessage(m_messageWorry);
                else if (Bond.Level > prevLevel)
                    HUD.Hint.SendMessage(m_messageCare);
            }

            SFXManager.PlaySelectingChoiceSFX();
            m_conversationManager.ChangeConversation(data.m_conversation);
            ChangeState(0);
        }
    }

    private void OnKeywordDropped(Keyword keyword)
    {
        if (m_gameState is ConversationState)
        {
            if (m_HUDManager.ChoiceBox.HoveredOver)
            {
                m_HUDManager.ChoiceBox.StartDecision(keyword);
                keyword.SetRandomPosition();
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
            if (data.Focus)
                Rooms.OnStateChange(true);
            m_conversationManager.ChangeConversation(data.Remark);
            ChangeState(0);
        }
    }

    private float m_endDelay = 3;
    private float m_endTime;
    public void OnGameOver()
    {
        m_endTime = m_endDelay;
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

        if (m_endTime > 0)
        {
            m_endTime -= Time.deltaTime;
            if (m_endTime <= 0)
            {
                SceneDirector.LoadSceneAsync(0);
            }
        }
    }

    public void ChangeState(int i)
    {
        if (m_gameState != null)
            m_gameState.Exit();

        m_gameState = m_states[i];

        m_gameState.Enter();
    }
}
