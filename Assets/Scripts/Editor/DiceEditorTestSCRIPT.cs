using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DicePapaSCRIPT), true)]
public class DiceEditorTestSCRIPT : Editor
{
    public override void OnInspectorGUI()
    {
        // Отрисовка стандартного интерфейса
        base.OnInspectorGUI();
        
        // Добавляем кнопку
        if (GUILayout.Button("Протестировать"))
        {
            // Вызываем метод у целевого компонента
            ((DicePapaSCRIPT)target).TestRoll();
        }
        if (GUILayout.Button("Сумма"))
        {
            // Вызываем метод у целевого компонента
            ((DicePapaSCRIPT)target).PrintProbabilitySum();
        }
    }
}
