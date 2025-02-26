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


    public bool enemyDices = false;
    private bool wasClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enemyDices) return;

        wasClicked = !wasClicked;


        outlineScript.enabled = !outlineScript.enabled;

        int AddOrRemoveValue = (wasClicked) ? 1 : -1;
        PlayerSCRIPT.Instance.UpdateClickedCubesDigits(cubeGameScript.CurrentNumber.ToString(), AddOrRemoveValue);
    }


    public void EnemyClick()
    {
        outlineScript.enabled = !outlineScript.enabled;
    }
}
