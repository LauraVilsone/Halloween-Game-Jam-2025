using System;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceBox : UIGroup, IPointerEnterHandler, IPointerExitHandler
{
    public GameObject List;
    public GameObject Prompt;

    private ChoiceBoxEntry[] m_choiceBoxEntries;

    public enum ChoiceState { Standby, Decide }
    private ChoiceState m_currentState;

    public bool HoveredOver;
    private Keyword m_draggedInKeyword;
    public Action<Decision> OnDecisionMade;


    public override void Start()
    {
        base.Start();
        EnterState(ChoiceState.Standby);

        m_choiceBoxEntries = List.GetComponentsInChildren<ChoiceBoxEntry>();

        foreach (var entry in m_choiceBoxEntries)
        {
            entry.OnChosen += OnEntryChosen;
        }
    }

    private void OnEntryChosen(int index)
    {
        OnDecisionMade?.Invoke(m_draggedInKeyword.Data.m_decision[index]);
        Empty();
        m_draggedInKeyword.OnDecisionSet(index);
    }

    public void EnterState(ChoiceState newState)
    {
        m_currentState = newState;

        switch (m_currentState)
        {
            case ChoiceState.Standby:
                List.SetActive(false);
                Prompt.SetActive(true);
                break;
            case ChoiceState.Decide:
                List.SetActive(true);
                Prompt.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void SetStandby()
    {
        EnterState(ChoiceState.Standby);
    }

    public void StartDecision(Keyword keyword)
    {
        if (keyword.Lock)
        {
            OnEntryChosen(keyword.SetDecision);
        }
        else
        {
            m_draggedInKeyword = keyword;
            EnterState(ChoiceState.Decide);
            SetUpDecisions();
        }
    }

    private void SetUpDecisions()
    {
        for (int i = 0; i < List.transform.childCount; i++)
        {
            List.transform.GetChild(i).gameObject.SetActive(false);
        }

        for (int i = 0; i < m_draggedInKeyword.Data.m_decision.Length; i++)
        {
            List.transform.GetChild(i).gameObject.SetActive(true);
            m_choiceBoxEntries[i].Fill(m_draggedInKeyword.Data.m_decision[i].m_name);
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        HoveredOver = true;
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        HoveredOver = false;
    }


    public bool On = false;
    public override void Show()
    {
        base.Show();
        On = true;
    }

    public override void Empty()
    {
        base.Empty();
        On = false;
    }
}
