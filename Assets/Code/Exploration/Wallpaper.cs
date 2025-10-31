using UnityEngine;

public class Wallpaper : MonoBehaviour
{
    private SpriteRenderer m_renderer;

    public SpriteRenderer Renderer => m_renderer;
    public float Length { get; private set; }

    private void Awake()
    {
        TryGetComponent(out m_renderer);

        Length = m_renderer.bounds.extents.x;
    }
}
