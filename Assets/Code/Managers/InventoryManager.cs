using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public MindMap Map;
    public ChoiceBox Choice;
    [Space]
    [SerializeField] private KeywordData[] m_initialKeywords;

    public bool Active => Map.Active;

    public KeywordData SelectedKeyword => Map.SelectedKeyword?.Data ?? null;

    private void Start()
    {
        foreach (var data in m_initialKeywords)
            AddKeyword(data);
    }

    public void AddKeyword(KeywordData keyword)
    {
        Map.Fill(keyword);
    }

    public void Toggle()
    {
        Map.Toggle();
    }
}
