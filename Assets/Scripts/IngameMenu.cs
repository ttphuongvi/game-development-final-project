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
        GameObject[] obj = GameObject.FindGameObjectsWithTag("GameController");
        if (obj.Length > 0) {
            gameManager = obj[0];
        }

        string nameScene = SceneManager.GetActiveScene().name;
        indexLevel = (int)nameScene[nameScene.Length - 1] - 49;
    }

    void Update()
    {
        if (gameManager) {
            Level level = gameManager.GetComponent<GameManager>().listLevel[indexLevel];
            lbScore.text = level.CurrentScore.ToString();
            GameObject[] listPig = GameObject.FindGameObjectsWithTag("Enemy");
            GameObject[] listBird = GameObject.FindGameObjectsWithTag("Bird");
            GameObject crossBow = GameObject.FindGameObjectsWithTag("CrossBow")[0];
            if (listPig.Length == 0) {
                Debug.Log("Win");
                level.Defeated = true;
                level.CurrentScore = 0;
                if (level.HighScore < level.CurrentScore)
                    level.HighScore = level.CurrentScore;
                SceneManager.LoadScene("ChooseLevel");
            }

            if (listBird.Length == 0) {
                Debug.Log("Lose");
                level.CurrentScore = 0;
                SceneManager.LoadScene("ChooseLevel");
            } else if (crossBow.GetComponent<CrossBow>().SelectedBird == null) {
                CrossBow crossBowComponent = crossBow.GetComponent<CrossBow>();
                crossBowComponent.SelectedBird = listBird[0];
                crossBowComponent.CrossBowState = CrossBow.CrossBowStateEnum.Idle;
            }

        }
    }



}
