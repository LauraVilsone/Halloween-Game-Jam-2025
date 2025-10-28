using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableText : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    private KeywordManager m_keywordManager;

    private void Awake()
    {
        m_keywordManager = FindFirstObjectByType<KeywordManager>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        /*
        var m_text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                var linkInfo = m_text.textInfo.linkInfo[linkIndex];
                var linkID = linkInfo.GetLinkID();
            }
        }*/
    }

    public void OnPointerMove(PointerEventData eventData)
    {
        var m_text = GetComponent<TextMeshProUGUI>();
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_text, Input.mousePosition, null);
        if (linkIndex > -1)
        {
            var linkInfo = m_text.textInfo.linkInfo[linkIndex];
            var linkID = linkInfo.GetLinkID();
            m_keywordManager.OnKeywordGain(linkID);
        }
    }
}
