using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableText : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler
{
    public void OnPointerClick(PointerEventData eventData)
    {
        var m_text = GetComponent<TextMeshProUGUI>();
        if (eventData.button == PointerEventData.InputButton.Right)
        {
            int linkIndex = TMP_TextUtilities.FindIntersectingLink(m_text, Input.mousePosition, null);
            if (linkIndex > -1)
            {
                var linkInfo = m_text.textInfo.linkInfo[linkIndex];
                var linkID = linkInfo.GetLinkID();
            }
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //Debug.Log("Entering Text");
    }
}
