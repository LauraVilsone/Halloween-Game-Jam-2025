using System.Collections.Generic;
using TMPro;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Hint : MonoBehaviour
{
    [SerializeField] private float m_messageDuration;
    [Space]
    [SerializeField] private CanvasGroup m_fadegroup;
    [SerializeField] private CanvasGroup m_flashgroup;
    [Space]
    [SerializeField] private float2 m_alphaMinMax;
    [SerializeField] private float m_curveSpeed = 1;
    [SerializeField] private AnimationCurve m_curve;

    private UIFade m_fade;
    private TextMeshProUGUI m_text;
    private Image m_icon;

    private float m_curveTime = 0;
    private float m_messageTime = 0;

    private HashSet<HintMessage> m_sentMessages;

    private void Awake()
    {
        m_icon = GetComponentInChildren<Image>();
        m_text = GetComponentInChildren<TextMeshProUGUI>();
        m_fade = GetComponent<UIFade>();

        m_sentMessages = new HashSet<HintMessage>();
    }

    private void Start()
    {
        m_fade.SetOpacity(0);
    }

    private void Update()
    {
        if (m_messageTime > 0)
        {
            m_messageTime -= Time.deltaTime;

            m_curveTime += m_curveSpeed * Time.deltaTime;
            var alpha = Mathf.Lerp(m_alphaMinMax.x, m_alphaMinMax.y, m_curve.Evaluate(m_curveTime));
            m_flashgroup.alpha = alpha;

            if (m_messageTime <= 0)
            {
                m_fade.Hide();
            }
        }
    }

    public void SendMessage(HintMessage message)
    {
        if (m_sentMessages.Contains(message))
            return;
        else
            m_sentMessages.Add(message);

        m_text.text = message.m_message;
        m_icon.sprite = message.m_icon;

        if (m_messageTime <= 0)
        {
            m_fadegroup.alpha = 0;
            m_curveTime = 0;

            m_fade.Show();
        }

        m_messageTime = m_messageDuration;
    }
}
