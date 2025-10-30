using TMPro;
using UnityEngine;

public class KeywordFlash : MonoBehaviour
{
    private Animator m_animator;
    private TextMeshProUGUI m_text;

    private readonly int FLASH_ID = Animator.StringToHash("Flash");


    private void Awake()
    {
        m_animator = GetComponent<Animator>();
        m_text = GetComponent<TextMeshProUGUI>();
    }

    public void Flash(Vector3 position, string keyword)
    {
        m_animator.SetTrigger(FLASH_ID);
        m_text.text = "<color=green>" + keyword + "</color>";
        m_text.transform.position = position;
    }
}
