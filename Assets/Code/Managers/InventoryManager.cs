using System;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public MindMap Map;

    public bool Active => Map.Active;

    public KeywordData SelectedKeyword => Map.SelectedKeyword;


    public void AddKeyword(KeywordData keyword)
    {
        Map.Fill(keyword);
    }

    public void Toggle()
    {
        Map.Toggle();
    }
}
