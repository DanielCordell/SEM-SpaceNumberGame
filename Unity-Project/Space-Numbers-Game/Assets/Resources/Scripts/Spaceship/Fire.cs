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
    
    private ShieldStateHandler shieldStateHandler;

    private bool hasFired;
    private bool areGapsFilled;

    public GameObject laserPrefab;

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

        shieldStateHandler = GameObject.FindGameObjectWithTag("ShieldStateHandler").GetComponent<ShieldStateHandler>();
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
        if (shieldStateHandler.countWrong <= 3)
            hasFired = false;
        if (hasFired || !areGapsFilled) return;
        if (levelHandler.ValidateAnswer())
        {
            audioSource.clip = soundShoot;
            hintText.text = "Good  Job!";
            FireLasers();
        }
        else
        {
            audioSource.clip = soundWrongAnswer;
            hintText.text = "Wrong  Answer!";
            shieldStateHandler.UpdateShieldState(shieldStateHandler.AddCountWrong());
            Debug.Log("Cunrrent wrong times: " + shieldStateHandler.countWrong);
        }
        hasFired = true;
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
