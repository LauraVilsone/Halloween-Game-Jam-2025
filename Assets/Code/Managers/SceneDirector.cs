using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneDirector : MonoBehaviour
{
    public static SceneDirector Instance = null;

    [SerializeField] private Canvas _canvas;
    [SerializeField] private Animator _animator;
    [Space]
    [SerializeField] private float _transitionDuration = .5f;

    private int TRANSITION1_ID = Animator.StringToHash("Transition_1");
    private int TRANSITIONEND_ID = Animator.StringToHash("Transition_End");

    private int currentScene = 0;

    private AsyncOperation _asyncLoadScene;
    private Coroutine _coroutine;


    private void Awake()
    {
        gameObject.SetActive(false);
        if (Instance == null)
        {
            DontDestroyOnLoad(gameObject);

            Instance = this;
        }
        else
            Destroy(gameObject);
    }

    private IEnumerator SceneTransitionBegin()
    {
        _animator.SetTrigger(TRANSITION1_ID);

        yield return new WaitForSecondsRealtime(_transitionDuration);
    }
    private IEnumerator SceneTransitionEnd()
    {
        _animator.SetTrigger(TRANSITIONEND_ID);
        yield return new WaitForSecondsRealtime(_transitionDuration);
    }


    public static void LoadSceneAsync(int id)
    {
        if (Instance == null) return;
        Instance.LoadSceneAsync_Internal(id);
    }
    private void LoadSceneAsync_Internal(int id)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        gameObject.SetActive(true);
        _coroutine = StartCoroutine(LoadLevel(id));
    }
    public static void LoadSceneAsync(string name)
    {
        if (Instance == null) return;
        Instance.LoadSceneAsync_Internal(name);
    }
    private void LoadSceneAsync_Internal(string name)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        gameObject.SetActive(true);
        _coroutine = StartCoroutine(LoadLevel(name));
    }

    private IEnumerator LoadLevel(int id)
    {
        //PauseMenuManager.DisablePausing = true;
        yield return SceneTransitionBegin();

        //if (GameManager.Instance != null)
        //    GameManager.Instance.SaveGame();
        _asyncLoadScene = SceneManager.LoadSceneAsync(id);

        while (!_asyncLoadScene.isDone)
            yield return null;

        yield return SceneTransitionEnd();

        _coroutine = null;
        //PauseMenuManager.DisablePausing = false;
    }
    private IEnumerator LoadLevel(string name)
    {
        //PauseMenuManager.DisablePausing = true;
        yield return SceneTransitionBegin();

        //GameManager.Instance.SaveGame();
        _asyncLoadScene = SceneManager.LoadSceneAsync(name);

        while (!_asyncLoadScene.isDone)
            yield return null;

        yield return SceneTransitionEnd();

        _coroutine = null;
        gameObject.SetActive(false);
        //PauseMenuManager.DisablePausing = false;
    }
    private IEnumerator UnloadLevel(int id)
    {
        _asyncLoadScene = SceneManager.UnloadSceneAsync(id);

        while (!_asyncLoadScene.isDone)
            yield return null;

        _coroutine = null;
    }
    private IEnumerator UnloadLevel(string name)
    {
        _asyncLoadScene = SceneManager.UnloadSceneAsync(name);

        while (!_asyncLoadScene.isDone)
            yield return null;

        _coroutine = null;
    }

    public void TickTestScenes()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
            Application.Quit();

        var newScene = currentScene;

        if (Input.GetKeyDown(KeyCode.F1))
            newScene = 0;
        else if (Input.GetKeyDown(KeyCode.F2))
            newScene = 1;
        else if (Input.GetKeyDown(KeyCode.F3))
            newScene = 2;
        else if (Input.GetKeyDown(KeyCode.F4))
            newScene = 3;
        else if (Input.GetKeyDown(KeyCode.F5))
            newScene = 4;
        else if (Input.GetKeyDown(KeyCode.F6))
            newScene = 5;
        else if (Input.GetKeyDown(KeyCode.F7))
            newScene = 6;
        else if (Input.GetKeyDown(KeyCode.F8))
            newScene = 7;
        else if (Input.GetKeyDown(KeyCode.F9))
            newScene = 8;
        else if (Input.GetKeyDown(KeyCode.F10))
            newScene = 9;
        else if (Input.GetKeyDown(KeyCode.F11))
            newScene = 10;
        else if (Input.GetKeyDown(KeyCode.F12))
            newScene = 11;

        if (newScene != currentScene)
        {
            SceneManager.LoadScene(newScene);
            currentScene = newScene;
        }
    }
}
