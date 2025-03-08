using System.Collections;
using System.Linq;
using UnityEngine;

public class AICleverLogicSCRIPT : AIChooseLogicPapaClass
{
    protected override IEnumerator AIChooseComboLogic()
    {
        if (enemy.curCombos.Count == 1)
        {
            yield return StartCoroutine(FindCubesForCombo(enemy.curCombos.ElementAt(0).Key));
        }
        else
        {
            float finalRatio = 0;
            int comboIndex = 0;
            for (int i = 0; i < enemy.curCombos.Count; i++)
            {
                var combo = enemy.curCombos.ElementAt(i);
                float comboRatio = combo.Value / combo.Key.Length;
                if (comboRatio > finalRatio + 20)
                {
                    finalRatio = comboRatio;
                    comboIndex = i;
                }
            }
            if (enemy.curCombos.Last().Key.Length == 5 && _cubesRemainOnStart == 6 && enemy.curCombos.Last().Value >= 500) comboIndex = enemy.curCombos.Count - 1;
            if (enemy.curCombos.Last().Key.Length == 4 && _cubesRemainOnStart == 5 && enemy.curCombos.Last().Value >= 400) comboIndex = enemy.curCombos.Count - 1;
            if (enemy.curCombos.Last().Key.Length == 3 && _cubesRemainOnStart == 4 && enemy.curCombos.Last().Value >= 300) comboIndex = enemy.curCombos.Count - 1;
            if (enemy.curCombos.Last().Key.Length == 2 && _cubesRemainOnStart == 3) comboIndex = enemy.curCombos.Count - 1;
            if (enemy.curCombos.Last().Key.Length == _cubesRemainOnStart) comboIndex = enemy.curCombos.Count - 1;
            yield return StartCoroutine(FindCubesForCombo(enemy.curCombos.ElementAt(comboIndex).Key));
        }
    }
    protected override void OnEndLogic()
    {
        if (_cubesRemainOnEnd == 3 &&_cubesRemainOnEnd != 0)
        {
            float percentOfNoContinue = (int.Parse(enemy.temporaryScoreText.text) < 400) ? 0f : 0.15f;

            float randomValue = Random.value;
            if (randomValue > percentOfNoContinue) enemy.continuePlay = true;
            else enemy.continuePlay = false;
            Debug.Log($"Процент что AI продолжит {randomValue * 100}% > {percentOfNoContinue * 100}%? {enemy.continuePlay}");
        }

        if (_cubesRemainOnEnd == 2 &&_cubesRemainOnEnd != 0)
        {
            float enemyScore = int.Parse(EnemySCRIPT.Instance.scoreText.text);
            float playerScore = int.Parse(PlayerSCRIPT.Instance.scoreText.text);
            float seq = 0;
            if (playerScore > 0.1f) seq = enemyScore - playerScore;

            float percentOfNoContinue = (int.Parse(enemy.temporaryScoreText.text) < 500 && seq >= 1000) ? 0.1f : 0.85f;

            float randomValue = Random.value;
            if (randomValue > percentOfNoContinue) enemy.continuePlay = true;
            else enemy.continuePlay = false;
            Debug.Log($"Процент что AI продолжит {randomValue * 100}% > {percentOfNoContinue * 100}%? {enemy.continuePlay}");
        }

        if (_cubesRemainOnEnd == 1 &&_cubesRemainOnEnd != 0)
        {
            float enemyScore = int.Parse(EnemySCRIPT.Instance.scoreText.text);
            float playerScore = int.Parse(PlayerSCRIPT.Instance.scoreText.text);
            float seq = 0;
            if (playerScore > 0.1f) seq = enemyScore - playerScore;

            float percentOfNoContinue = (int.Parse(enemy.temporaryScoreText.text) < 600 && seq >= 1000) ? 0.7f : 1f;

            float randomValue = Random.value;
            if (randomValue > percentOfNoContinue) enemy.continuePlay = true;
            else enemy.continuePlay = false;
            Debug.Log($"Процент что AI продолжит {randomValue * 100}% > {percentOfNoContinue * 100}%? {enemy.continuePlay}");
        }

        if (_cubesRemainOnEnd <= 3 &&_cubesRemainOnEnd != 0)
        {
            float enemyScore = int.Parse(EnemySCRIPT.Instance.temporaryScoreText.text);
            float goalScore = GameHandlerSCRIPT.Instance.goalScore;
            float seq = 1000;
            if (enemyScore > 0.1f) seq = goalScore / enemyScore;
            if (seq <= 2.4f) enemy.continuePlay = false;
            Debug.Log($"AI боится и заканчивает");
        }

        if (_cubesRemainOnEnd == 0) enemy.continuePlay = true;

        if (EnemySCRIPT.Instance.CheckCurScore() >= GameHandlerSCRIPT.Instance.goalScore) enemy.continuePlay = false;
    }
}