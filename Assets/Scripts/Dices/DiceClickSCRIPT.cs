using System.Collections;
using DG.Tweening;
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

        startPos = transform.position;
    }


    public bool enemyDice = false;
    public bool wasClicked = false;
    public void OnPointerClick(PointerEventData eventData)
    {
        if (enemyDice) return;

        if (!PlayerSCRIPT.Instance.canClickCubes) return;

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
            this.enabled = false;
            StartCoroutine(PutCubeAside());
        }
        wasClicked = false;
    }
    private Vector3 startPos;
    private IEnumerator PutCubeAside()
    {
        Tween tween = (GameHandlerSCRIPT.Instance.IsPlayerTurn) ? transform.DOLocalJump(new Vector3(transform.position.x, 0, -10), 10f, 1, 0.5f) : transform.DOLocalJump(new Vector3(transform.position.x, 0, 10), 10f, 1, 0.5f);
        yield return tween.WaitForCompletion();
        gameObject.SetActive(false);
    }
    public IEnumerator PutCubeAsideOnTurnEnd()
    {
        if (!wasClicked) yield break;
        Tween tween = (GameHandlerSCRIPT.Instance.IsPlayerTurn) ? transform.DOLocalJump(new Vector3(transform.position.x, -2, -10), 10f, 1, 0.5f) : transform.DOLocalJump(new Vector3(transform.position.x, -2, 10), 10f, 1, 0.5f);
        yield return tween.WaitForCompletion();
    }
    private void OnEnable()
    {
        transform.position = startPos;
    }
}
