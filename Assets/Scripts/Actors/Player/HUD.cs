using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instace;

    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider HBslider;
    [SerializeField] private Animator heartAnimator;

    private void Awake()
    {
        instace = this;
    }

    private void Start()
    {
        SetAmmoCount(30);
        heartAnimator.speed = 1.2f;
        SetHealth(GameData.PlayerHP);
        SetScore(0);
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

    public void SetScore(int amount)
    {
        GameData.Score += amount;
        scoreText.text = GameData.Score.ToString();
    }
}
