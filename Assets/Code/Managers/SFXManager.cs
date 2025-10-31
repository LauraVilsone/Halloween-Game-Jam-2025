using UnityEngine;
using UnityEngine.Audio;

public class SFXManager : MonoBehaviour
{
    private AudioSource Source;

    public static SFXManager Instance;

    [SerializeField] private SFXCollection m_sfxCollection;
    [Space]
    [SerializeField] private AudioMixerGroup m_sfxMixerGroup;

    private void Awake()
    {
        Source = GetComponent<AudioSource>();

        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(gameObject);
    }

    static public void PlayTypingSFX() => Instance.PlayTypingSFX_Internal();
    private void PlayTypingSFX_Internal() => PlaySFX_Internal(m_sfxCollection[0].Clip, m_sfxCollection[0].Volume);
    static public void PlayNextDialogueSFX() => Instance.PlayNextDialogueSFX_Internal();
    private void PlayNextDialogueSFX_Internal() => PlaySFX_Internal(m_sfxCollection[1].Clip, m_sfxCollection[1].Volume);
    static public void PlayCloseConversationSFX() => Instance.PlayCloseConversationSFX_Internal();
    private void PlayCloseConversationSFX_Internal() => PlaySFX_Internal(m_sfxCollection[2].Clip, m_sfxCollection[2].Volume);
    static public void PlayOpenInventorySFX() => Instance.PlayOpenInventorySFX_Internal();
    private void PlayOpenInventorySFX_Internal() => PlaySFX_Internal(m_sfxCollection[3].Clip, m_sfxCollection[3].Volume);
    static public void PlayCloseInventorySFX() => Instance.PlayCloseInventorySFX_Internal();
    private void PlayCloseInventorySFX_Internal() => PlaySFX_Internal(m_sfxCollection[4].Clip, m_sfxCollection[4].Volume);
    static public void PlayGrabbingSFX() => Instance.PlayGrabbingSFX_Internal();
    private void PlayGrabbingSFX_Internal() => PlaySFX_Internal(m_sfxCollection[5].Clip, m_sfxCollection[5].Volume);
    static public void PlayDroppingSFX() => Instance.PlayDroppingSFX_Internal();
    private void PlayDroppingSFX_Internal() => PlaySFX_Internal(m_sfxCollection[6].Clip, m_sfxCollection[6].Volume);
    static public void PlaySelectingChoiceSFX() => Instance.PlaySelectingChoiceSFX_Internal();
    private void PlaySelectingChoiceSFX_Internal() => PlaySFX_Internal(m_sfxCollection[7].Clip, m_sfxCollection[7].Volume);
    static public void PlayCollectSFX() => Instance.PlayCollectSFX_Internal();
    private void PlayCollectSFX_Internal() => PlaySFX_Internal(m_sfxCollection[8].Clip, m_sfxCollection[8].Volume);

    static public void PlaySFX(AudioClip p_clip, float p_volume = 1, bool p_randomizePitch = false)
    {
        Instance.PlaySFX_Internal(p_clip, p_volume, p_randomizePitch);
    }
    private void PlaySFX_Internal(AudioClip p_clip, float p_volume, bool p_randomizePitch = false)
    {
        if (p_clip == null)
            return;
        /*if (p_randomizePitch)
            Source.pitch = Random.Range(1 - m_pitchRange, 1 + m_pitchRange);
        else if (Source.pitch != 1)
            Source.pitch = 1*/;

        Source.PlayOneShot(p_clip, p_volume);
    }


}
