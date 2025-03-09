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
}