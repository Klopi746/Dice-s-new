using System.Collections;
using UnityEngine;

public class AIStupidLogicSCRIPT : AIChooseLogicPapaClass
{
    public override IEnumerator AIChooseLogic()
    {
        yield return new WaitForSeconds(2f);
    }
}