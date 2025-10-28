using System;
using System.Collections.Generic;
using UnityEngine;

public class KeywordManager : MonoBehaviour
{
    public StringToKeywordTable m_referenceTable;

    private HashSet<KeywordData> m_collectedKeywords;

    private InventoryManager m_inventoryManager;

    private void Awake()
    {
        m_collectedKeywords = new HashSet<KeywordData>();
        m_inventoryManager = FindFirstObjectByType<InventoryManager>();
    }

    private void Start()
    {
        m_referenceTable.BuildTable();
    }

    public void OnKeywordGain(string linkID)
    {
        if (m_referenceTable.Table.ContainsKey(linkID))
        {
            var keyword = m_referenceTable.Table[linkID];

            if (m_collectedKeywords.Contains(keyword))
                return;

            m_collectedKeywords.Add(keyword);
            m_inventoryManager.AddKeyword(keyword);
        }
    }
}
