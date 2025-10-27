using UnityEngine;

public class Room : MonoBehaviour
{
    public float m_moveMod = 0.5f;
    public float m_maxMoveSpeed = 0.75f;

    private Wallpaper m_wallpaper;

    private Wallpaper[] m_wallpaperExtensions;

    private void Start()
    {
        m_wallpaper = GetComponentInChildren<Wallpaper>();

        m_wallpaperExtensions = new Wallpaper[3] {
            m_wallpaper,
            Instantiate(m_wallpaper, new Vector3(m_wallpaper.Length * 2, 0), Quaternion.identity, this.transform),
            Instantiate(m_wallpaper, new Vector3(-m_wallpaper.Length* 2, 0), Quaternion.identity, this.transform)
        };
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
}
