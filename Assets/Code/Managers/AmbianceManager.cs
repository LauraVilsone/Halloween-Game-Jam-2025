using UnityEngine;

public class AmbianceManager : MonoBehaviour
{
    [SerializeField] private bool m_startWithAmbiance = true;

    private AudioSource Source;

    [Space]
    [SerializeField] private float m_maxVolume = 1f;
    [SerializeField] private float m_fadeSpeed = .3f;

    private float m_targetVolume;
    private float m_prevVolume;

    private float m_startTime;

    private void Awake()
    {
        TryGetComponent(out Source);
    }

    private void Start()
    {
        m_startTime = Time.time;

        Source.Play();
        if (m_startWithAmbiance)
            PlayAmbiance();
        else
            StopAmbiance(true);
    }

    private void Update()
    {
        if (Source.volume != m_targetVolume)
        {
            float t = (Time.time - m_startTime) / m_fadeSpeed;
            Source.volume = Mathf.SmoothStep(m_prevVolume, m_targetVolume, t);
        }
    }

    private void PlayAmbiance()
    {
        m_prevVolume = Source.volume;
        m_targetVolume = m_maxVolume;
    }

    private void StopAmbiance(bool abrupt = false)
    {
        if (abrupt)
        {
            Source.volume = 0;
        }
        m_prevVolume = Source.volume;
        m_targetVolume = 0;

    }
}
