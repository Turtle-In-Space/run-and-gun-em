using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private Slider HBslider;
    [SerializeField] private Animator heartAnimator;


    private void Start()
    {
        SetAmmoCount(30);
        heartAnimator.speed = 1.2f;
        SetHealth(GameData.PlayerHP);
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
}
