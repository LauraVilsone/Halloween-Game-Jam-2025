using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Log : MonoBehaviour, IScrollHandler
{
    private ScrollRect m_scroll;
    private RectTransform m_content;
    private UIFade m_fade;

    [SerializeField] private GameObject m_textboxPrefab;
    [Space]
    [SerializeField] private float m_scrollSpeed;

    public bool Active { get; private set; }

    private void Awake()
    {
        m_scroll = GetComponentInChildren<ScrollRect>();
        m_fade = GetComponent<UIFade>();
        m_content = m_scroll.content;
    }

    private void Start()
    {
        m_fade.SetOpacity(0);
    }

    public void Add(Line line)
    {
        var newLogEntry = Instantiate(m_textboxPrefab, m_content);
        var newLogText = newLogEntry.GetComponent<TextMeshProUGUI>();

        newLogText.text = line.Dialogue;

        m_scroll.normalizedPosition = new Vector2(0, 0);
    }

    public void OnScroll(PointerEventData eventData)
    {
        //m_scroll.verticalScrollbar.value += Input.GetAxis("Mouse ScrollWheel") * m_scrollSpeed * Time.smoothDeltaTime;
        m_scroll.content.anchoredPosition += new Vector2(0, -eventData.scrollDelta.y * m_scrollSpeed * Time.smoothDeltaTime);
    }

    public void Show()
    {
        m_fade.Show();
        Active = true;
    }

    public void Hide()
    {
        m_fade.Hide();
        Active = false;
    }
}
