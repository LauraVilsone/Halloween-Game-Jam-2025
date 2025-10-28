using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextBox : UIGroup
{
    [SerializeField] private TextMeshProUGUI m_text;

    public override void Show()
    {
        base.Show();
    }

    public void Fill(string text)
    {
        m_text.text = text;
    }

    public override void Empty()
    {
        base.Empty();
        m_text.text = "";
    }
}
