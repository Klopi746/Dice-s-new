using UnityEngine;
using UnityEngine.UI;

public class EndTurnButtSCRIPT : MonoBehaviour
{
    private Button button;
    public static EndTurnButtSCRIPT Instance;
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
        ContinueButtSCRIPT.Instance.ChangeButtInteractable(false);
        GameHandlerSCRIPT.Instance.EndTurn();
    }


    private void HandleTurnChange(bool isPlayerTurn)
    {
        //if (isPlayerTurn) button.interactable = true; // Butt is changing in PlayerPapa.GetAndDropCubes();
    }


    public void ChangeButtInteractable(bool value)
    {
        button.interactable = value;
    }
}
