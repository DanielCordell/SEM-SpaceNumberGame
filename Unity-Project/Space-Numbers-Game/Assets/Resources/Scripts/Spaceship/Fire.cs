using UnityEngine;
using UnityEngine.UI;

public class Fire : MonoBehaviour
{
    Button fireBtn;

    private AudioSource audioSource;

    private AudioClip soundShoot;
    private AudioClip soundWrongAnswer;

    private Text hintText;

    LevelHandler levelHandler;

    private bool hasFired;

    private bool areGapsFilled;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<LevelHandler>();

        soundWrongAnswer = Resources.Load<AudioClip>("audio/sound/wrong_answer");
        soundShoot = Resources.Load<AudioClip>("audio/sound/shoot");
        audioSource = GetComponent<AudioSource>();

        fireBtn = GetComponentInChildren<Button>();
        fireBtn.onClick.AddListener(fireBtnOnClick);

        hintText = GetComponentInChildren<Text>();

        Reset();
    }

    public void Reset()
    {
        hasFired = false;
    }

    // Update is called once per frame
    void Update()
    {
        areGapsFilled = levelHandler.AreAllGapsFilled();
        if (!hasFired)
        {
            if (areGapsFilled)
                hintText.text = "Click  To  Fire!";
            else
                hintText.text = "";
        }
    }


    private void fireBtnOnClick()
    {
        Debug.Log("Firing!");
        if (hasFired || !areGapsFilled) return;
        if (levelHandler.ValidateAnswer())
        {
            audioSource.clip = soundShoot;
            hintText.text = "Good  Job!";
        }
        else
        {
            audioSource.clip = soundWrongAnswer;
            hintText.text = "Wrong  Answer!";
        }
        audioSource.Play();
        hasFired = true;
    }
}
