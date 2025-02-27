using UnityEngine;
using System.Collections.Generic;

[CreateAssetMenu(fileName = "Dice", menuName = "Custom/Dice Data", order = 1)]
public class DiceData : ScriptableObject
{
    [Header("Game info")]
    public GameObject dicePref;

    public List<int> sidesList;
    public List<int> probabilityList;

    [Header("Shop&Inventory")]
    public string diceName;
    public Sprite diceIcon;
    public int dicePrice;
}
