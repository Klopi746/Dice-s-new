using System.Collections;
using DG.Tweening;
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


    private void OnEnable()
    {
        transform.localScale = new Vector3(0.1f, 0.1f, 0.1f);
        StartCoroutine(AnimateEnableCoroutine());
    }
    private IEnumerator AnimateEnableCoroutine()
    {
        Tween tween = transform.DORotate(new Vector3(0, 360, 0), 1f, RotateMode.LocalAxisAdd);
        tween = transform.DOScale(new Vector3(1, 1, 1), 1f);
        yield return tween.WaitForCompletion();
    }
}
