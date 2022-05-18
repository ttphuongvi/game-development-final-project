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
    public int indexLevel, currentScore = 0;

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
        GameObject[] listPig = GameObject.FindGameObjectsWithTag("Enemy");
        GameObject[] listBird = GameObject.FindGameObjectsWithTag("Bird");
        GameObject crossBow = GameObject.FindGameObjectsWithTag("CrossBow")[0];
        lbScore.text = currentScore.ToString();

        
        if (gameManager) {
            Level level = gameManager.GetComponent<GameManager>().listLevel[indexLevel];
            

            if (listPig.Length == 0) {
                Debug.Log("Win");
                level.Defeated = true;
                currentScore = 0;
                if (level.HighScore < currentScore)
                    level.HighScore = currentScore;
                SceneManager.LoadScene("ChooseLevel");
            }

            if (listBird.Length == 0) {
                Debug.Log("Lose");
                currentScore = 0;
                SceneManager.LoadScene("ChooseLevel");
            }
        
        }
        if (crossBow.GetComponent<CrossBow>().SelectedBird == null) {
            CrossBow crossBowComponent = crossBow.GetComponent<CrossBow>();
            GameObject bird = GetClosestBird(listBird, crossBow);
            crossBowComponent.SelectedBird = bird;
            crossBowComponent.CrossBowState = CrossBow.CrossBowStateEnum.Idle;
            GameObject mainCamera = GameObject.FindGameObjectsWithTag("MainCamera")[0];
            mainCamera.GetComponent<CameraFollow>().BirdToFollow = bird;
        }   
    }

    public GameObject GetClosestBird(GameObject[] Bird, GameObject crossBow)
    {
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = crossBow.transform.position;
        foreach (GameObject t in Bird)
        {
            float dist = Vector3.Distance(t.transform.position, currentPos);
            if (dist < minDist)
            {
                tMin = t;
                minDist = dist;
            }
        }
        return tMin;
    }
}
