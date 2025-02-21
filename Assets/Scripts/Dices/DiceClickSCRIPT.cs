using UnityEngine;
using UnityEngine.EventSystems;

public class DiceClickSCRIPT : MonoBehaviour, IPointerClickHandler
{
    private SpriteRenderer clickedSpriteRenderer;
    void Awake()
    {
        clickedSpriteRenderer = transform.Find("ClickedSprite").GetComponent<SpriteRenderer>();
        if (clickedSpriteRenderer == null) Debug.Log($"{this.name} can't find clickedSprite");
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        clickedSpriteRenderer.transform.rotation = Quaternion.Euler(90, 0, 0);
        if (!clickedSpriteRenderer.enabled) clickedSpriteRenderer.enabled = true;
        else clickedSpriteRenderer.enabled = false;
    }
}
