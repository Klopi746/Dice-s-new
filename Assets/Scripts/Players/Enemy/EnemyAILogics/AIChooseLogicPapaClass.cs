using System.Collections;
using System.Linq;
using UnityEngine;

public class AIChooseLogicPapaClass : MonoBehaviour
{
    protected EnemySCRIPT enemy;
    private void Awake()
    {
        enemy = transform.GetComponent<EnemySCRIPT>();
    }


    public IEnumerator AILogic()
    {
        yield return StartCoroutine(CheckForCombo());
        if (enemy.continuePlay)
        {
            _cubesRemainOnEnd = CubesRemainOnEnd();
            OnEndLogic();
        }
        yield return new WaitForSeconds(1f);
    }
    private IEnumerator CheckForCombo()
    {
        _cubesRemainOnStart = CubesRemainOnStart();
        if (enemy.isFindCombo)
        {
            enemy.continuePlay = true;
            if (enemy.curCombos.Count > 0) yield return StartCoroutine(AIChooseComboLogic());
        }
        else
        {
            Debug.Log("NO COMBO! :(");
            enemy.temporaryScoreText.text = "0";
            enemy.noComboTextObj.gameObject.SetActive(true);
            enemy.continuePlay = false;
        }
    }
    /// <summary>
    /// Here logic before choosing cubes. You have _cubesRemainOnStart
    /// </summary>
    protected virtual IEnumerator AIChooseComboLogic()
    {
        if (enemy.curCombos.Count == 1)
        {
            yield return StartCoroutine(FindCubesForCombo(enemy.curCombos.ElementAt(0).Key));
        }
        else
        {
            yield return StartCoroutine(FindCubesForCombo(enemy.curCombos.Last().Key));
        }
    }
    protected IEnumerator FindCubesForCombo(string combo)
    {
        Debug.Log($"AI choosed combo: {combo}");
        for (int i = 0; i < combo.Length; i++)
        {
            yield return new WaitForSeconds(0.7f);

            int number = int.Parse(combo[i].ToString());

            foreach (var diceScript in enemy.cubesScripts)
            {
                if (diceScript.enabled)
                {
                    if (diceScript.CurrentNumber == number && diceScript.diceClickSCRIPT.wasClicked == false) { diceScript.diceClickSCRIPT.EnemyClick(); break; }
                }
            }
        }
        Debug.Log("AI find all needed cubes!");
    }


    protected int _cubesRemainOnStart = 0;
    protected int CubesRemainOnStart()
    {
        int cubesRemain = 0;
        foreach (DicePapaSCRIPT script in enemy.cubesScripts)
        {
            if (script.enabled) cubesRemain += 1;
        }
        return cubesRemain;
    }


    protected int _cubesRemainOnEnd = 0;
    protected int CubesRemainOnEnd()
    {
        int cubesRemain = 0;
        foreach (DicePapaSCRIPT script in enemy.cubesScripts)
        {
            if (script.enabled && script.diceClickSCRIPT.wasClicked == false) cubesRemain += 1;
        }
        return cubesRemain;
    }


    /// <summary>
    /// Here logic before choosing cubes. You have _cubesRemainOnEnd
    /// </summary>
    protected virtual void OnEndLogic()
    {
        if (_cubesRemainOnEnd < 3 && _cubesRemainOnEnd != 0)
        {
            float percentOfNoContinue = (_cubesRemainOnEnd > 1) ? 0.5f : 0.75f;

            float randomValue = Random.value;
            if (randomValue > percentOfNoContinue) enemy.continuePlay = true;
            else enemy.continuePlay = false;
            Debug.Log($"Процент что AI продолжит {randomValue * 100}% > {percentOfNoContinue * 100}%? {enemy.continuePlay}");
        }

        if (_cubesRemainOnEnd == 0) enemy.continuePlay = true;

        if (EnemySCRIPT.Instance.CheckCurScore() >= GameHandlerSCRIPT.Instance.goalScore) enemy.continuePlay = false;
    }
}
