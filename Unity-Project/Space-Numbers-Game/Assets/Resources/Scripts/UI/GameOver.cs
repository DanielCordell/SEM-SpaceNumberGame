using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Button playAgainBtn;
    Button menuBtn;
    Button quitBtn;
    SceneHandler sceneHandler;

    // Start is called before the first frame update
    void Start()
    {
        sceneHandler = GameObject.FindGameObjectWithTag("GameOverSceneHandler").GetComponent<SceneHandler>();
        playAgainBtn = GameObject.FindGameObjectWithTag("PlayAgain").GetComponent<Button>();
        menuBtn = GameObject.FindGameObjectWithTag("Menu").GetComponent<Button>();
        quitBtn = GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>();
        playAgainBtn.onClick.AddListener(sceneHandler.GoLevelScene);
        menuBtn.onClick.AddListener(sceneHandler.GoMenuScene);
        quitBtn.onClick.AddListener(sceneHandler.ExitGame);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
