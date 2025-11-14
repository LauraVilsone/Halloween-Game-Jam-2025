using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnHoverEnter;
    public Action OnHoverExit;

    private Button m_button;
    private bool m_hovered;

    public void Awake()
    {
        TryGetComponent(out m_button);
        m_hovered = false;
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        m_hovered = true;
        OnHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        m_hovered = false;
        OnHoverExit?.Invoke();
    }

    private void OnDisable()
    {
        if (m_hovered)
        {
            OnHoverExit();
        }
    }
}
