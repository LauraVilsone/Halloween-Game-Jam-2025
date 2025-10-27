using Unity.Mathematics;
using UnityEngine;

public class PlayerManager
{
    private bool m_lastMouseLeftValue;
    private bool m_lastMouseRightValue;

    private bool m_mouseLeftHeld;
    private bool m_mouseRightHeld;

    public bool MouseLeftDown { get; set; }
    public bool MouseLeftHeld { get; set; }

    public bool MouseRightDown { get; set; }
    public bool MouseRightHeld { get; set; }
    
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


        MouseLeftDown = Input.GetMouseButtonDown(0);
        MouseRightDown = Input.GetMouseButtonDown(1);

        MouseLeftHeld = Input.GetMouseButton(0);
        MouseRightHeld = Input.GetMouseButton(1);

        MouseDelta = Input.GetAxis("Mouse X");
    }

    private void ResetInput()
    {
        MouseLeftDown = false;
        MouseRightDown = false;
        MouseDelta = 0;
    }
}
