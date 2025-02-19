using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EndTurnButtSCRIPT : MonoBehaviour, IPointerClickHandler
{
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
    }
    private void Start()
    {
        GameHandlerSCRIPT.Instance.OnTurnChanged.AddListener(HandleTurnChange);
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        button.interactable = false;
        GameHandlerSCRIPT.Instance.EndTurn();
    }


    private void HandleTurnChange(bool isPlayerTurn)
    {
        if (isPlayerTurn) button.interactable = true;
    }
}
