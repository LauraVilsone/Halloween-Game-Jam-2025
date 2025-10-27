using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextBox Box;

    private void Awake()
    {
        if (Box == null)
        {
            Box = GetComponentInChildren<TextBox>();
        }
    }

}
