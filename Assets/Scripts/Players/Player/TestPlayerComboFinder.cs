using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class TestPlayerComboFinder : MonoBehaviour
{
    public Dictionary<string, int> FindAllCombos(Dictionary<int, int> diceValues)
    {
        List<int> values = new List<int>(6);
        foreach (var item in diceValues)
        {
            values.Add(item.Value);
        }

        values.Sort();

        string valuesStr = string.Join("", values);

        int[] valuesCount = new int[6];
        for (int i = 1; i <= valuesStr.Length; i++)
        {
            valuesCount[i - 1] = valuesStr.Count(x => x == i.ToString()[0]);
        }
        for (int i = 0; i < valuesCount.Length; i++)
        {
            if (valuesCount[i] >= 3)
            {
                Debug.Log($"Combo: {string.Concat(Enumerable.Repeat((i + 1).ToString(), valuesCount[i]))}");
            }
        }

        Dictionary<string, int> foundCombos = new Dictionary<string, int>(){
            { "5", 50 }, { "1", 100 }, { "15", 150 }
        };

        return foundCombos;
    }
}