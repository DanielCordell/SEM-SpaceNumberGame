using UnityEngine;
using UnityEngine.UI;

public class GameOver : MonoBehaviour
{
    Button playAgainBtn;
    Button menuBtn;
    Button quitBtn;
    SceneHandler sceneHandler;
    Text scoreText;

    // Start is called before the first frame update
    void Start()
    {
        scoreText = GameObject.FindGameObjectWithTag("ScoreText").GetComponent<Text>();
        sceneHandler = GameObject.FindGameObjectWithTag("SceneHandler").GetComponent<SceneHandler>();
        playAgainBtn = GameObject.FindGameObjectWithTag("PlayAgain").GetComponent<Button>();
        menuBtn = GameObject.FindGameObjectWithTag("Menu").GetComponent<Button>();
        quitBtn = GameObject.FindGameObjectWithTag("Quit").GetComponent<Button>();
        playAgainBtn.onClick.AddListener(sceneHandler.GoLevelScene);
        menuBtn.onClick.AddListener(sceneHandler.GoMenuScene);
        quitBtn.onClick.AddListener(sceneHandler.ExitGame);
    }

    void Update()
    {
        scoreText.text = "You Got " + PlayerPrefs.GetInt("Score", 0).ToString() + " Question Right!";
    }

}
