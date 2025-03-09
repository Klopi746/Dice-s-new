using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LogoClickSCRIPT : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] Image imageComp;


    public void OnPointerClick(PointerEventData eventData)
    {
        if (imageComp.fillAmount > 0.1f)
        {
            imageComp.fillAmount -= 0.2f;
            int curLives = MainMenuManagerSCRIPT.Instance.Lives;
            MainMenuManagerSCRIPT.Instance.UpdateLivesTo(curLives + 2);
            GeneralSoundManagerSCRIPT.Instance.PlayBuySound();
        }
    }

    private bool moved = false;
    private Vector3 startPos;
    public void OnPlayButtonClick()
    {
        transform.DOComplete();

        if (moved == false)
        {
            imageComp.raycastTarget = false;

            startPos = transform.localPosition;
            transform.DOLocalMove(new Vector3(0, -300, 0), 0.75f);
        }
        else
        {
            imageComp.raycastTarget = true;

            transform.DOLocalMove(startPos, 0.4f);
        }
        moved = !moved;
    }
}
