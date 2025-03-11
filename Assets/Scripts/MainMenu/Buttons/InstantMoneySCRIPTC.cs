using UnityEngine;
using UnityEngine.UI;

public class InstantMoneySCRIPTC : MonoBehaviour
{
    private Button button;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);
    }


    public void OnButtonClicked()
    {
        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        int curLives = MainMenuManagerSCRIPT.Instance.Lives;
        MainMenuManagerSCRIPT.Instance.UpdateLivesTo(curLives + 100);
    }
}
