using System;
using TMPro;
using UnityEngine;

public class TextBox : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI m_text;

    public void Start()
    {
        gameObject.SetActive(false);
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Fill(string text)
    {
        m_text.text = text;
    }

    public void Empty()
    {
        m_text.text = "";
        gameObject.SetActive(false);
    }
}
