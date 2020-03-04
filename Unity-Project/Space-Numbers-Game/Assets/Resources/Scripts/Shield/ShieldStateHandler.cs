using UnityEngine;

public class ShieldStateHandler : MonoBehaviour
{
    public GameObject Shields;
    // healthBars is regarded as part of Shield
    public GameObject HealthBars;
    // Record how many wrong times currently
    public int CountWrong;
    // Shiled broke Sound effect
    //public AudioSource shieldBreak;

    // The current sprite of healthBar
    SpriteRenderer healthBarSR;
    // The current sprite of Shield
    SpriteRenderer shieldSR;


    // Start is called before the first frame update
    void Start()
    {
        InitialiseShieldState();
    }

    public void InitialiseShieldState()
    {
        // Find game objects
        HealthBars = GameObject.FindGameObjectWithTag("HealthBars");
        Shields = GameObject.FindGameObjectWithTag("Shields");
        //shieldBreak = shields.transform.GetComponent<AudioSource>();
        // Find the current sprite
        healthBarSR = HealthBars.transform.GetComponent<SpriteRenderer>();
        shieldSR = Shields.transform.GetComponent<SpriteRenderer>();
        // Reset countWrong
        CountWrong = 0;
        // Set initial sprite
        healthBarSR.sprite = Resources.Load("Art/Textures/healthBarFull", typeof(Sprite)) as Sprite;
        shieldSR.sprite = Resources.Load("Art/Textures/shieldsFull", typeof(Sprite)) as Sprite;
    }

    public void AddCountWrong()
    {
        CountWrong++;
        UpdateShieldState(CountWrong);
    }

    void UpdateShieldState(int currentWrongTimes) {
        Debug.Log("Current Wrong Times: " + currentWrongTimes);
        // according to the currentWrongTimes, change the current sprites to show
        switch (currentWrongTimes)
        {
            case 0:
                break;
            case 1:
            case 2:
            case 3:
                shieldSR.sprite = Resources.Load("Art/Textures/shields" + currentWrongTimes, typeof(Sprite)) as Sprite;
                healthBarSR.sprite = Resources.Load("Art/Textures/healthBar" + currentWrongTimes, typeof(Sprite)) as Sprite;
                if (currentWrongTimes == 3)
                    healthBarSR.sprite = null;
                else
                    healthBarSR.sprite = Resources.Load("Art/Textures/healthBar" + currentWrongTimes, typeof(Sprite)) as Sprite;
                break;
            default:
                //TODO Should jump to game over scene.
                Debug.Log("Once player gets 3 times wrong, jump to game over scene.");
                break;
        }
    }

    public string GetHealthTextureName()
    {
        if (healthBarSR.sprite == null) return null;
        return healthBarSR.sprite.texture.name;
    }

    public string GetShieldTextureName()
    {
        if (shieldSR.sprite == null) return null;
        return shieldSR.sprite.texture.name;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
