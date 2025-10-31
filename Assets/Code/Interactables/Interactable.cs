using System;
using Tools.Fade;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    public InteractableData Data;

    public bool ReactToKeywords = true;
    public bool AnimateOnHover = true;

    public Action<InteractableData> OnHoverStart;
    public Action<InteractableData> OnClick;
    public Action<InteractableData> OnRelease;
    public Action OnHoverEnd;

    [Space]
    [SerializeField] private float m_sizeMod = .125f;
    [SerializeField] private float m_rotationMod = 120f;
    [SerializeField] private AnimationCurve m_hoverAnimationCurve;
    private float m_curveTime;
    private float m_curveTimeMod = 2;
    private float m_curveValue;

    private SpriteRenderer m_renderer;
    
    private bool Active { get; set; }

    private Vector3 m_original;
    private SpriteFade m_fade;

    public SpriteFade Fade => m_fade;

    private void Awake()
    {
        m_fade = GetComponentInChildren<SpriteFade>();
        m_renderer = GetComponentInChildren<SpriteRenderer>();
        m_original = m_renderer.transform.localScale;
    }

    private void OnMouseEnter()
    {
        OnHoverStart?.Invoke(Data);
        Active = true;
    }

    private void OnMouseUp()
    {
        if (ReactToKeywords)
            OnRelease?.Invoke(Data);
    }

    private void OnMouseUpAsButton()
    {
        if (ReactToKeywords)
            OnClick?.Invoke(Data);
    }

    //For right-click purposes
    /*function OnMouseOver()
    {
        If(Input.GetMouseButtonUp(1)){
            //do stuff here
        }
    }*/

    private void OnMouseExit()
    {
        OnHoverEnd?.Invoke();
        Active = false;
    }

    private void Update()
    {
        if (AnimateOnHover)
            HandleAnimation();
    }

    private void HandleAnimation()
    {
        if (Active)
        {
            if (m_curveTime < 1)
            {
                m_curveTime += Time.deltaTime * m_curveTimeMod;
                if (m_curveTime >= 1)
                    m_curveTime = 1;
            }
        }
        else
        {
            if (m_curveTime > 0)
            {
                m_curveTime -= Time.deltaTime * m_curveTimeMod;
                if (m_curveTime <= 0)
                    m_curveTime = 0;
            }
        }

        m_curveValue = m_hoverAnimationCurve.Evaluate(m_curveTime);
        float mod = m_sizeMod * m_curveValue;
        m_renderer.transform.localScale = m_original + new Vector3(mod, mod, 0);
        m_renderer.transform.eulerAngles = new Vector3(0, 0, Mathf.Lerp(0, m_rotationMod, m_curveValue));
    }
}
