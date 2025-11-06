using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

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

    private EventSystem m_system;
    private UIButton[] m_buttons;

    public bool ViewingLog => Log.Active;
    public bool ViewingButtons => m_hoveredButtons > 0;

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
        m_system = GetComponentInChildren<EventSystem>();
        m_buttons = GetComponentsInChildren<UIButton>();
    }

    private void Start()
    {
        ListenToButtons();
    }

    private void ListenToButtons()
    {
        foreach (var button in m_buttons)
        {
            button.OnHoverEnter += OnButtonEnter;
            button.OnHoverExit += OnButtonExit;
            //interactable.OnRelease += OnRelease;
        }
    }

    private int m_hoveredButtons;
    private void OnButtonEnter()
    {
        m_hoveredButtons++;
    }
    private void OnButtonExit()
    {
        m_hoveredButtons--;
        if (m_hoveredButtons < 0)
            m_hoveredButtons = 0;
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
    public void ToggleLog()
    {
        if (Log.Active)
            Log.Hide();
        else
            Log.Show();
    }
}
