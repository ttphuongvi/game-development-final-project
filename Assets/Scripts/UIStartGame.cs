using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.SceneManagement;

public class UIStartGame : MonoBehaviour
{
    [HideInInspector]
    public VisualElement root;
    [HideInInspector]
    public Button BtnPlay;
    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        BtnPlay = root.Q<Button>("btn_play");
        BtnPlay.clicked += OnPlayClicked;

    }

    void OnPlayClicked() {
        SceneManager.LoadScene("ChooseLevel");
    }

}
