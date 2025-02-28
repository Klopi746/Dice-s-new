using UnityEngine;
using UnityEngine.EventSystems;

public class DiceClickSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public OutlineSCRIPT outlineScript;
    public DicePapaSCRIPT cubeGameScript;
    void Awake()
    {
        cubeGameScript = GetComponent<DicePapaSCRIPT>();

        outlineScript = GetComponent<OutlineSCRIPT>();
        if (outlineScript == null) Debug.LogWarning($"{this.name} can't find outline script!");
    }


    public bool enemyDice = false;
    public bool wasClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enemyDice) return;

        wasClicked = !wasClicked;


        outlineScript.enabled = !outlineScript.enabled;

        int AddOrRemoveValue = (wasClicked) ? 1 : -1;
        PlayerSCRIPT.Instance.UpdateClickedCubesDigits(cubeGameScript.CurrentNumber.ToString(), AddOrRemoveValue);
    }


    public void EnemyClick()
    {
        wasClicked = !wasClicked;

        outlineScript.enabled = !outlineScript.enabled;
        int AddOrRemoveValue = (wasClicked) ? 1 : -1;
        EnemySCRIPT.Instance.UpdateClickedCubesDigits(cubeGameScript.CurrentNumber.ToString(), AddOrRemoveValue);
    }


    public void TurnOffDiceOutline()
    {
        if (outlineScript.enabled) outlineScript.enabled = false;
        if (wasClicked)
        {
            if (!enemyDice) PlayerSCRIPT.Instance.UpdateClickedCubesDigits(cubeGameScript.CurrentNumber.ToString(), -1);
            if (enemyDice) EnemySCRIPT.Instance.UpdateClickedCubesDigits(cubeGameScript.CurrentNumber.ToString(), -1);
        }
    }
    public void DisableCubeOnContinueIfClicked()
    {
        if (wasClicked)
        {
            cubeGameScript.enabled = false;
            gameObject.SetActive(false);
        }
        wasClicked = false;
    }
}
