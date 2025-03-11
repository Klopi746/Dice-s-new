using System.Collections;
using DG.Tweening;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class HideSliderButtSCRIPT : MonoBehaviour
{
    private Button button;
    private bool wasHidden = false;
    [SerializeField] GameObject slider1;
    [SerializeField] GameObject slider2;
    private void Awake()
    {
        button = gameObject.GetComponent<Button>();
        button.onClick.AddListener(OnButtonClicked);

        wasHidden = PlayerPrefs.GetInt("VolumeSliderHide", 0) == 1 ? true : false;
        if (wasHidden)
        {
            slider1.SetActive(false);
            slider2.SetActive(false);
            image.color = secondColor;
        }
    }


    private Coroutine coroutine = null;
    public void OnButtonClicked()
    {
        if (coroutine != null) return;
        coroutine = StartCoroutine(ButtClickRoutine());
    }

    [SerializeField] Image image;
    private Color startColor = new Color(168 / 255f, 144 / 255f, 85 / 255f, 255 / 255f);
    private Color secondColor = new(67 / 255f, 61 / 255f, 50 / 255f, 255 / 255f);
    private IEnumerator ButtClickRoutine()
    {
        wasHidden = PlayerPrefs.GetInt("VolumeSliderHide", 0) == 1 ? true : false;
        if (!wasHidden)
        {
            PlayerPrefs.SetInt("VolumeSliderHide", 1);
            slider1.SetActive(false);
            slider2.SetActive(false);
            yield return StartCoroutine(ChangeColorRoutine(secondColor));
        }
        else
        {
            PlayerPrefs.SetInt("VolumeSliderHide", 0);
            slider1.SetActive(true);
            slider2.SetActive(true);
            yield return StartCoroutine(ChangeColorRoutine(startColor));
        }
        coroutine = null;
    }
    private IEnumerator ChangeColorRoutine(Color color)
    {
        Tween tween = transform.DORotate(new Vector3(90, 0, 0), 0.5f);
        yield return tween.WaitForCompletion();
        image.DOColor(color, 0);
        tween = transform.DORotate(new Vector3(0, 0, 0), 0.5f);
        yield return tween.WaitForCompletion();
    }
}
