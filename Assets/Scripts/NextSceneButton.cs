using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextSceneButton : MonoBehaviour
{
    [SerializeField] Button nextSceneButton;
    int currentSceneIndex = 1;
    void Awake()
    {
        nextSceneButton.onClick.AddListener(NextScene);
    }

    void Start()
    {
#if UNITY_EDITOR
#else
        SceneManager.LoadSceneAsync(currentSceneIndex, LoadSceneMode.Additive);
#endif
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
