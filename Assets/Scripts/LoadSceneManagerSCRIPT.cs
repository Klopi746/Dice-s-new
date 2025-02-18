using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadSceneManagerSCRIPT : MonoBehaviour
{
    public static LoadSceneManagerSCRIPT Instance;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void LoadNewScene(int sceneNumber = 0)
    {
        SceneManager.LoadScene(sceneNumber);
    }
}
