using System;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu(fileName = "Keyword Table", menuName = "Scriptables/String To Keyword Table")]
public class StringToKeywordTable : ScriptableObject
{
    [System.Serializable]
    public struct StringToKeyword
    {
        public string String;
        public KeywordData Data;
    }

    public StringToKeyword[] m_references;

    private Dictionary<string, KeywordData> m_table;
    public Dictionary<string, KeywordData> Table => m_table;

    public void BuildTable()
    {
        m_table = new Dictionary<string, KeywordData>();

        foreach (var keyvalue in m_references)
            m_table.Add(keyvalue.String, keyvalue.Data);
    }
}
