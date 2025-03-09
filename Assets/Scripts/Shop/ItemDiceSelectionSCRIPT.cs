using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemDiceSelectionSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public int diceId;


    [SerializeField] Image ImageComponent;
    public void OnPointerClick(PointerEventData eventData)
    {
        bool check = CheckThatCanAssign();
        if (check)
        {
            DiceSelectorSCRIPT.Instance.DisableAll();
            ImageComponent.color = Color.yellow;
            PlayerPrefs.SetInt("DiceInSlot" + DiceSelectorSCRIPT.Instance.curSlotId, diceId);
            ShopManagerSCRIPT.Instance.UpdateSlotDice(DiceSelectorSCRIPT.Instance.curSlotId);
            GeneralSoundManagerSCRIPT.Instance.PlayShopClickSound();
        }
        else ShowError();
    }
    public bool CheckThatCanAssign()
    {
        int amount = ShopManagerSCRIPT.Instance.CheckAmountOfAssignDiceId(diceId);
        int buyedAmount = ShopManagerSCRIPT.Instance.CheckAmountOfBuyedDiceId(diceId);
        if (amount >= buyedAmount) return false;
        else return true;
    }

    private Coroutine routine;
    private void ShowError()
    {
        if (routine != null) return;
        routine = StartCoroutine(ShowErrorRutine());
    }
    private IEnumerator ShowErrorRutine()
    {
        Color startColor = ImageComponent.color;
        Tween tween = ImageComponent.DOColor(Color.red, 0.4f);
        yield return tween.WaitForCompletion();
        tween = ImageComponent.DOColor(startColor, 0.4f);
        yield return tween.WaitForCompletion();
        routine = null;
    }
}
