using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ComboFinder : MonoBehaviour
{
    public static ComboFinder Instance;
    void Awake()
    {
        Instance = this;
    }


    public Dictionary<string, int> FindAllCombos(Dictionary<int, int> diceValues)
    {
        Dictionary<string, int> foundCombos = new Dictionary<string, int>() { };


        List<int> values = new List<int>(6);
        foreach (var item in diceValues)
        {
            values.Add(item.Value);
        }

        Debug.Log(string.Join("", values));
        values.Sort();

        string valuesStr = string.Join("", values);
        // Find FullSequence
        bool noSequenceInValues = false;
        KeyValuePair<string, int> newpair = FindSequence(valuesStr);
        if (newpair.Value == 0) noSequenceInValues = true;
        else if (newpair.Key == "123456")
        {
            Dictionary<string, int> fullCombo = new Dictionary<string, int>() { { newpair.Key, newpair.Value } };
            // Debug.Log("End Test FULL!");
            return fullCombo;
        }
        else { foundCombos.Add(newpair.Key, newpair.Value); valuesStr = valuesStr.Replace(newpair.Key, ""); }
        // Debug.Log($"Combo Sequence: {newpair.Key} Value: {newpair.Value}");
        // Find All three or more & two or less
        int[] valuesCount = new int[6];
        for (int i = 1; i <= valuesStr.Length; i++)
        {
            valuesCount[i - 1] = valuesStr.Count(x => x == i.ToString()[0]);
        }

        int cubesUsed = 0;
        if (noSequenceInValues)
        {
            // Find All three or more
            for (int i = 0; i < valuesCount.Length; i++)
            {
                if (valuesCount[i] >= 3)
                {
                    cubesUsed += valuesCount[i];

                    string key = string.Concat(Enumerable.Repeat((i + 1).ToString(), valuesCount[i]));

                    int value;
                    if (FindComboInCombinations(key)) value = _cubesCombos[key];
                    else value = SumTheCombosLessThanThree(key, foundCombos);

                    // Debug.Log($"Combo: {key} Value: {value}");
                    foundCombos.Add(key, value);
                }
            }
            if (cubesUsed == values.Count)
            {
                Dictionary<string, int> fullCombo = SumToFullCombo(foundCombos);
                // Debug.Log("End Test FULL!");
                return fullCombo;
            }
        }
        // Find All two or less
        for (int i = 0; i < valuesCount.Length; i++)
        {
            if (valuesCount[i] > 0 && valuesCount[i] <= 2 && (i + 1 == 1 || i + 1 == 5))
            {

                cubesUsed += valuesCount[i];

                string key = string.Concat(Enumerable.Repeat((i + 1).ToString(), valuesCount[i]));

                int value = 0;
                if (FindComboInCombinations(key)) value = _cubesCombos[key];
                else value = SumTheCombosLessThanThree(key, foundCombos);

                if (value == 0) continue;
                // Debug.Log($"Combo: {key} Value: {value}");
                foundCombos.Add(key, value);
            }
        }
        if (cubesUsed == values.Count)
        {
            Dictionary<string, int> fullCombo = SumToFullCombo(foundCombos);
            // Debug.Log("End Test FULL!");
            return fullCombo;
        }

        if (foundCombos.Count > 1)
        {
            KeyValuePair<string, int> sumOfCombos = SumCombos(foundCombos);
            foundCombos.Add(sumOfCombos.Key, sumOfCombos.Value);
        }

        // Debug.Log("End Test!");
        return foundCombos;
    }
    private bool FindComboInCombinations(string key)
    {
        if (_cubesCombos.ContainsKey(key)) return true;
        else return false;
    }
    private int SumTheCombosLessThanThree(string Key, Dictionary<string, int> foundCombos)
    {
        if (Key.Length > 2) Debug.LogWarning("Error!");

        int value = 0;
        bool added = false; // to check that it's gooing second +
        foreach (var c in Key)
        {
            switch (c)
            {
                case '1':
                    value += 100;
                    break;
                case '5':
                    value += 50;
                    break;
            }
            if (!added && Key.Length == 2) { foundCombos.Add(Key[0].ToString(), value); itsTwoTheSame = true; }
            added = true;
        }
        return value;
    }
    private Dictionary<string, int> SumToFullCombo(Dictionary<string, int> foundCombos)
    {
        if (itsTwoTheSame) { string minKey = foundCombos.Keys.OrderBy(k => k.Length).First(); foundCombos.Remove(minKey); }


        Dictionary<string, int> fullCombo = new Dictionary<string, int>();
        string newKey = "";
        int newValue = 0;
        foreach (var combo in foundCombos)
        {
            newKey += combo.Key;
            newValue += combo.Value;
        }
        string sortedKey = new string(newKey.OrderBy(c => c).ToArray());
        fullCombo.Add(sortedKey, newValue);

        // Debug.Log($"Combo: {sortedKey} Value: {newValue}");
        return fullCombo;
    }
    bool itsTwoTheSame = false;
    private KeyValuePair<string, int> SumCombos(Dictionary<string, int> foundCombos)
    {
        if (itsTwoTheSame) { string minKey = foundCombos.Keys.OrderBy(k => k.Length).First(); foundCombos.Remove(minKey); }


        string newKey = "";
        int newValue = 0;
        foreach (var combo in foundCombos)
        {
            newKey += combo.Key;
            newValue += combo.Value;
        }
        string sortedKey = new string(newKey.OrderBy(c => c).ToArray());
        KeyValuePair<string, int> allComboPair = new KeyValuePair<string, int>(sortedKey, newValue);

        // Debug.Log($"Combo: {sortedKey} Value: {newValue}");
        return allComboPair;
    }
    private KeyValuePair<string, int> FindSequence(string valuesStr)
    {
        if (valuesStr.Contains("123456")) return new KeyValuePair<string, int>(valuesStr, 1500);
        else if (valuesStr.Contains("12345")) return new KeyValuePair<string, int>("12345", 500);
        else if (valuesStr.Contains("23456")) return new KeyValuePair<string, int>("23456", 750);
        else return new KeyValuePair<string, int>("0", 0);
    }


    //SANYA
    protected static readonly Dictionary<string, int> _cubesCombos = new Dictionary<string, int>(){
        {"1",100},
        {"5",50},
        {"111",1000},
        {"1111",2000},
        {"11111",4000},
        {"111111",8000},
        {"222",200},
        {"2222",400},
        {"22222",800},
        {"222222",1600},
        {"333",300},
        {"3333",600},
        {"33333",1200},
        {"333333",2400},
        {"444",400},
        {"4444",800},
        {"44444",1600},
        {"444444",3200},
        {"555",500},
        {"5555",1000},
        {"55555",2000},
        {"555555",4000},
        {"666",600},
        {"6666",1200},
        {"66666",2400},
        {"666666",4800},
        {"12345",500},
        {"23456",750},
        {"123456",1500}
    };
    private void FindCombinations(string s, List<string> substrings, List<string> currentCombination, int startIndex, List<List<string>> combinations)
    {
        if (startIndex == s.Length)
        {
            combinations.Add(new List<string>(currentCombination));
            return;
        }
        foreach (var substring in substrings)
        {
            if (s.Length - startIndex >= substring.Length && s.Substring(startIndex, substring.Length) == substring)
            {
                currentCombination.Add(substring);
                FindCombinations(s, substrings, currentCombination, startIndex + substring.Length, combinations);
                currentCombination.RemoveAt(currentCombination.Count - 1);
            }
        }
        if (startIndex < s.Length)
        {
            string remaining = s.Substring(startIndex, 1);
            currentCombination.Add($"Остаток: {remaining}");
            FindCombinations(s, substrings, currentCombination, startIndex + 1, combinations);
            currentCombination.RemoveAt(currentCombination.Count - 1);
        }
    }


    // Обратился к чуваку за помощью. Но решил оставить свой код)
    public Dictionary<string, int> FindAllCombosSanya(Dictionary<int, int> diceValues)
    {
        var sortedDictionary = diceValues.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
        List<int> boneNumbers = sortedDictionary.Keys.ToList();
        List<int> boneValues = sortedDictionary.Values.ToList();
        List<string> combinationsDefault = _cubesCombos.Keys.ToList();
        string value = "";
        foreach (var val in boneValues)
        {
            value += val.ToString();
        }
        List<List<string>> combinations = new List<List<string>>();
        FindCombinations(value, combinationsDefault, new List<string>(), 0, combinations);
        //Тут тоже потесть
        foreach (var combination in combinations)
        {
            foreach (var combination2 in combination)
            {
                Debug.Log(combination2 + " ");
            }
            Debug.Log("\n");
        }
        List<Dictionary<List<int>, int>> cubes_out = new List<Dictionary<List<int>, int>>();
        List<List<int>> cubes_in = new List<List<int>>();
        foreach (var list in combinations)
        {
            Dictionary<List<int>, int> pribavka_temp = new Dictionary<List<int>, int>();
            List<int> played_cubes_numbers_in_event = new List<int>();
            List<int> cubes_in_event = new List<int>();
            int winsum = 0;
            for (int i = 0; i < list.Count; i++)
            {





                if (list[i].IndexOf("Остаток") == -1)
                {
                    foreach (var s in _cubesCombos)
                    {
                        if (s.Key == list[i])
                        {

                            winsum = winsum + s.Value;
                            int counter = i;
                            for (int j = counter; j < counter + list[i].Length; j++)
                            {
                                played_cubes_numbers_in_event.Add(boneNumbers[j]);
                            }
                            // i = counter;

                        }
                    }



                }
                else
                {

                    cubes_in_event.Add(boneNumbers[i]);
                }


            }
            pribavka_temp.Add(played_cubes_numbers_in_event, winsum);
            cubes_in.Add(cubes_in_event);
            cubes_out.Add(pribavka_temp);


        }
        //тестиков наделай красиво брат!
        foreach (var a in cubes_in)
        {
            foreach (var b in a)
            {
                Debug.Log($"{b} ");
            }
            Debug.Log("\n");
        }
        Dictionary<string, int> cubesOutDictionary = new Dictionary<string, int>();
        for (int i = 0; i < cubes_out.Count; i++)
        {
            Debug.Log($"Словарь {i + 1}:");
            foreach (var kvp in cubes_out[i])
            {
                // Форматируем ключ (список int) в строку
                string keyList = string.Join("", kvp.Key);
                // Выводим ключ и значение
                Debug.Log($"  Ключ: {keyList}, Значение: {kvp.Value}");
                cubesOutDictionary.Add(keyList, kvp.Value);
            }
        }
        return cubesOutDictionary;
        //Если что,алгоритм с рекурсией придумала нейронка ,а не я.Это во-первых.Во-вторых,я подумал,что ты должжен давать возможность в некоторых случаях выбирать<широкого> выбора, поэтому
        //    в некоторых ситуациях будет происходить такое, что игрок выбирает костей сколько хочет.Может быть я не знаю правила(по типу того, что если есть более крупная комбинация, он обязан! ее выбрать);



    }
}