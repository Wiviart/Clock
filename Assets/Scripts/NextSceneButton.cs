using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    Button nextSceneButton;
    int currentSceneIndex = 1;
    void Awake()
    {
        nextSceneButton = GetComponentInChildren<Button>();
        nextSceneButton.onClick.AddListener(NextScene);
    }

    void Start()
    {
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
    }

    private void NextScene()
    {
        SceneManager.UnloadSceneAsync(currentSceneIndex);
        currentSceneIndex++;
        if (currentSceneIndex > SceneManager.sceneCountInBuildSettings - 1)
        {
            currentSceneIndex = 1;
        }
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
    }
}
