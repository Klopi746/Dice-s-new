using System.Collections.Generic;
using UnityEngine;

public class TestPlayerComboFinder : MonoBehaviour
{
    public Dictionary<string, int> FindAllCombos(Dictionary<int, int> diceValues)
    {
        string values = "";
        foreach (var item in diceValues)
        {
            values += item.Value.ToString();
        }
        Debug.Log(values);

        Dictionary<string, int> foundCombos = new Dictionary<string, int>(){
            { "5", 50 }, { "1", 100 }, { "15", 150 }
        };

        return foundCombos;
    }
}