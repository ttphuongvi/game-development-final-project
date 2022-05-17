using UnityEngine;
using UnityEngine.UIElements;

public class IngameMenu : MonoBehaviour
{
    public VisualElement root;

    public Button btnPause, btnReturn;
    // Start is called before the first frame update
    void OnEnable()
    {
        root = GetComponent<UIDocument>().rootVisualElement;
        btnPause = root.Q<Button>("btnPause");
        // btnPause.clicked += OnPauseClicked;

        btnReturn = root.Q<Button>("btnReturn");
        // btnReturn += OnReturnClicked;
    }

    void OnPauseClicked()
    {
        Debug.Log("Clicked");
    }

    void OnReturnClicked()
    {
        Debug.Log("Clicked");
    }

}
