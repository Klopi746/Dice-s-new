using TMPro;
using UnityEngine;

public class HintTextSCRIPT : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI hintTextPro;
    private void Start()
    {
        if (MainMenuManagerSCRIPT.Instance.firstStart == 1)
        {
            hintTextPro.enabled = true;
        }
    }
}