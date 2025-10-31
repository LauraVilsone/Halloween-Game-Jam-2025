using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Keyword : MonoBehaviour, IBeginDragHandler, IDragHandler, IEndDragHandler
{
    public KeywordData Data;

    [SerializeField] private Color m_lockedColor;
    private Color m_originalColor;

    private TextMeshProUGUI m_text;
    private Image m_image;

    public Action<Keyword> OnGrab;
    public Action OnRelease;

    public bool Lock { get; private set; }
    private int m_setDecision;
    public int SetDecision => m_setDecision;

    private void Awake()
    {
        m_text = GetComponentInChildren<TextMeshProUGUI>();
        m_image = GetComponent<Image>();
        m_originalColor = m_image.color;

        Lock = false;
    }

    public void AddData(KeywordData data)
    {
        Data = data;
        m_text.text = data.m_name;
    }

    private bool m_dragOnSurface = true;

    private GameObject m_draggingIcon;
    private RectTransform m_draggingPlane;

    public void OnBeginDrag(PointerEventData eventData)
    {
        var canvas = GetComponentInParent<Canvas>();
        if (canvas == null)
            return;

        m_draggingIcon = new GameObject("icon");

        m_draggingIcon.transform.SetParent(canvas.transform, false);
        m_draggingIcon.transform.SetAsLastSibling();

        var image = m_draggingIcon.AddComponent<Image>();
        image.sprite = GetComponent<Image>().sprite;
        image.type = Image.Type.Sliced;
        image.raycastTarget = false;
        
        MatchOther(image.rectTransform, GetComponent<Image>().rectTransform);

        if (m_dragOnSurface)
            m_draggingPlane = transform as RectTransform;
        else m_draggingPlane = canvas.transform as RectTransform;

        SetDraggedPosition(eventData);
        OnGrab?.Invoke(this);
        SFXManager.PlayGrabbingSFX();
    }

    private void SetDraggedPosition(PointerEventData data)
    {
        if (m_dragOnSurface && data.pointerEnter != null && data.pointerEnter.transform as RectTransform != null)
            m_draggingPlane = data.pointerEnter.transform as RectTransform;

        var rt = m_draggingIcon.GetComponent<RectTransform>();
        Vector3 globalMousePosition;
        if (RectTransformUtility.ScreenPointToWorldPointInRectangle(
            m_draggingPlane, data.position, data.pressEventCamera, out globalMousePosition))
        {
            rt.position = globalMousePosition;
            rt.rotation = m_draggingPlane.rotation;
        }
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (m_draggingIcon != null)
            SetDraggedPosition(eventData);
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        transform.position = m_draggingIcon.transform.position;
        OnRelease?.Invoke();
        SFXManager.PlayDroppingSFX();

        if (m_draggingIcon != null)
            Destroy(m_draggingIcon);
    }
    public void MatchOther(RectTransform rt, RectTransform other)
    {
        Vector2 myPrevPivot = rt.pivot;
        myPrevPivot = other.pivot;
        rt.position = other.position;

        rt.localScale = other.localScale;

        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Horizontal, other.rect.width);
        rt.SetSizeWithCurrentAnchors(RectTransform.Axis.Vertical, other.rect.height);
        //rectTransf.ForceUpdateRectTransforms(); - needed before we adjust pivot a second time?
        rt.pivot = myPrevPivot;
    }

    Vector3 m_bottomLeftCorner;
    float m_rectX;
    float m_rectY;

    public void SetSpawnBoundaries(Vector3 bottomLeftCorner, float rectX, float rectY)
    {
        m_bottomLeftCorner = bottomLeftCorner;
        m_rectX = rectX;
        m_rectY = rectY;
    }

    public void SetRandomPosition()
    {
        Vector3 spawnPosition;
        spawnPosition = m_bottomLeftCorner - new Vector3(Random.Range(0, m_rectX), Random.Range(0, m_rectY));
        transform.position = spawnPosition;
    }

    public void OnDecisionSet(int decisionIndex)
    {
        Lock = true;
        m_setDecision = decisionIndex;

        m_image.color = m_lockedColor;
    }
}
