using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class IngameMenu : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root, playState, pauseState, gameOverState;
    [HideInInspector]
    public Button btnPause, btnReturn, btnReturnPause, btnResume, btnReload;
    [HideInInspector]
    public Label lbScore;
    [ShowOnly]
    public int indexLevel;

    public GameObject gameManager;
    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        btnPause = root.Q<Button>("btnPause");
        btnPause.clicked += OnPauseClicked;

        btnReturn = root.Q<Button>("btnReturn");
        btnReturn.clicked += OnReturnClicked;

        btnReturnPause = root.Q<Button>("btnReturnPause");
        btnReturnPause.clicked += OnReturnClicked;

        btnResume = root.Q<Button>("btnResume");
        btnResume.clicked += OnResumeClicked;

        btnReload = root.Q<Button>("btnReload");
        btnReload.clicked += OnReloadClicked;

        lbScore = root.Q<Label>("lbScore");

        playState = root.Q<VisualElement>("playState");
        pauseState = root.Q<VisualElement>("pauseState");
    }

    void OnPauseClicked()
    {
        Time.timeScale = 0;
        playState.style.display = DisplayStyle.None;
        pauseState.style.display = DisplayStyle.Flex;
    }

    void OnResumeClicked() {
        playState.style.display = DisplayStyle.Flex;
        pauseState.style.display = DisplayStyle.None;
        Time.timeScale = 1;
    }

    void OnReloadClicked() {
        // TODO
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void OnReturnClicked() {
        SceneManager.LoadScene("ChooseLevel");
    }
    
    void Start() {
        gameManager = GameObject.FindGameObjectsWithTag("GameController")[0];
        string nameScene = SceneManager.GetActiveScene().name;
        indexLevel = (int)nameScene[nameScene.Length - 1] - 49;
    }

    void Update()
    {
        lbScore.text = gameManager.GetComponent<GameManager>().listLevel[indexLevel].CurrentScore.ToString();
    }



}
