using UnityEngine;

[CreateAssetMenu(fileName = "Exploration", menuName = "Scriptables/States/Exploration", order = 1)]
public class ExplorationState : GameState
{

    public override void Initialize(PlayerManager player, RoomManager rooms)
    {
        base.Initialize(player, rooms);

        
    }

    public override void Enter()
    {

    }

    public override void Tick()
    {
        if (m_player.MouseLeftDown)
        {
            m_rooms.CurrentRoom.MouseMove(m_player.MouseDelta);
        }
    }

    public override void Exit()
    {

    }
}
