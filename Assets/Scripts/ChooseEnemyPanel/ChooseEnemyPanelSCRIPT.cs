using UnityEngine;
using UnityEngine.UI;

public class ChooseEnemyPanelSCRIPT : MonoBehaviour
{
    public static ChooseEnemyPanelSCRIPT Instance;
    private void Awake()
    {
        Instance = this;
    }


    [SerializeField] Image[] EnemiesImageComponents;
    public void DisableAll()
    {
        foreach (var image in EnemiesImageComponents)
        {
            image.color = Color.white;            
        }
    }
}