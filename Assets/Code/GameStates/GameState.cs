using UnityEngine;

public abstract class GameState : ScriptableObject
{
    protected GameManager m_game;

    protected PlayerManager m_player;
    protected RoomManager m_rooms;

    public virtual void Initialize(GameManager game, PlayerManager player, RoomManager rooms)
    {
        m_game = game;

        m_player = player;
        m_rooms = rooms;
    }

    public abstract void Enter();
    public abstract void Tick();
    public abstract void Exit();
}