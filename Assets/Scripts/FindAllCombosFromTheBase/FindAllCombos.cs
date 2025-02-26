using System.Collections.Generic;
using System.IO;
using System.Linq;
using UnityEngine;

public class FindAllCombos : MonoBehaviour
{
    private void Start()
    {
        Main();
    }
    protected static readonly Dictionary<string, int> _cubesCombos = new Dictionary<string, int>()
    {
        {"1",100},
        {"11",200},
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
        {"5",50},
        {"55",100},
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

    // Результирующий словарь: ключ – отсортированная комбинация, значение – сумма значений исходных слов
    static Dictionary<string, int> _results = new Dictionary<string, int>();

    public static void Main()
    {
        // Для удобства работаем со списком пар (ключ, значение) в каноническом порядке
        var baseCombos = _cubesCombos.ToList();
        // Сортируем по ключу (лексикографически) – это позволит генерировать комбинации без дубликатов в порядке выбора
        baseCombos.Sort((a, b) => string.Compare(a.Key, b.Key));

        // Рекурсивно генерируем комбинации
        GenerateCombos(baseCombos, 0, "", 0);

        // Записываем результат в файл в формате: {"ключ",значение},
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "combos.txt");
        using (StreamWriter writer = new StreamWriter(outputPath))
        {
            foreach (var item in _results.OrderBy(r => r.Key))
            {
                writer.WriteLine($"{{\"{item.Key}\",{item.Value}}},");
            }
        }
        Debug.Log($"Комбинации записаны в файл: {outputPath}");
    }

    // Рекурсивный метод для генерации комбинаций.
    // Параметры:
    //  baseCombos – список исходных пар (ключ, значение)
    //  startIndex – индекс, с которого разрешено брать следующий элемент (чтобы избежать разных перестановок одного и того же набора)
    //  currentCombo – накопленная строка (конкатенация выбранных ключей)
    //  currentValue – накопленная сумма значений выбранных ключей
    static void GenerateCombos(List<KeyValuePair<string, int>> baseCombos, int startIndex, string currentCombo, int currentValue)
    {
        // Если комбинация не пустая и её длина меньше 7, обрабатываем её
        if (currentCombo.Length > 0 && currentCombo.Length < 7)
        {
            // Сортируем символы по возрастанию – итоговый ключ
            string sortedKey = new string(currentCombo.ToCharArray().OrderBy(c => c).ToArray());

            // Если такая комбинация уже была – оставляем ту, у которой сумма больше
            if (_results.ContainsKey(sortedKey))
            {
                if (currentValue > _results[sortedKey])
                    _results[sortedKey] = currentValue;
            }
            else
            {
                _results.Add(sortedKey, currentValue);
            }
        }

        // Если достигли длины 6, дальше добавлять нельзя
        if (currentCombo.Length >= 6)
            return;

        // Перебираем все базовые ключи, начиная с startIndex,
        // чтобы избежать разных вариантов одного и того же набора
        for (int i = startIndex; i < baseCombos.Count; i++)
        {
            string keyPart = baseCombos[i].Key;
            // Проверяем, не превысим ли мы допустимую длину
            if (currentCombo.Length + keyPart.Length <= 6)
            {
                GenerateCombos(baseCombos,
                               i, // разрешаем брать тот же элемент (повторения)
                               currentCombo + keyPart,
                               currentValue + baseCombos[i].Value);
            }
        }
    }
}