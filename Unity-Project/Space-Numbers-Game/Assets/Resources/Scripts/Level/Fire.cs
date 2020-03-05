using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using PathCreation;
using System.Collections.Generic;
using System.Collections;

public class Fire : MonoBehaviour
{
    private Button buttonFire;
    private AudioSource audioSource;
    private Text hintText;

    private AudioClip soundShoot;
    private AudioClip soundWrongAnswer;

    private LevelHandler levelHandler;
    
    private ShieldStateHandler shieldStateHandler;

    private bool hasFired;
    private bool areGapsFilled;

    public GameObject PrefabLaser;

    private DataHandler dataHandler;

    // Start is called before the first frame update
    void Start()
    {
        levelHandler = GameObject.FindGameObjectWithTag("GameHandler").GetComponent<LevelHandler>();
        dataHandler = GameObject.FindGameObjectWithTag("DataHandler").GetComponent<DataHandler>();
        soundWrongAnswer = Resources.Load<AudioClip>("audio/sound/wrong_answer");
        soundShoot = Resources.Load<AudioClip>("audio/sound/shoot");
        audioSource = GetComponent<AudioSource>();

        buttonFire = GetComponentInChildren<Button>();
        buttonFire.onClick.AddListener(FireButtonOnClick);

        hintText = GetComponentInChildren<Text>();

        shieldStateHandler = GameObject.FindGameObjectWithTag("Shields").GetComponent<ShieldStateHandler>();
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


    private void FireButtonOnClick()
    {
        Debug.Log("Firing!");
        if (shieldStateHandler.CountWrong <= 3)
            hasFired = false;
        if (hasFired || !areGapsFilled) return;
        if (levelHandler.ValidateAnswer())
        {
            audioSource.clip = soundShoot;
            hintText.text = "Good  Job!";
            dataHandler.AddRightNum();
            FireLasers();
            StartCoroutine(TriggerUpdateAfterDelay());
        }
        else
        {
            audioSource.clip = soundWrongAnswer;
            hintText.text = "Wrong  Answer!";
            shieldStateHandler.AddCountWrong();
            Debug.Log("Current wrong times: " + shieldStateHandler.CountWrong);
        }
        hasFired = true;
        audioSource.Play();
        
    }

    private void FireLasers()
    {
        Vector3 spawnPosition = transform.Find("LaserStart").position;
        List<GameObject> selectedAsteroids = GameObject.FindGameObjectsWithTag("Asteroid").Where(it => it.GetComponent<Asteroid>().Selected).ToList();
        selectedAsteroids.ForEach(it => {
            GameObject laser = Instantiate(PrefabLaser, spawnPosition, Quaternion.identity);
            laser.GetComponentInChildren<LaserMove>().AsteroidTarget = it;
            PathCreator pathCreator = laser.GetComponent<PathCreator>();
            Vector3 point = new Vector3(it.transform.position.x - spawnPosition.x, it.transform.position.y - spawnPosition.y, 0);
            pathCreator.bezierPath = new BezierPath(new Vector3[3] { pathCreator.bezierPath.GetPoint(0), pathCreator.bezierPath.GetPoint(1), point }, false, PathSpace.xy);
        });
    }

    private IEnumerator TriggerUpdateAfterDelay()
    {
        yield return new WaitForSeconds(3);
        levelHandler.SetLevelShouldUpdate();
    }
}
