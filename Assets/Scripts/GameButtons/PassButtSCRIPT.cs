using UnityEngine;
using UnityEngine.UI;

public class PassButtSCRIPT : MonoBehaviour
{
    private Button button;
    public static PassButtSCRIPT Instance;
    private void Awake()
    {
        Instance = this;
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
        button.interactable = false;
    }
    private void Start()
    {
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }


    public void OnButtonClicked()
    {
        button.interactable = false;
        LoadSceneManagerSCRIPT.Instance.LoadNewScene();
    }


    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (isPlayerTurn) button.interactable = true;
        else button.interactable = false;
    }


    public void ChangeButtInteractable(bool value)
    {
        button.interactable = value;
    }
}
