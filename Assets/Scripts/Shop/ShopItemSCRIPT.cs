using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(RectTransform))]
public class ShopItemSCRIPT : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
    [Header("Hover Expansion Settings")]
    [SerializeField] private float horizontalExpansionPercent = 10f;
    [SerializeField] private float verticalExpansionPercent = 10f;
    [SerializeField] private float animationDuration = 0.2f;

    [Header("Selected Expansion Settings")]
    [SerializeField] private float selectedHorizontalPercent = 20f;
    [SerializeField] private float selectedVerticalPercent = 20f;

    [Header("ShopVisualization")]
    [SerializeField] private DiceData diceInfo;
    [SerializeField] private TextMeshProUGUI diceName;
    [SerializeField] private TextMeshProUGUI dicePrice;
    [SerializeField] Image diceIcon;
    [SerializeField] List<Image> diceSideIcons;
    [SerializeField] List<TextMeshProUGUI> diceSideChances;
    [SerializeField] Image diceIconOnSelection;
    [SerializeField] TextMeshProUGUI curAmountOfDice;


    private Vector2 originalSize;
    private Vector2 expandedSize;
    private Coroutine resizeCoroutine;
    private RectTransform rectTransform;
    private bool isSelected = false;

    private void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        originalSize = rectTransform.sizeDelta;
        expandedSize = new Vector2(
            originalSize.x * (1 + horizontalExpansionPercent / 100f),
            originalSize.y * (1 + verticalExpansionPercent / 100f)
        );

        ShopManagerSCRIPT.Instance.RegisterItem(this);
        Visualize();
    }

    public void Visualize()
    {
        diceIcon.sprite = diceInfo.diceIcon;
        dicePrice.text = diceInfo.dicePrice.ToString();
        diceName.text = diceInfo.diceName;

        for (int i = 0; i < diceSideIcons.Count; i++)
        {
            diceSideIcons[i].sprite = ShopManagerSCRIPT.Instance.diceSideIcons[diceInfo.sidesList[i]-1];
            diceSideChances[i].text = diceInfo.probabilityList[i].ToString();
        }

        diceIconOnSelection.sprite = diceInfo.diceIcon;
        curAmountOfDice.text = PlayerPrefs.GetInt("Dice"+ diceInfo.diceName+"InInventory",0).ToString();
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        if (!isSelected)
         StartResize(expandedSize);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (!isSelected)
            StartResize(originalSize);
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        ShopManagerSCRIPT.Instance.SelectItem(this);
    }

    public void SetSelected(bool selected)
    {
        isSelected = selected;
        if (selected)
        {
            Vector2 selectedSize = new Vector2(
                originalSize.x * (1 + selectedHorizontalPercent / 100f),
                originalSize.y * (1 + selectedVerticalPercent / 100f)
            );
            StartResize(selectedSize);
        }
        else
        {
            StartResize(originalSize);
        }
    }

    public void DisableHover()
    {
        isSelected = true;
    }

    private void StartResize(Vector2 newSize)
    {
        if (resizeCoroutine != null)
            StopCoroutine(resizeCoroutine);
        resizeCoroutine = StartCoroutine(ResizeRoutine(newSize));
    }

    private System.Collections.IEnumerator ResizeRoutine(Vector2 newSize)
    {
        Vector2 initialSize = rectTransform.sizeDelta;
        float elapsedTime = 0f;
        while (elapsedTime < animationDuration)
        {
            rectTransform.sizeDelta = Vector2.Lerp(initialSize, newSize, elapsedTime / animationDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        rectTransform.sizeDelta = newSize;
    }

    private void OnDisable()
    {
        rectTransform.sizeDelta = originalSize;
    }
}