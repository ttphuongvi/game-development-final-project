using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root, playState, pauseState, gameOverState;
    [HideInInspector]
    public Button btnPause, btnReturn, btnResume, btnReload;
    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        btnPause = root.Q<Button>("btnPause");
        btnPause.clicked += OnPauseClicked;

        btnReturn = root.Q<Button>("btnReturn");
        btnReturn.clicked += OnReturnClicked;

        btnResume = root.Q<Button>("btnResume");
        btnResume.clicked += OnResumeClicked;

        btnReload = root.Q<Button>("btnReload");
        btnReload.clicked += OnReloadClicked;

        playState = root.Q<VisualElement>("playState");
        pauseState = root.Q<VisualElement>("pauseState");
    }

    void OnPauseClicked()
    {
        Time.timeScale = 0;
        playState.style.display = DisplayStyle.None;
        pauseState.style.display = DisplayStyle.Flex;
    }

    void OnReturnClicked()
    {
        Debug.Log("Clicked");
    }

    void OnResumeClicked() {
        playState.style.display = DisplayStyle.Flex;
        pauseState.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    void OnReloadClicked() {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
