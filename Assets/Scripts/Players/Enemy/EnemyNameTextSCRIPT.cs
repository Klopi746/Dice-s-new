using TMPro;
using UnityEngine;

public class EnemyNameTextSCRIPT : MonoBehaviour
{
    [SerializeField] TextMeshPro textMeshProComponent;
    private void OnMouseEnter()
    {
        textMeshProComponent.enabled = true;
    }
    private void OnMouseExit()
    {
        textMeshProComponent.enabled = false;
    }
}
