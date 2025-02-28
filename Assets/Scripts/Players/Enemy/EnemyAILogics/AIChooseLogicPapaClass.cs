using System.Collections;
using UnityEngine;

public class AIChooseLogicPapaClass : MonoBehaviour
{
    public virtual IEnumerator AIChooseLogic()
    {
        yield return new WaitForSeconds(1f);
    }
}
