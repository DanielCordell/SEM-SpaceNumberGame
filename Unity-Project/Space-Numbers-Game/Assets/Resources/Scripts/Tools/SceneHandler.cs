using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneHandler : MonoBehaviour
{

    public void GoGameOverScene()
    {
        SceneManager.LoadScene("GameOverScene");
    }

    public void GoSpaceScene()
    {
        SceneManager.LoadScene("SpaceScene");
    }

    public void GoLevelScene()
    {
        SceneManager.LoadScene("SpaceScene");
    }

    public void GoMenuScene()
    {

        SceneManager.LoadScene("MenuScene");
    }

    public void ExitGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
        Application.Quit();
    }
}
