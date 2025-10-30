using System;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TextBox : UIGroup
{
    [SerializeField] private TextMeshProUGUI m_text;
    [SerializeField] private TextMeshProUGUI m_name;

    public override void Show()
    {
        base.Show();
    }

    public void Name(Actors actor)
    {
        if (actor == Actors.Narrator)
            m_name.text = "";
        else
            m_name.text = actor.ToString();
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
