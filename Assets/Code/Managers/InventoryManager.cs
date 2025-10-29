using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public MindMap Map;
    public ChoiceBox Choice;

    public bool Active => Map.Active;

    public KeywordData SelectedKeyword => Map.SelectedKeyword;


    public void AddKeyword(KeywordData keyword)
    {
        Map.Fill(keyword);
    }

    public void Toggle(bool decisionAvailable)
    {
        Map.Toggle();
        if (decisionAvailable)
        {
            if (Map.Visible)
            {
                Choice.Show();
                Choice.SetStandby();
            }
            else
            {
                Choice.Empty();
            }
        }
    }
}
