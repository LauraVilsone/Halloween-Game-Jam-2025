using UnityEngine;

public class Room : MonoBehaviour
{
    public float m_moveMod = 0.5f;
    public float m_maxMoveSpeed = 0.75f;
    [Space]
    [SerializeField] private Color m_conversationColor;
    public float m_fadeSpeed = .5f;

    private Wallpaper m_wallpaper;

    private Color m_originalColor;

    private Color m_targetColor;
    
    private float m_lerpTime = 0;


    private Wallpaper[] m_wallpaperExtensions;

    private void Start()
    {
        m_wallpaper = GetComponentInChildren<Wallpaper>();
        m_originalColor = m_targetColor = m_wallpaper.Renderer.color;

        m_wallpaperExtensions = new Wallpaper[3] {
            m_wallpaper,
            Instantiate(m_wallpaper, new Vector3(m_wallpaper.Length * 2, 0), Quaternion.identity, this.transform),
            Instantiate(m_wallpaper, new Vector3(-m_wallpaper.Length* 2, 0), Quaternion.identity, this.transform)
        };

        OnTalk();
    }

    private void Update()
    {
        if (!m_wallpaper.Renderer.color.Equals(m_targetColor))
        {
            m_lerpTime += Time.deltaTime;
            Color color = Color.Lerp(m_wallpaper.Renderer.color, m_targetColor, m_lerpTime);
            UpdateRenderers(color);
            if (m_lerpTime >= 1)
                UpdateRenderers(m_targetColor);
        }
    }

    public void MouseMove(float delta)
    {
        float move = delta * m_moveMod;

        if (move > m_maxMoveSpeed)
            move = m_maxMoveSpeed;
        else if (move < -m_maxMoveSpeed)
            move = -m_maxMoveSpeed;

        Move(move);
    }

    public void Move(float delta)
    {
        foreach (var wallpaper in m_wallpaperExtensions)
        {
            wallpaper.transform.Translate(delta, 0, 0);
        }

        CheckConstrains();
    }

    private void CheckConstrains()
    {
        float halfExtent = (m_wallpaper.Length);
        if (m_wallpaper.transform.position.x > halfExtent)
        {
            Move(-halfExtent*2);
        }
        else if (m_wallpaper.transform.position.x < -halfExtent)
        {
            Move(halfExtent*2);
        }
    }

    public void OnTalk()
    {
        m_targetColor = m_conversationColor;
        m_lerpTime = 0;
    }

    public void OnExit()
    {
        m_targetColor = m_originalColor;
        m_lerpTime = 0;
    }


    private void UpdateRenderers(Color color) => UpdateRenderers(color, m_wallpaperExtensions);
    private void UpdateRenderers(Color color, Wallpaper[] wallpapers)
    {
        if (wallpapers == null || wallpapers.Length == 0) return;
        foreach (var wallpaper in wallpapers)
        {
            if (wallpaper == null) continue;
            wallpaper.Renderer.color = color;//new Color(wallpaper.Renderer.color.r, wallpaper.Renderer.color.g, wallpaper.Renderer.color.b, opacity);
        }
    }

}
