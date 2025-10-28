using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public MindMap Map;

    public bool Active => Map.Active;

    private void Awake()
    {
        
    }

    public void AddKeyword(KeywordData keyword)
    {
        Map.Fill(keyword);
    }
}
