using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UIButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    public Action OnHoverEnter;
    public Action OnHoverExit;

    private Button m_button;

    public void Awake()
    {
        TryGetComponent(out m_button);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        OnHoverEnter?.Invoke();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        OnHoverExit?.Invoke();
    }
}
