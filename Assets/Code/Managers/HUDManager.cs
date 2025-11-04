using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextBox Box;
    public MindMap MindMap;
    public ChoiceBox ChoiceBox;
    public KeywordFlash Flash;
    public Hint Hint;
    public Log Log;
    [Space]
    [SerializeField] private HintMessage HintInventoryTutorial;

    public bool ViewingLog => Log.Active;

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

        if (Log == null)
        {
            Log = GetComponentInChildren<Log>();
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

    public void ShowLog() => Log.Show();

    public void HideLog() => Log.Hide();
}
