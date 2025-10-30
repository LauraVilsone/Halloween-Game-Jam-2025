using UnityEngine;

public class BondManager : MonoBehaviour
{
    [SerializeField] private int m_startingLevel = 0;

    public int Level { get; set; }

    private void Awake()
    {
        Level = m_startingLevel;
    }
}