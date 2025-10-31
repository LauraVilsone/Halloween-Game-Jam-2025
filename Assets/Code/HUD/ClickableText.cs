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

            string concatenatedWord = "";
            int firstCharacterIndex = 0;
            int lastCharacterIndex = 0;

            var success = m_keywordManager.OnKeywordGain(linkID);
            //success = true;

            if (success)
            {
                Vector3 position = Vector3.zero;
                string[] linkWords = linkText.Split(' ');
                
                //foreach (var wordInfo in m_text.textInfo.wordInfo)
                for (int i = 0; i < m_text.textInfo.wordInfo.Length; i++)
                {
                    var wordInfo = m_text.textInfo.wordInfo[i];
                    if (wordInfo.textComponent == null) break;

                    var word = wordInfo.GetWord();
                    if (wordInfo.characterCount == 0)
                        continue;
                    else
                    {
                        if (linkText.Contains(word))
                        {
                            concatenatedWord = word;

                            var otherWordInfo = wordInfo;
                            if (linkWords.Length > 1)
                            {
                                int j = i + 1;
                                otherWordInfo = m_text.textInfo.wordInfo[j];
                                string otherword = "";
                                for (; j < i + (linkWords.Length); j++)
                                {
                                    otherWordInfo = m_text.textInfo.wordInfo[j];
                                    otherword = otherWordInfo.GetWord();
                                    concatenatedWord = concatenatedWord + " " + otherword;
                                }
                            }

                            if (!linkText.Contains(concatenatedWord))
                                continue;

                            firstCharacterIndex = wordInfo.firstCharacterIndex;
                            lastCharacterIndex = otherWordInfo.lastCharacterIndex;
                        }
                        else continue;
                    }

                    var firstCharInfo = m_text.textInfo.characterInfo[firstCharacterIndex];
                    var lastCharInfo = m_text.textInfo.characterInfo[lastCharacterIndex];
                    var wordLocation = m_text.transform.TransformPoint((firstCharInfo.topLeft + lastCharInfo.bottomRight) / 2f);

                    position = wordLocation;
                    break;
                }

                m_HUDManager.OnKeywordGain(position, linkText);
                m_dialogueManager.RewriteWithoutTags();
                SFXManager.PlayCollectSFX();
            }
        }
    }
}
