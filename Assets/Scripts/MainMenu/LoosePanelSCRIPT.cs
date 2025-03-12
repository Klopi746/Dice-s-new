using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LoosePanelSCRIPT : MonoBehaviour
{
    [SerializeField] Image image;
    private void OnEnable()
    {
        StartCoroutine(AnimateEnableCoroutine());
    }
    private IEnumerator AnimateEnableCoroutine()
    {
        Tween tween = image.transform.DOScale(new Vector3(5, 5, 5), 3f);
        yield return new WaitForSeconds(2f);
        GeneralSoundManagerSCRIPT.Instance.PlayLooseSound();
        yield return tween.WaitForCompletion();
        yield return new WaitForSeconds(1f);
        LoadSceneManagerSCRIPT.Instance.LoadNewScene();
    }
}
