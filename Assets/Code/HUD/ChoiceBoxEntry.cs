using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChoiceBoxEntry : MonoBehaviour, IPointerClickHandler
{
    private TextMeshProUGUI m_text;

    public Action<int> OnChosen;

    private void Awake()
    {
        m_text = GetComponentInChildren<TextMeshProUGUI>();
    }

    public void Fill(string text)
    {
        m_text.text = text;
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        OnChosen?.Invoke(transform.GetSiblingIndex());
    }
}
