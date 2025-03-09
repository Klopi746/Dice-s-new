using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class ContinueButtSCRIPT : MonoBehaviour
{
    private Button button;
    public static ContinueButtSCRIPT Instance;
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
        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        button.interactable = false;
        PlayerSCRIPT.Instance.ContinuePlay();
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
