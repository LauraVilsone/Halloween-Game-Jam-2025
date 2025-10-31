using System;
using UnityEngine;

public class IllustrationManager : MonoBehaviour
{
    private readonly int FADE_ID = Animator.StringToHash("Fade");
    private readonly int SHOW_ID = Animator.StringToHash("Show");
    private readonly int QUICKSHOW_ID = Animator.StringToHash("QuickShow");
    private readonly int STAYFADED_ID = Animator.StringToHash("StayFaded");
    private readonly int END_ID = Animator.StringToHash("End");

    public SpriteRenderer m_illustration;
    public SpriteRenderer m_endingIllustration;

    private Animator m_animator;
    [SerializeField] private Animator m_endingAnimator;

    public Action OnAnimationFinished;

    private float m_fadeDuration = .5f;

    private void Awake()
    {
        m_animator = GetComponent<Animator>();
    }

    public void ShowEnding(Sprite sprite)
    {
        m_endingIllustration.sprite = sprite;
        m_endingAnimator.SetTrigger(END_ID);
    }

    public void ShowIllustration(Sprite sprite, bool quick, bool intro)
    {
        m_illustration.sprite = sprite;

        if (intro)
        {
            m_animator.SetTrigger(STAYFADED_ID);
            Invoke("AnimationFinished", m_fadeDuration * 2);
        }
        else if (quick)
        {
            m_animator.SetTrigger(QUICKSHOW_ID);
            m_animator.SetBool(SHOW_ID, true);
            Invoke("AnimationFinished", m_fadeDuration * 2);
        }
        else
        {
            m_animator.SetTrigger(FADE_ID);
            m_animator.SetBool(SHOW_ID, true);
            Invoke("AnimationFinished", m_fadeDuration);
        }
    }

    public void HideIllustration()
    {
        m_animator.SetBool(SHOW_ID, false);
        m_animator.SetTrigger(FADE_ID);
        Invoke("AnimationFinished", m_fadeDuration * 2);
    }

    public void Fade()
    {
        m_animator.SetTrigger(FADE_ID);
        Invoke("AnimationFinished", m_fadeDuration);
    }

    private void AnimationFinished()
    {
        OnAnimationFinished?.Invoke();
    }
}
