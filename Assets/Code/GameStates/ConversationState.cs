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
        m_conversation.Begin();
        m_interactable.ToggleInteractableVisibility(false);
    }

    public override void Tick()
    {
        if (m_inventory.Active)
        {
            if (m_player.MouseRightDown)
            {
                m_inventory.Toggle();
            }
            return;
        }
        else if (m_game.HUD.ViewingLog)
        {
            if (m_player.ScrollWheelUp)
                m_game.HUD.HideLog();
            return;
        }

        if (m_player.MouseLeftDown || m_player.SpacebarDown)
        {
            /*if (m_conversation.ConversationFinished)
            {
                m_game.ChangeState(1);
            }
            else*/
            if (m_conversation.Proceed())
                m_game.ChangeState(1);
        }
        else if (m_player.MouseRightDown && (m_conversation.OnFinalEvent && !m_conversation.SkipChoices && m_dialogue.ConversationFinished))
        {
            m_inventory.Toggle();
        }
        else if (m_player.ScrollWheelDown || m_player.ScrollWheelDelta != Vector2.zero)
        {
            m_game.HUD.ShowLog();
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
        SFXManager.PlayCloseConversationSFX();
    }
}
