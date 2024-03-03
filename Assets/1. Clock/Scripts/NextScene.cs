using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class NextScene : MonoBehaviour
{
    Button nextSceneButton;
    void Awake()
    {
        nextSceneButton = GetComponent<Button>();
        nextSceneButton.onClick.AddListener(NextSceneButton);
    }

    private void NextSceneButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
