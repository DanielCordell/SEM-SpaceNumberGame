using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{
    public Image healthBar;
    public int countWrong;
    // Start is called before the first frame update
    void Start()
    {
        healthBar = GetComponent<Image>();
       
    }
    public void UpdateHealthBar(int countWrong)
    {
        switch (countWrong)
        {
            case 0:
                break;
            case 1:
                healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(140 * 2 / 3, 20);
                break;
            case 2:
                healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(140 * 1 / 3, 20);
                break;
            default:
                healthBar.GetComponent<RectTransform>().sizeDelta = new Vector2(0 , 20);
                break;


        } 
    }
    // Update is called once per frame
    void Update()
    {
        countWrong = 2;
        UpdateHealthBar(countWrong);
    }
}
