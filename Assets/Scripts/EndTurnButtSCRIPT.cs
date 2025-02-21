using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButtSCRIPT : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }
    private void Start()
    {
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }


    public void OnButtonClicked()
    {
        button.interactable = false;
        GameHandlerSCRIPT.Instance.EndTurn();
    }


    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (isPlayerTurn) button.interactable = true;
    }
}
