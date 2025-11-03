using UnityEngine;

public class Portrait : MonoBehaviour
{
    private SpriteRenderer m_renderer;
    private SpriteRenderer m_fadeRenderer;
    private Animator m_animator;

    private readonly int FADE_ID = Animator.StringToHash("Fade");

    private Sprite m_newSprite;

    private void Awake()
    {
        m_renderer = transform.GetChild(0).GetComponent<SpriteRenderer>();
        m_fadeRenderer = transform.GetChild(1).GetComponent<SpriteRenderer>();

        m_animator = GetComponentInChildren<Animator>();
    }

    public void ChangePortrait(Sprite sprite)
    {
        if (m_fadeRenderer.sprite == sprite)
            return;

        m_fadeRenderer.sprite = sprite;
        m_animator.SetTrigger(FADE_ID);
    }

    public void OnFadeEnd()
    {
        m_renderer.sprite = m_fadeRenderer.sprite;
    }
}
