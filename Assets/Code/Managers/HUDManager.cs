using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextBox Box;
    public MindMap MindMap;
    public ChoiceBox ChoiceBox;
    public KeywordFlash Flash;
    public Hint Hint;
    [Space]
    [SerializeField] private HintMessage HintInventoryTutorial;

    private void Awake()
    {
        if (Box == null)
        {
            Box = GetComponentInChildren<TextBox>();
        }

        if (MindMap == null)
        {
            MindMap = GetComponentInChildren<MindMap>();
        }

        if (ChoiceBox == null)
        {
            ChoiceBox = GetComponentInChildren<ChoiceBox>();
        }

        if (Flash == null)
        {
            Flash = GetComponentInChildren<KeywordFlash>();
        }

        if (Hint == null)
        {
            Hint = GetComponentInChildren<Hint>();
        }

        m_firstTimeCollecting = true;
    }


    private enum HUDState { TextBox, MindMap, ChoiceBox }
    private HUDState m_state;

    public void ShowChoiceBox()
    {
        if (!ChoiceBox.On)
        {
            ChoiceBox.Show();
            ChoiceBox.SetStandby();
        }
    }

    public void HideChoiceBox()
    {
        ChoiceBox.Empty();
    }

    private bool m_firstTimeCollecting = true;
    public void OnKeywordGain(Vector3 position, string keyword)
    {
        if (m_firstTimeCollecting)
        {
            m_firstTimeCollecting = false;
            Hint.SendMessage(HintInventoryTutorial);
        }
        Flash.Flash(position, keyword);
    }
}
