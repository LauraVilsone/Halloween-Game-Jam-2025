using System.Collections.Generic;
using UnityEngine;

public class InteractableManager : MonoBehaviour
{
    private List<Interactable> m_interactables;

    private void Awake()
    {
        FindInteractables();
    }

    private void FindInteractables()
    {
        m_interactables = new List<Interactable>();
        m_interactables.AddRange(FindObjectsByType<Interactable>(FindObjectsSortMode.InstanceID));
    }
}
