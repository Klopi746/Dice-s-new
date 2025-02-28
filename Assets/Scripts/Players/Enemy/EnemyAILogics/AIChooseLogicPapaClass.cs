using System.Collections;
using System.Linq;
using UnityEngine;

public class AIChooseLogicPapaClass : MonoBehaviour
{
    private EnemySCRIPT enemy;
    private void Awake()
    {
        enemy = transform.GetComponent<EnemySCRIPT>();
    }


    public IEnumerator AILogic()
    {
        yield return StartCoroutine(CheckForCombo());
        yield return new WaitForSeconds(10f);
    }
    private IEnumerator CheckForCombo()
    {
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
        yield return null;
    }
    protected virtual IEnumerator AIChooseComboLogic()
    {
        if (int.Parse(enemy.temporaryScoreText.text) > 500) { enemy.continuePlay = false; yield return null; }
        if (enemy.curCombos.Count == 1)
        {
            StartCoroutine(FindCubesForCombo(enemy.curCombos.ElementAt(0).Key));
        }
        else
        {
            StartCoroutine(FindCubesForCombo(enemy.curCombos.Last().Key));
        }
        yield return null;
    }
    protected IEnumerator FindCubesForCombo(string combo)
    {
        Debug.Log($"AI choosed combo: {combo}");
        for (int i = 0; i < combo.Length; i++)
        {
            yield return new WaitForSeconds(2f);

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
}
