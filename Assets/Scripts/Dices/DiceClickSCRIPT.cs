using UnityEngine;
using UnityEngine.EventSystems;

public class DiceClickSCRIPT : MonoBehaviour, IPointerClickHandler
{
    public OutlineSCRIPT outlineScript;

    void Awake()
    {
        outlineScript = GetComponent<OutlineSCRIPT>();
        if (outlineScript == null) Debug.LogWarning($"{this.name} can't find outline script!");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (!outlineScript.enabled) outlineScript.enabled = true;
        else outlineScript.enabled = false;
    }
}
