using UnityEngine;
using UnityEngine.UI;

public class ShopBuyButtSCRIPT : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }


    public void OnButtonClicked()
    {
        GeneralSoundManagerSCRIPT.Instance.PlayBuySound();
    }
}
