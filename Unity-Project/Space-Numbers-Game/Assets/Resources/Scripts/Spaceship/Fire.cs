using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using PathCreation;
using System.Collections.Generic;

public class Fire : MonoBehaviour
{
    private Button fireBtn;
    private AudioSource audioSource;
    private Text hintText;

    private AudioClip soundShoot;
    private AudioClip soundWrongAnswer;

    private LevelHandler levelHandler;

    private ShieldStateHandler ShieldStateHandler;

    private bool hasFired;
    private bool areGapsFilled;
    private bool isWrongAnswer;

    public GameObject laserPrefab;

    // Start is called before the first frame update
    void Start()
    {
        isWrongAnswer = false;
        levelHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<LevelHandler>();
        ShieldStateHandler = GameObject.FindGameObjectWithTag("ShieldStateHandler").GetComponent<ShieldStateHandler>();
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
            if (isWrongAnswer)
                hintText.text = "Wrong  Answer!";
        }
    }


    private void fireBtnOnClick()
    {
        Debug.Log("Firing!");
        if (hasFired || !areGapsFilled) return;
        if (levelHandler.ValidateAnswer())
        {
            audioSource.clip = soundShoot;
            isWrongAnswer = false;
            hintText.text = "Good  Job!";
            FireLasers();
            hasFired = true;
        }
        else
        {
            audioSource.clip = soundWrongAnswer;
            isWrongAnswer = true;
            Debug.Log(hintText.text);
            ShieldStateHandler.AddCountWrong();
            hasFired = false;
        }
        audioSource.Play();
    }

    private void FireLasers()
    {
        Vector3 spawnPosition = transform.Find("LaserStart").position;
        List<GameObject> selectedAsteroids = GameObject.FindGameObjectsWithTag("Asteroid").Where(it => it.GetComponent<Asteroid>().Selected).ToList();
        selectedAsteroids.ForEach(it => {
            GameObject laser = Instantiate(laserPrefab, spawnPosition, Quaternion.identity);
            laser.GetComponentInChildren<LaserMove>().asteroidTarget = it;
            PathCreator pathCreator = laser.GetComponent<PathCreator>();
            Vector3 point = new Vector3(it.transform.position.x - spawnPosition.x, it.transform.position.y - spawnPosition.y, 0);
            pathCreator.bezierPath = new BezierPath(new Vector3[3] { pathCreator.bezierPath.GetPoint(0), pathCreator.bezierPath.GetPoint(1), point }, false, PathSpace.xy);
        });
    }
}
