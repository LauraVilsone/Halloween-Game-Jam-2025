using UnityEngine;

public class Bar : MonoBehaviour
{
    public GameObject ButtonList;
    public GameObject QuitPrompt;

    private void Start()
    {
        ToggleList(true);
        TogglePrompt(false);
    }

    public void ToggleList(bool on)
    {
        ButtonList.SetActive(on);
    }
    public void TogglePrompt(bool on)
    {
        QuitPrompt.SetActive(on);
    }

    public void QuitToMenu()
    {
        SceneDirector.LoadSceneAsync(0);
    }
}
