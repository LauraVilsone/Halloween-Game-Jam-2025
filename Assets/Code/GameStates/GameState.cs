using UnityEngine;

public abstract class GameState : ScriptableObject
{
    protected GameManager m_game;

    protected PlayerManager m_player;
    protected RoomManager m_rooms;
    protected InventoryManager m_inventory;
    protected InteractableManager m_interactable;

    public virtual void Initialize(GameManager game, PlayerManager player, RoomManager rooms)
    {
        m_game = game;

        m_player = player;
        m_rooms = rooms;
        m_inventory = game.Inventory;
        
        m_interactable = game.GetComponent<InteractableManager>();
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}