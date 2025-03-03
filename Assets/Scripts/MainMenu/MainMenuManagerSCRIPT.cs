using DG.Tweening;
using TMPro;
using UnityEngine;

public class MainMenuManagerSCRIPT : MonoBehaviour
{
    public static MainMenuManagerSCRIPT Instance;


    [SerializeField] TextMeshProUGUI LivesTextPro;


    public int Lives = 40;
    private void Awake()
    {
        Instance = this;
        PlayerPrefs.SetInt("ChoosedBet", 10);
        Lives = PlayerPrefs.GetInt("Lives", 40);
        LivesTextPro.text = $"Lives: {Lives}";
    }


    public void ShowErrorOnLives()
    {
        LivesTextPro.transform.DOPunchScale(new Vector3(1, 1, 0), 1f, 4);
    }
}
