using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenesManager : MonoBehaviour
{
    public static ScenesManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log($"ScenesManager instance created in scene: {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}");

        }
        else
        {
            Debug.LogWarning($"Duplicate ScenesManager detected in {UnityEngine.SceneManagement.SceneManager.GetActiveScene().name}, destroying it.");
            Destroy(gameObject);
        }
    }

    public void LoadLevel(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
}
