using System.Collections;
using UnityEngine;

public class AICleverLogicSCRIPT : AIChooseLogicPapaClass
{
    public override IEnumerator AIChooseLogic()
    {
        yield return new WaitForSeconds(0.5f);
    }
}