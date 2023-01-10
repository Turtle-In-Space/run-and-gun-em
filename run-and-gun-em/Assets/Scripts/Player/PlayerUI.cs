using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerUI : MonoBehaviour
{
    //TODO HP bar
    private Text bulletText;
    private GameObject Healthbar;
    private Slider HBslider;
    private Animator heartAnimator;


    private void Awake()
    {
        bulletText = GameObject.Find("AmmoCount").GetComponent<Text>();
        Healthbar = GameObject.Find("HealthBar").gameObject;
        HBslider = Healthbar.GetComponent<Slider>();
        heartAnimator = Healthbar.transform.GetChild(2).GetComponent<Animator>();
    }

    private void Start()
    {
        SetMaxHP();
        SetAmmoCount(30);
        heartAnimator.speed = 1.2f;
    }

    public int SetAmmoCount(int newAmmo)
    {
        bulletText.text = newAmmo.ToString() + "/30";

        return newAmmo;
    }

    public void SetHealth(int currentHP)
    {
        HBslider.value = currentHP;

        heartAnimator.speed = (float)(1.2f + Mathf.Abs(currentHP - 10) * 0.2);
    }

    public void SetMaxHP()
    {
        HBslider.value = HBslider.maxValue;
    }
}
