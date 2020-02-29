using UnityEngine;

public class ShieldStateHandler : MonoBehaviour
{
    public GameObject shields;
    // healthBars is regarded as part of Shield
    public GameObject healthBars;
    // Record how many wrong times currently
    public int countWrong;
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
        healthBars = GameObject.FindGameObjectWithTag("HealthBars");
        shields = GameObject.FindGameObjectWithTag("Shields");
        //shieldBreak = shields.transform.GetComponent<AudioSource>();
        // Find the current sprite
        healthBarSR = healthBars.transform.GetComponent<SpriteRenderer>();
        shieldSR = shields.transform.GetComponent<SpriteRenderer>();
        // Reset countWrong
        countWrong = 0;
        // Set initial sprite
        healthBarSR.sprite = Resources.Load("Art/Textures/healthBarFull", typeof(Sprite)) as Sprite;
        shieldSR.sprite = Resources.Load("Art/Textures/shieldsFull", typeof(Sprite)) as Sprite;
    }

    public void AddCountWrong()
    {
        countWrong += 1;
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
                Debug.Log("This shouldn't happen casue player should have only 3 chances.");
                break;
        }
    }

    public int UpdateCountWrong()
    {
        return countWrong;
    }

    // Update is called once per frame
    void Update()
    {
        UpdateShieldState(UpdateCountWrong());
    }
}
