using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptables/States/Conversation", order = 0)]
public class ConversationState : ExplorationState
{
    ConversationManager m_conversation;

    public override void Initialize(GameManager game, PlayerManager player, RoomManager rooms)
    {
        base.Initialize(game, player, rooms);

        m_conversation = game.GetComponent<ConversationManager>();
    }

    public override void Enter()
    {
        m_conversation.Begin();
    }

    public override void Tick()
    {
        if (m_player.MouseLeftDown)
        {
            if (m_conversation.ConversationFinished)
            {
                m_game.ChangeState(1);
            }
            else
                m_conversation.Proceed();
        }
    }

    public override void Exit()
    {
        m_conversation.End();
    }
}
