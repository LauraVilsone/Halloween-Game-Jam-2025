using UnityEngine;

[CreateAssetMenu(fileName = "Conversation", menuName = "Scriptables/States/Conversation", order = 0)]
public class ConversationState : ExplorationState
{
    DialogueManager m_dialogue;
    ConversationManager m_conversation;

    public override void Initialize(GameManager game, PlayerManager player, RoomManager rooms)
    {
        base.Initialize(game, player, rooms);

        m_dialogue = game.GetComponent<DialogueManager>();
        m_conversation = game.GetComponent<ConversationManager>();
    }

    public override void Enter()
    {
        //m_dialogue.Begin();
        m_conversation.Begin();
    }

    public override void Tick()
    {
        if (m_inventory.Active)
        {
            if (m_player.MouseRightDown)
            {
                m_inventory.Toggle(true);
            }
            return;
        }
        if (m_player.MouseLeftDown)
        {
            if (m_conversation.ConversationFinished)
            {
                m_game.ChangeState(1);
            }
            else
                m_conversation.Proceed();
        }
        else if (m_player.MouseRightDown)
        {
            m_inventory.Toggle(true);
        }

        /*if (Input.GetKeyDown(KeyCode.LeftControl))
        {
            m_game.ChangeState(1);
        }*/
    }

    public override void Exit()
    {
        //m_dialogue.End();
        m_conversation.End();
    }
}
