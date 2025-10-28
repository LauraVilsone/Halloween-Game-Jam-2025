using UnityEngine;

public class UIGroup : MonoBehaviour
{
    protected UIFade m_fade;

    protected virtual void Awake()
    {
        m_fade = GetComponent<UIFade>();
    }

    public virtual void Start()
    {
        m_fade.SetOpacity(0);
    }

    public virtual void Show()
    {
        m_fade.Show();
    }

    public virtual void Empty()
    {
        m_fade.Hide();
    }
}
