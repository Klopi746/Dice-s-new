using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class RealLivesManager : MonoBehaviour, IPointerClickHandler
{
    public static RealLivesManager Instance;


    public int RealLives = 10;
    [SerializeField] private Image[] LivesImages;
    private void Start()
    {
        Instance = this;

        RealLives = PlayerPrefs.GetInt("RealLives", 10);
        StartCoroutine(CheckRealLives());
    }
    public AudioClip LiveDieSound;
    public bool LivesShown = false;
    private IEnumerator CheckRealLives()
    {
        yield return new WaitForSeconds(0.2f);

        int length = LivesImages.Length;
        for (int i = 1; i <= LivesImages.Length; i++)
        {
            if (RealLives < (length + 1) - i)
            {
                Tween tween = LivesImages[length - i].DOColor(Color.black, 0.4f);
                AudioManager_SCRIPT.Instance.PlaySound(LiveDieSound, pitch: length + 1 - i);
                yield return tween.WaitForCompletion();
            }

        }
        LivesShown = true;
        MainMenuManagerSCRIPT.Instance.CheckWinLoose();
    }


    // REACTION TO PLAYER
    public void OnPointerClick(PointerEventData eventData)
    {
        transform.DOComplete();
        transform.DOPunchPosition(new Vector3(0, -1000, 0), 1f, 1);
        transform.DOPunchScale(new Vector3(2, 2, 1), 1f, 1);
    }
}
