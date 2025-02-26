using System;
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


    /// <summary>
    /// Функция принимает словарь с кубиками (ключ – номер объекта, значение – число на кубике)
    /// и возвращает все комбинации из _cubesCombos, которые можно составить из данных чисел.
    /// Результат сортируется по возрастанию цены комбинации.
    /// </summary>
    /// <param name="diceValues">Словарь значений на кубиках</param>
    /// <returns>Словарь найденных комбинаций: ключ – комбинация (отсортированные цифры), значение – сумма</returns>
    public Dictionary<string, int> FindAllCombos(Dictionary<int, int> diceValues)
    {
        // Формируем словарь частот для цифр, полученных с кубиков
        Dictionary<char, int> diceFreq = new Dictionary<char, int>();
        foreach (var value in diceValues.Values)
        {
            string digitStr = value.ToString();
            foreach (char c in digitStr)
            {
                if (diceFreq.ContainsKey(c))
                    diceFreq[c]++;
                else
                    diceFreq[c] = 1;
            }
        }


        // Список валидных комбинаций
        List<KeyValuePair<string, int>> validCombos = new List<KeyValuePair<string, int>>();


        // Проходим по всем комбинациям из _cubesCombos
        foreach (var combo in _cubesCombos)
        {
            string comboKey = combo.Key;
            // Берем словарь частот для цифр комбинации
            Dictionary<char, int> comboFreq = _cubesCombosFreq[comboKey];


            // Проверяем, хватает ли цифр в diceFreq для формирования комбинации
            bool isValid = true;
            foreach (var kvp in comboFreq)
            {
                char digit = kvp.Key;
                int required = kvp.Value;
                int available = diceFreq.ContainsKey(digit) ? diceFreq[digit] : 0;
                if (available < required)
                {
                    isValid = false;
                    break;
                }
            }

            // Если хватает
            if (isValid)
            {
                validCombos.Add(new KeyValuePair<string, int>(comboKey, combo.Value));
            }
        }

        // Сортируем найденные комбинации по возрастанию их цены.
        var sortedCombos = validCombos.OrderBy(x => x.Value)
                                       .ThenBy(x => x.Key) // при равных ценах сортировка по ключу
                                       .ToDictionary(x => x.Key, x => x.Value);

        return sortedCombos;
    }


    private static readonly Dictionary<string, int> _cubesCombos = new Dictionary<string, int>(){
        {"1",100},
        {"11",200},
        {"111",1000},
        {"1111",2000},
        {"11111",4000},
        {"111111",8000},
        {"111115",4050},
        {"11115",2050},
        {"111155",2100},
        {"111222",1200},
        {"111333",1300},
        {"111444",1400},
        {"1115",1050},
        {"11155",1100},
        {"111555",1500},
        {"111666",1600},
        {"11222",400},
        {"112222",600},
        {"112225",450},
        {"112345",600},
        {"11333",500},
        {"113333",800},
        {"113335",550},
        {"11444",600},
        {"114444",1000},
        {"114445",650},
        {"115",250},
        {"1155",300},
        {"11555",700},
        {"115555",1200},
        {"115666",850},
        {"11666",800},
        {"116666",1400},
        {"1222",300},
        {"12222",500},
        {"122222",900},
        {"122225",550},
        {"12225",350},
        {"122255",400},
        {"12345",500},
        {"123455",550},
        {"123456",1500},
        {"1333",400},
        {"13333",700},
        {"133333",1300},
        {"133335",750},
        {"13335",450},
        {"133355",500},
        {"1444",500},
        {"14444",900},
        {"144444",1700},
        {"144445",950},
        {"14445",550},
        {"144455",600},
        {"15",150},
        {"155",200},
        {"1555",600},
        {"15555",1100},
        {"155555",2100},
        {"155666",800},
        {"15666",750},
        {"156666",1350},
        {"1666",700},
        {"16666",1300},
        {"166666",2500},
        {"222",200},
        {"2222",400},
        {"22222",800},
        {"222222",1600},
        {"222225",850},
        {"22225",450},
        {"222255",500},
        {"222333",500},
        {"222444",600},
        {"2225",250},
        {"22255",300},
        {"222555",700},
        {"222666",800},
        {"234556",800},
        {"23456",750},
        {"333",300},
        {"3333",600},
        {"33333",1200},
        {"333333",2400},
        {"333335",1250},
        {"33335",650},
        {"333355",700},
        {"333444",700},
        {"3335",350},
        {"33355",400},
        {"333555",800},
        {"333666",900},
        {"444",400},
        {"4444",800},
        {"44444",1600},
        {"444444",3200},
        {"444445",1650},
        {"44445",850},
        {"444455",900},
        {"4445",450},
        {"44455",500},
        {"444555",900},
        {"444666",1000},
        {"5",50},
        {"55",100},
        {"555",500},
        {"5555",1000},
        {"55555",2000},
        {"555555",4000},
        {"555666",1100},
        {"55666",700},
        {"556666",1300},
        {"5666",650},
        {"56666",1250},
        {"566666",2450},
        {"666",600},
        {"6666",1200},
        {"66666",2400},
        {"666666",4800},
    };


    private static readonly Dictionary<string, Dictionary<char, int>> _cubesCombosFreq = new Dictionary<string, Dictionary<char, int>>()
    {
        {"1", new Dictionary<char, int>{{'1',1}}},
        {"11", new Dictionary<char, int>{{'1',2}}},
        {"111", new Dictionary<char, int>{{'1',3}}},
        {"1111", new Dictionary<char, int>{{'1',4}}},
        {"11111", new Dictionary<char, int>{{'1',5}}},
        {"111111", new Dictionary<char, int>{{'1',6}}},
        {"111115", new Dictionary<char, int>{{'1',5}, {'5',1}}},
        {"11115", new Dictionary<char, int>{{'1',4}, {'5',1}}},
        {"111155", new Dictionary<char, int>{{'1',4}, {'5',2}}},
        {"111222", new Dictionary<char, int>{{'1',3}, {'2',3}}},
        {"111333", new Dictionary<char, int>{{'1',3}, {'3',3}}},
        {"111444", new Dictionary<char, int>{{'1',3}, {'4',3}}},
        {"1115", new Dictionary<char, int>{{'1',3}, {'5',1}}},
        {"11155", new Dictionary<char, int>{{'1',3}, {'5',2}}},
        {"111555", new Dictionary<char, int>{{'1',3}, {'5',3}}},
        {"111666", new Dictionary<char, int>{{'1',3}, {'6',3}}},
        {"11222", new Dictionary<char, int>{{'1',2}, {'2',3}}},
        {"112222", new Dictionary<char, int>{{'1',2}, {'2',4}}},
        {"112225", new Dictionary<char, int>{{'1',2}, {'2',3}, {'5',1}}},
        {"112345", new Dictionary<char, int>{{'1',2}, {'2',1}, {'3',1}, {'4',1}, {'5',1}}},
        {"11333", new Dictionary<char, int>{{'1',2}, {'3',3}}},
        {"113333", new Dictionary<char, int>{{'1',2}, {'3',4}}},
        {"113335", new Dictionary<char, int>{{'1',2}, {'3',3}, {'5',1}}},
        {"11444", new Dictionary<char, int>{{'1',2}, {'4',3}}},
        {"114444", new Dictionary<char, int>{{'1',2}, {'4',4}}},
        {"114445", new Dictionary<char, int>{{'1',2}, {'4',3}, {'5',1}}},
        {"115", new Dictionary<char, int>{{'1',2}, {'5',1}}},
        {"1155", new Dictionary<char, int>{{'1',2}, {'5',2}}},
        {"11555", new Dictionary<char, int>{{'1',2}, {'5',3}}},
        {"115555", new Dictionary<char, int>{{'1',2}, {'5',4}}},
        {"115666", new Dictionary<char, int>{{'1',2}, {'5',1}, {'6',3}}},
        {"11666", new Dictionary<char, int>{{'1',2}, {'6',3}}},
        {"116666", new Dictionary<char, int>{{'1',2}, {'6',4}}},
        {"1222", new Dictionary<char, int>{{'1',1}, {'2',3}}},
        {"12222", new Dictionary<char, int>{{'1',1}, {'2',4}}},
        {"122222", new Dictionary<char, int>{{'1',1}, {'2',5}}},
        {"122225", new Dictionary<char, int>{{'1',1}, {'2',4}, {'5',1}}},
        {"12225", new Dictionary<char, int>{{'1',1}, {'2',3}, {'5',1}}},
        {"122255", new Dictionary<char, int>{{'1',1}, {'2',3}, {'5',2}}},
        {"12345", new Dictionary<char, int>{{'1',1}, {'2',1}, {'3',1}, {'4',1}, {'5',1}}},
        {"123455", new Dictionary<char, int>{{'1',1}, {'2',1}, {'3',1}, {'4',1}, {'5',2}}},
        {"123456", new Dictionary<char, int>{{'1',1}, {'2',1}, {'3',1}, {'4',1}, {'5',1}, {'6',1}}},
        {"1333", new Dictionary<char, int>{{'1',1}, {'3',3}}},
        {"13333", new Dictionary<char, int>{{'1',1}, {'3',4}}},
        {"133333", new Dictionary<char, int>{{'1',1}, {'3',5}}},
        {"133335", new Dictionary<char, int>{{'1',1}, {'3',4}, {'5',1}}},
        {"13335", new Dictionary<char, int>{{'1',1}, {'3',3}, {'5',1}}},
        {"133355", new Dictionary<char, int>{{'1',1}, {'3',3}, {'5',2}}},
        {"1444", new Dictionary<char, int>{{'1',1}, {'4',3}}},
        {"14444", new Dictionary<char, int>{{'1',1}, {'4',4}}},
        {"144444", new Dictionary<char, int>{{'1',1}, {'4',5}}},
        {"144445", new Dictionary<char, int>{{'1',1}, {'4',4}, {'5',1}}},
        {"14445", new Dictionary<char, int>{{'1',1}, {'4',3}, {'5',1}}},
        {"144455", new Dictionary<char, int>{{'1',1}, {'4',3}, {'5',2}}},
        {"15", new Dictionary<char, int>{{'1',1}, {'5',1}}},
        {"155", new Dictionary<char, int>{{'1',1}, {'5',2}}},
        {"1555", new Dictionary<char, int>{{'1',1}, {'5',3}}},
        {"15555", new Dictionary<char, int>{{'1',1}, {'5',4}}},
        {"155555", new Dictionary<char, int>{{'1',1}, {'5',5}}},
        {"155666", new Dictionary<char, int>{{'1',1}, {'5',2}, {'6',3}}},
        {"15666", new Dictionary<char, int>{{'1',1}, {'5',1}, {'6',3}}},
        {"156666", new Dictionary<char, int>{{'1',1}, {'5',1}, {'6',4}}},
        {"1666", new Dictionary<char, int>{{'1',1}, {'6',3}}},
        {"16666", new Dictionary<char, int>{{'1',1}, {'6',4}}},
        {"166666", new Dictionary<char, int>{{'1',1}, {'6',5}}},
        {"222", new Dictionary<char, int>{{'2',3}}},
        {"2222", new Dictionary<char, int>{{'2',4}}},
        {"22222", new Dictionary<char, int>{{'2',5}}},
        {"222222", new Dictionary<char, int>{{'2',6}}},
        {"222225", new Dictionary<char, int>{{'2',5}, {'5',1}}},
        {"22225", new Dictionary<char, int>{{'2',4}, {'5',1}}},
        {"222255", new Dictionary<char, int>{{'2',4}, {'5',2}}},
        {"222333", new Dictionary<char, int>{{'2',3}, {'3',3}}},
        {"222444", new Dictionary<char, int>{{'2',3}, {'4',3}}},
        {"2225", new Dictionary<char, int>{{'2',3}, {'5',1}}},
        {"22255", new Dictionary<char, int>{{'2',3}, {'5',2}}},
        {"222555", new Dictionary<char, int>{{'2',3}, {'5',3}}},
        {"222666", new Dictionary<char, int>{{'2',3}, {'6',3}}},
        {"234556", new Dictionary<char, int>{{'2',1}, {'3',1}, {'4',1}, {'5',2}, {'6',1}}},
        {"23456", new Dictionary<char, int>{{'2',1}, {'3',1}, {'4',1}, {'5',1}, {'6',1}}},
        {"333", new Dictionary<char, int>{{'3',3}}},
        {"3333", new Dictionary<char, int>{{'3',4}}},
        {"33333", new Dictionary<char, int>{{'3',5}}},
        {"333333", new Dictionary<char, int>{{'3',6}}},
        {"333335", new Dictionary<char, int>{{'3',5}, {'5',1}}},
        {"33335", new Dictionary<char, int>{{'3',4}, {'5',1}}},
        {"333355", new Dictionary<char, int>{{'3',4}, {'5',2}}},
        {"333444", new Dictionary<char, int>{{'3',3}, {'4',3}}},
        {"3335", new Dictionary<char, int>{{'3',3}, {'5',1}}},
        {"33355", new Dictionary<char, int>{{'3',3}, {'5',2}}},
        {"333555", new Dictionary<char, int>{{'3',3}, {'5',3}}},
        {"333666", new Dictionary<char, int>{{'3',3}, {'6',3}}},
        {"444", new Dictionary<char, int>{{'4',3}}},
        {"4444", new Dictionary<char, int>{{'4',4}}},
        {"44444", new Dictionary<char, int>{{'4',5}}},
        {"444444", new Dictionary<char, int>{{'4',6}}},
        {"444445", new Dictionary<char, int>{{'4',5}, {'5',1}}},
        {"44445", new Dictionary<char, int>{{'4',4}, {'5',1}}},
        {"444455", new Dictionary<char, int>{{'4',4}, {'5',2}}},
        {"4445", new Dictionary<char, int>{{'4',3}, {'5',1}}},
        {"44455", new Dictionary<char, int>{{'4',3}, {'5',2}}},
        {"444555", new Dictionary<char, int>{{'4',3}, {'5',3}}},
        {"444666", new Dictionary<char, int>{{'4',3}, {'6',3}}},
        {"5", new Dictionary<char, int>{{'5',1}}},
        {"55", new Dictionary<char, int>{{'5',2}}},
        {"555", new Dictionary<char, int>{{'5',3}}},
        {"5555", new Dictionary<char, int>{{'5',4}}},
        {"55555", new Dictionary<char, int>{{'5',5}}},
        {"555555", new Dictionary<char, int>{{'5',6}}},
        {"555666", new Dictionary<char, int>{{'5',3}, {'6',3}}},
        {"55666", new Dictionary<char, int>{{'5',2}, {'6',3}}},
        {"556666", new Dictionary<char, int>{{'5',2}, {'6',4}}},
        {"5666", new Dictionary<char, int>{{'5',1}, {'6',3}}},
        {"56666", new Dictionary<char, int>{{'5',1}, {'6',4}}},
        {"566666", new Dictionary<char, int>{{'5',1}, {'6',5}}},
        {"666", new Dictionary<char, int>{{'6',3}}},
        {"6666", new Dictionary<char, int>{{'6',4}}},
        {"66666", new Dictionary<char, int>{{'6',5}}},
        {"666666", new Dictionary<char, int>{{'6',6}}}
    };


    // MY GOVNO Code
    // public Dictionary<string, int> FindAllCombosD(Dictionary<int, int> diceValues)
    // {
    //     Dictionary<string, int> foundCombos = new Dictionary<string, int>() { };


    //     List<int> values = new List<int>(6);
    //     foreach (var item in diceValues)
    //     {
    //         values.Add(item.Value);
    //     }

    //     Debug.Log(string.Join("", values));
    //     values.Sort();

    //     List<int> valuesDistinct = values.Distinct().ToList();
    //     valuesDistinct.Sort();
    //     string valuesStr = string.Join("", values);
    //     // Find FullSequence
    //     bool noSequenceInValues = false;
    //     KeyValuePair<string, int> newpair = FindSequence(valuesStr);
    //     if (newpair.Value == 0) noSequenceInValues = true;
    //     else if (newpair.Key == "123456")
    //     {
    //         Dictionary<string, int> fullCombo = new Dictionary<string, int>() { { newpair.Key, newpair.Value } };
    //         // Debug.Log("End Test FULL!");
    //         return fullCombo;
    //     }
    //     else { foundCombos.Add(newpair.Key, newpair.Value); valuesStr = valuesStr.Replace(newpair.Key, ""); }
    //     // Debug.Log($"Combo Sequence: {newpair.Key} Value: {newpair.Value}");
    //     // Find All three or more & two or less
    //     int[] valuesCount = new int[6];
    //     for (int i = 1; i <= valuesStr.Length; i++)
    //     {
    //         valuesCount[i - 1] = valuesStr.Count(x => x == i.ToString()[0]);
    //     }

    //     int cubesUsed = 0;
    //     if (noSequenceInValues)
    //     {
    //         // Find All three or more
    //         for (int i = 0; i < valuesCount.Length; i++)
    //         {
    //             if (valuesCount[i] >= 3)
    //             {
    //                 cubesUsed += valuesCount[i];

    //                 string key = string.Concat(Enumerable.Repeat((i + 1).ToString(), valuesCount[i]));

    //                 int value;
    //                 if (FindComboInCombinations(key)) value = _cubesCombos[key];
    //                 else value = SumTheCombosLessThanThree(key, foundCombos);

    //                 // Debug.Log($"Combo: {key} Value: {value}");
    //                 foundCombos.Add(key, value);
    //             }
    //         }
    //         if (cubesUsed == values.Count)
    //         {
    //             Dictionary<string, int> fullCombo = SumToFullCombo(foundCombos);
    //             // Debug.Log("End Test FULL!");
    //             return fullCombo;
    //         }
    //     }
    //     // Find All two or less
    //     for (int i = 0; i < valuesCount.Length; i++)
    //     {
    //         if (valuesCount[i] > 0 && valuesCount[i] <= 2 && (i + 1 == 1 || i + 1 == 5))
    //         {

    //             cubesUsed += valuesCount[i];

    //             string key = string.Concat(Enumerable.Repeat((i + 1).ToString(), valuesCount[i]));

    //             int value = 0;
    //             if (FindComboInCombinations(key)) value = _cubesCombos[key];
    //             else value = SumTheCombosLessThanThree(key, foundCombos);

    //             if (value == 0) continue;
    //             // Debug.Log($"Combo: {key} Value: {value}");
    //             foundCombos.Add(key, value);
    //         }
    //     }
    //     if (cubesUsed == values.Count)
    //     {
    //         Dictionary<string, int> fullCombo = SumToFullCombo(foundCombos);
    //         // Debug.Log("End Test FULL!");
    //         return fullCombo;
    //     }

    //     if (foundCombos.Count > 1)
    //     {
    //         KeyValuePair<string, int> sumOfCombos = SumCombos(foundCombos);
    //         if (!foundCombos.ContainsKey(sumOfCombos.Key)) foundCombos.Add(sumOfCombos.Key, sumOfCombos.Value);
    //     }

    //     // Debug.Log("End Test!");
    //     return foundCombos;
    // }
    // private bool FindComboInCombinations(string key)
    // {
    //     if (_cubesCombos.ContainsKey(key)) return true;
    //     else return false;
    // }
    // private int SumTheCombosLessThanThree(string Key, Dictionary<string, int> foundCombos)
    // {
    //     if (Key.Length > 2) Debug.LogWarning("Error!");

    //     int value = 0;
    //     bool added = false; // to check that it's gooing second +
    //     foreach (var c in Key)
    //     {
    //         switch (c)
    //         {
    //             case '1':
    //                 value += 100;
    //                 break;
    //             case '5':
    //                 value += 50;
    //                 break;
    //         }
    //         if (!added && Key.Length == 2) { foundCombos.Add(Key[0].ToString(), value); itsTwoTheSame = true; }
    //         added = true;
    //     }
    //     return value;
    // }
    // private Dictionary<string, int> SumToFullCombo(Dictionary<string, int> foundCombos)
    // {
    //     if (itsTwoTheSame) { string minKey = foundCombos.Keys.OrderBy(k => k.Length).First(); foundCombos.Remove(minKey); }


    //     Dictionary<string, int> fullCombo = new Dictionary<string, int>();
    //     string newKey = "";
    //     int newValue = 0;
    //     foreach (var combo in foundCombos)
    //     {
    //         newKey += combo.Key;
    //         newValue += combo.Value;
    //     }
    //     string sortedKey = new string(newKey.OrderBy(c => c).ToArray());
    //     fullCombo.Add(sortedKey, newValue);

    //     // Debug.Log($"Combo: {sortedKey} Value: {newValue}");
    //     return fullCombo;
    // }
    // bool itsTwoTheSame = false;
    // private KeyValuePair<string, int> SumCombos(Dictionary<string, int> foundCombos)
    // {
    //     if (itsTwoTheSame) { string minKey = foundCombos.Keys.OrderBy(k => k.Length).First(); foundCombos.Remove(minKey); }


    //     string newKey = "";
    //     int newValue = 0;
    //     foreach (var combo in foundCombos)
    //     {
    //         newKey += combo.Key;
    //         newValue += combo.Value;
    //     }
    //     string sortedKey = new string(newKey.OrderBy(c => c).ToArray());
    //     KeyValuePair<string, int> allComboPair = new KeyValuePair<string, int>(sortedKey, newValue);

    //     // Debug.Log($"Combo: {sortedKey} Value: {newValue}");
    //     return allComboPair;
    // }
    // private KeyValuePair<string, int> FindSequence(string valuesStr)
    // {
    //     if (valuesStr.Contains("123456")) return new KeyValuePair<string, int>(valuesStr, 1500);
    //     else if (valuesStr.Contains("12345")) return new KeyValuePair<string, int>("12345", 500);
    //     else if (valuesStr.Contains("23456")) return new KeyValuePair<string, int>("23456", 750);
    //     else return new KeyValuePair<string, int>("0", 0);
    // }


    // SANYA GOVNO Code
    // private void FindCombinations(string s, List<string> substrings, List<string> currentCombination, int startIndex, List<List<string>> combinations)
    // {
    //     if (startIndex == s.Length)
    //     {
    //         combinations.Add(new List<string>(currentCombination));
    //         return;
    //     }
    //     foreach (var substring in substrings)
    //     {
    //         if (s.Length - startIndex >= substring.Length && s.Substring(startIndex, substring.Length) == substring)
    //         {
    //             currentCombination.Add(substring);
    //             FindCombinations(s, substrings, currentCombination, startIndex + substring.Length, combinations);
    //             currentCombination.RemoveAt(currentCombination.Count - 1);
    //         }
    //     }
    //     if (startIndex < s.Length)
    //     {
    //         string remaining = s.Substring(startIndex, 1);
    //         currentCombination.Add($"Остаток: {remaining}");
    //         FindCombinations(s, substrings, currentCombination, startIndex + 1, combinations);
    //         currentCombination.RemoveAt(currentCombination.Count - 1);
    //     }
    // }


    // // Обратился к чуваку за помощью. Переделал его код как мне надо
    // public Dictionary<string, int> FindAllCombosS(Dictionary<int, int> diceValues)
    // {
    //     var sortedDictionary = diceValues.OrderBy(x => x.Value).ToDictionary(x => x.Key, x => x.Value);
    //     List<int> boneNumbers = sortedDictionary.Keys.ToList();
    //     List<int> boneValues = sortedDictionary.Values.ToList();
    //     List<string> combinationsDefault = _cubesCombos.Keys.ToList();
    //     string value = "";
    //     foreach (var val in boneValues)
    //     {
    //         value += val.ToString();
    //     }
    //     List<List<string>> combinations = new List<List<string>>();
    //     FindCombinations(value, combinationsDefault, new List<string>(), 0, combinations);
    //     //Тут тоже потесть
    //     foreach (var combination in combinations)
    //     {
    //         foreach (var combination2 in combination)
    //         {
    //             //Debug.Log(combination2 + " ");
    //         }
    //         //Debug.Log("\n");
    //     }
    //     List<Dictionary<List<int>, int>> cubes_out = new List<Dictionary<List<int>, int>>();
    //     List<List<int>> cubes_in = new List<List<int>>();
    //     foreach (var list in combinations)
    //     {
    //         Dictionary<List<int>, int> pribavka_temp = new Dictionary<List<int>, int>();
    //         List<int> played_cubes_numbers_in_event = new List<int>();
    //         List<int> cubes_in_event = new List<int>();
    //         int winsum = 0;
    //         for (int i = 0; i < list.Count; i++)
    //         {





    //             if (list[i].IndexOf("Остаток") == -1)
    //             {
    //                 foreach (var s in _cubesCombos)
    //                 {
    //                     if (s.Key == list[i])
    //                     {

    //                         winsum = winsum + s.Value;
    //                         int counter = i;
    //                         for (int j = counter; j < counter + list[i].Length; j++)
    //                         {
    //                             played_cubes_numbers_in_event.Add(boneNumbers[j]);
    //                         }
    //                         // i = counter;

    //                     }
    //                 }



    //             }
    //             else
    //             {

    //                 cubes_in_event.Add(boneNumbers[i]);
    //             }


    //         }
    //         pribavka_temp.Add(played_cubes_numbers_in_event, winsum);
    //         cubes_in.Add(cubes_in_event);
    //         cubes_out.Add(pribavka_temp);


    //     }
    //     //тестиков наделай красиво брат!
    //     foreach (var a in cubes_in)
    //     {
    //         foreach (var b in a)
    //         {
    //             //Debug.Log($"{b} ");
    //         }
    //         //Debug.Log("\n");
    //     }
    //     Dictionary<string, int> cubesOutDictionary = new Dictionary<string, int>();
    //     for (int i = 0; i < cubes_out.Count; i++)
    //     {
    //         foreach (var kvp in cubes_out[i])
    //         {
    //             // Форматируем ключ (список int) в строку
    //             string keyList = string.Join("", kvp.Key);
    //             if (!cubesOutDictionary.ContainsKey(keyList)) cubesOutDictionary.Add(keyList, kvp.Value);
    //         }
    //     }
    //     // Sort value by ascending
    //     var cubesOutDictionarySorted = from combo in cubesOutDictionary orderby combo.Value ascending select combo;
    //     // Вывод в Debug.Log
    //     foreach (var combo in cubesOutDictionarySorted)
    //     {
    //         Debug.Log($"  Ключ: {combo.Key}, Значение: {combo.Value}");
    //     }
    //     Debug.Log("Test END!!!");
    //     return cubesOutDictionary;
    //     //Если что,алгоритм с рекурсией придумала нейронка ,а не я.Это во-первых.Во-вторых,я подумал,что ты должжен давать возможность в некоторых случаях выбирать<широкого> выбора, поэтому
    //     //    в некоторых ситуациях будет происходить такое, что игрок выбирает костей сколько хочет.Может быть я не знаю правила(по типу того, что если есть более крупная комбинация, он обязан! ее выбрать);
    // }


    // GPT + my Mind - GOVNO code
    // public Dictionary<string, int> FindAllCombosGPT(Dictionary<int, int> dice)
    // {
    //     var result = new Dictionary<string, int>();
    //     var values = dice.Values.ToList();
    //     int n = values.Count;

    //     for (int mask = 1; mask < (1 << n); mask++)
    //     {
    //         List<int> subset = new List<int>();
    //         for (int i = 0; i < n; i++)
    //         {
    //             if ((mask & (1 << i)) != 0)
    //                 subset.Add(values[i]);
    //         }

    //         subset.Sort();
    //         string subsetKey = string.Join(",", subset);

    //         int points = CalculateSubsetPoints(subset);
    //         if (points > 0)
    //             result[subsetKey] = points;
    //     }

    //     return result.OrderBy(kv => kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
    // }

    // private int CalculateSubsetPoints(List<int> subset)
    // {
    //     List<int> dice = new List<int>(subset);
    //     int points = 0;
    //     Dictionary<int, int> counts = dice.GroupBy(x => x).ToDictionary(g => g.Key, g => g.Count());

    //     if (IsStraight(dice)) return 1500;
    //     if (IsSmallStraight(dice)) return 500;
    //     if (IsLargeStraight(dice)) return 750;

    //     if (IsSmallStraight(dice) || IsLargeStraight(dice))
    //     {
    //         int basePoints = IsSmallStraight(dice) ? 500 : 750;
    //         int bonus = 0;

    //         if (counts.ContainsKey(1) && counts[1] > 0) bonus = 100;
    //         if (counts.ContainsKey(5) && counts[5] > 0) bonus = 50;

    //         return basePoints + bonus;
    //     }

    //     for (int num = 1; num <= 6; num++)
    //     {
    //         if (!counts.ContainsKey(num) || counts[num] == 0) continue;

    //         int count = counts[num];
    //         if (count >= 3)
    //         {
    //             int multiplier = count - 2;
    //             points += (num == 1 ? 1000 : num * 100) * multiplier;
    //         }
    //         else if (num == 1 || num == 5)
    //         {
    //             points += count * (num == 1 ? 100 : 50);
    //         }
    //     }
    //     return points;
    // }

    // private bool IsStraight(List<int> dice)
    // {
    //     return dice.Count == 6 && dice.Distinct().OrderBy(x => x).SequenceEqual(new[] { 1, 2, 3, 4, 5, 6 });
    // }

    // private bool IsSmallStraight(List<int> dice)
    // {
    //     return dice.Count >= 5 && dice.Distinct().OrderBy(x => x).Take(5).SequenceEqual(new[] { 1, 2, 3, 4, 5 });
    // }

    // private bool IsLargeStraight(List<int> dice)
    // {
    //     return dice.Count >= 5 && dice.Distinct().OrderBy(x => x).Take(5).SequenceEqual(new[] { 2, 3, 4, 5, 6 });
    // }
}