using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shield : MonoBehaviour
{

    // This variable receives values of how many time does the user wrongly answer.
    public int countWrong;

    public GameObject healthBarFull;
    public GameObject healthBar_2_3;
    public GameObject healthBar_1_3;
    public GameObject shield_full;
    public GameObject shield_2_3;
    public GameObject shield_1_3;
    public GameObject shield_destoryed;
    // Start is called before the first frame update
    void Start()
    {
        healthBarFull = transform.Find("HealthBar_full").gameObject;
        healthBar_2_3 = transform.Find("HealthBar_2_3").gameObject;
        healthBar_1_3 = transform.Find("HealthBar_1_3").gameObject;
        shield_full = GameObject.FindGameObjectWithTag("shield_full").gameObject;
        shield_2_3 = GameObject.FindGameObjectWithTag("shield_2_3").gameObject;
        shield_1_3 = GameObject.FindGameObjectWithTag("shield_1_3").gameObject;
        shield_destoryed = GameObject.FindGameObjectWithTag("shield_destoryed").gameObject;
    }

    public void UpdateHealthBar(int countWrong)
    {
        switch (countWrong)
        {
            case 0:
                shield_full.SetActive(true);
                shield_2_3.SetActive(false);
                shield_1_3.SetActive(false);
                shield_destoryed.SetActive(false);
                healthBarFull.SetActive(true);
                healthBar_2_3.SetActive(false);
                healthBar_1_3.SetActive(false);
                break;
            case 1:
                shield_full.SetActive(false);
                shield_2_3.SetActive(true);
                shield_1_3.SetActive(false);
                shield_destoryed.SetActive(false);
                healthBarFull.SetActive(false);
                healthBar_2_3.SetActive(true);
                healthBar_1_3.SetActive(false);
                break;
            case 2:
                shield_full.SetActive(false);
                shield_2_3.SetActive(false);
                shield_1_3.SetActive(true);
                shield_destoryed.SetActive(false);
                healthBarFull.SetActive(false);
                healthBar_2_3.SetActive(false);
                healthBar_1_3.SetActive(true);
                break;
            default:
                shield_full.SetActive(false);
                shield_2_3.SetActive(false);
                shield_1_3.SetActive(false);
                shield_destoryed.SetActive(true);
                healthBarFull.SetActive(false);
                healthBar_2_3.SetActive(false);
                healthBar_1_3.SetActive(false);
                break;


        } 
    }

    // Update is called once per frame
    void Update()
    {
        countWrong = 1;
        UpdateHealthBar(countWrong);
    }
}
