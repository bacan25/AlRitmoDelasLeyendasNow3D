using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    public static SceneLoader Instance { get; private set; }
    private string currentSceneName = "SampleScene";
    private bool isLoading = false; // Añadido para prevenir cargas múltiples

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        LoadScene("Inicio");
    }

    public void LoadScene(string sceneName)
    {
        if (isLoading)
        {
            Debug.LogWarning("A scene load is already in progress. Ignoring LoadScene request for: " + sceneName);
            return;
        }

        if (currentSceneName == sceneName)
        {
            Debug.LogWarning("Trying to load the current scene again: " + sceneName);
            return;
        }
        isLoading = true;
        Debug.Log("Loading scene: " + sceneName);
        StartCoroutine(SwitchSceneCoroutine(sceneName));
    }

    private IEnumerator SwitchSceneCoroutine(string newScene)
    {
        if (currentSceneName != "SampleScene")
        {
            Debug.Log("Unloading scene: " + currentSceneName);
            yield return SceneManager.UnloadSceneAsync(currentSceneName);
        }
        else
        {
            Debug.Log("SampleScene is persistent and will not be unloaded.");
        }

        yield return SceneManager.LoadSceneAsync(newScene, LoadSceneMode.Additive);
        currentSceneName = newScene;
        isLoading = false; // Carga completada
        Debug.Log("Current scene is now: " + currentSceneName);
    }

    public void GoToNextLevel()
    {
        if (isLoading)
        {
            Debug.LogWarning("A scene load is already in progress. Ignoring GoToNextLevel request.");
            return;
        }

        string nextSceneName = GetNextSceneName();
        if (!string.IsNullOrEmpty(nextSceneName))
        {
            LoadScene(nextSceneName);
        }
        else
        {
            Debug.LogError("Next level name is null or empty.");
        }
    }

    private string GetNextSceneName()
    {
        // Asegúrate de que este switch refleja correctamente el flujo de tu juego
        switch (currentSceneName)
        {
            case "Inicio":
                return "Level_1_Android";
            case "Level_1_Android":
                return "NextLevel";
            case "NextLevel":
                return "Level_2_Android";
            case "Level_2_Android":
                return "Final";
            default:
                Debug.LogError("Unknown scene: " + currentSceneName);
                return null;
        }
    }
}
