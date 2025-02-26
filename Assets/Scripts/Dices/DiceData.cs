using UnityEngine;
using System.Collections.Generic;
//using AYellowpaper.SerializedCollections;

[CreateAssetMenu(fileName = "Dice", menuName = "Custom/Dice Data", order = 1)]
public class DiceData : ScriptableObject
{
    [Header("Game info")]
    public GameObject dicePref;
    /*
    [SerializedDictionary("Damage Type", "Description")]
    public SerializedDictionary<DamageType, string> ElementDescriptions;
    */
    [Header("Shop&Inventory")]
    public Sprite cubeIcon;
}
