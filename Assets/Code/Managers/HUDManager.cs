using TMPro;
using UnityEngine;

public class HUDManager : MonoBehaviour
{
    public TextBox Box;
    public MindMap MindMap;
    public ChoiceBox ChoiceBox;

    private void Awake()
    {
        if (Box == null)
        {
            Box = GetComponentInChildren<TextBox>();
        }

        if (MindMap == null)
        {
            MindMap = GetComponentInChildren<MindMap>();
        }

        if (ChoiceBox == null)
        {
            ChoiceBox = GetComponentInChildren<ChoiceBox>();
        }
    }


    private enum HUDState { TextBox, MindMap, ChoiceBox }
    private HUDState m_state;



}
