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

    public void ToggleInteractableVisibility(bool toggle)
    {
        foreach (var interactable in m_interactables)
        {
            if (interactable.AnimateOnHover)
            {
                if (toggle)
                    interactable.Fade.Show();
                else
                    interactable.Fade.Hide();
            }
        }
    }
}
