using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableText : MonoBehaviour, IPointerClickHandler, IPointerMoveHandler
{
    private DialogueManager m_dialogueManager;
    private KeywordManager m_keywordManager;
    private HUDManager m_HUDManager;

    private void Awake()
    {
        m_keywordManager = FindFirstObjectByType<KeywordManager>();
        m_HUDManager = FindFirstObjectByType<HUDManager>();
        m_dialogueManager = FindFirstObjectByType<DialogueManager>();
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
            var linkText = linkInfo.GetLinkText();

            var success = m_keywordManager.OnKeywordGain(linkID);
            //success = true;
            if (success)
            {
                Vector3 position = Vector3.zero;
                foreach (var wordInfo in m_text.textInfo.wordInfo)
                {
                    if (wordInfo.characterCount == 0 || wordInfo.GetWord() != linkText)
                        continue;

                    var firstCharInfo = m_text.textInfo.characterInfo[wordInfo.firstCharacterIndex];
                    var lastCharInfo = m_text.textInfo.characterInfo[wordInfo.lastCharacterIndex];
                    var wordLocation = m_text.transform.TransformPoint((firstCharInfo.topLeft + lastCharInfo.bottomRight) / 2f);

                    position = wordLocation;
                    break;
                }

                m_HUDManager.OnKeywordGain(position, linkText);
                m_dialogueManager.RewriteWithoutTags();
            }
        }
    }
}
