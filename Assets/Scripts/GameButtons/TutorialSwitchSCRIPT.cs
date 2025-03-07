using UnityEngine;
using UnityEngine.UI;

public class TutorialSwitchSCRIPT : MonoBehaviour
{
    [SerializeField] Transform trans1;
    [SerializeField] Transform trans2;
    [SerializeField] Button button;
    private void Awake()
    {
        button.onClick.AddListener(OnButtonClicked);
    }


    public void OnButtonClicked()
    {
        GeneralSoundManagerSCRIPT.Instance.PlayButtSound();
        trans1.gameObject.SetActive(!trans1.gameObject.activeSelf);
        trans2.gameObject.SetActive(!trans2.gameObject.activeSelf);
    }
}
