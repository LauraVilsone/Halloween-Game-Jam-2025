using Unity.Mathematics;
using UnityEngine;

public class PlayerManager
{

    public bool MouseLeftDown { get; set; }
    public bool MouseRightDown { get; set; }
    public float MouseDelta { get; set; }

    private bool m_lock;
    public bool MouseLock { get => m_lock; set {
            m_lock = value;
            ResetInput();
        }
    }

    public void Tick()
    {
        if (MouseLock)
        {
            return;
        }

        if (Input.GetMouseButton(0))
        {
            MouseLeftDown = true;
        }
        else
        {
            MouseLeftDown = false;
        }

        if (Input.GetMouseButton(1))
        {
            MouseRightDown = true;
        }
        else
        {
            MouseRightDown = false;
        }

        MouseDelta = Input.GetAxis("Mouse X");
    }

    private void ResetInput()
    {
        MouseLeftDown = false;
        MouseRightDown = false;
        MouseDelta = 0;
    }
}
