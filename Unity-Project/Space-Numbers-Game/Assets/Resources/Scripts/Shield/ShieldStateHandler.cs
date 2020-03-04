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

    public int AddCountWrong()
    {
        CountWrong += 1;
        return CountWrong;
    }

    public void UpdateShieldState(int currentWrongTimes)
    {
        // according to the currentWrongTimes, change the current sprites to show
        switch (currentWrongTimes)
        {
            case 0:
                break;
            case 1:
                healthBarSR.sprite = Resources.Load("Art/Textures/healthBar1", typeof(Sprite)) as Sprite;
                shieldSR.sprite = Resources.Load("Art/Textures/shields1", typeof(Sprite)) as Sprite;
                break;
            case 2:
                healthBarSR.sprite = Resources.Load("Art/Textures/healthBar2", typeof(Sprite)) as Sprite;
                shieldSR.sprite = Resources.Load("Art/Textures/shields2", typeof(Sprite)) as Sprite;
                break;
            case 3:
                healthBarSR.sprite = Resources.Load("Art/Textures/healthBar3", typeof(Sprite)) as Sprite;
                shieldSR.sprite = Resources.Load("Art/Textures/shields3", typeof(Sprite)) as Sprite;
                break;
            default:
                //TODO Should jump to game over scene.
                Debug.Log("Once player gets 3 times wrong, jump to game over scene.");
                break;
        }
    }


    // Update is called once per frame
    void Update()
    {

    }
}
