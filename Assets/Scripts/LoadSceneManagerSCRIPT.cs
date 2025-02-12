using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManagerSCRIPT : MonoBehaviour
{
    public static LoadSceneManagerSCRIPT Instance;

    private void Awake()
    {
        Instance = this;
        DontDestroyOnLoad(this);
    }

    public void LoadNewScene(int sceneNumber = 0)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
