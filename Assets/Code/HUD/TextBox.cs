using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextBox : UIGroup
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TextMeshProUGUI m_name;
    [Space]
    [SerializeField] private Image m_header;

    public string Text => m_text.text;

    public override void Show()
    {
        base.Show();
    }

    public void Name(Actors actor)
    {
        if (actor == Actors.Narrator)
        {
            m_name.text = "";
            m_header.gameObject.SetActive(false);
        }
        else
        {
            m_name.text = actor.ToString();
            m_header.gameObject.SetActive(true);
        }
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
