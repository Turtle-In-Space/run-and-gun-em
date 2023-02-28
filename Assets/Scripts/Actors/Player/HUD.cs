using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instace;

    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider HBslider;

    private Animator heartAnimator;

    private void Awake()
    {
        instace = this;
        heartAnimator = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        heartAnimator.speed = 1.2f;        
        AddScore(0);
    }

    public int SetAmmoCount(int newAmmo)
    {
        bulletText.text = newAmmo.ToString() + "/21";

        return newAmmo;
    }

    public void SetHealth(int currentHP)
    {
        HBslider.value = currentHP;

        heartAnimator.speed = (float)(1.2f + Mathf.Abs(currentHP - 10) * 0.2);
    }

    public void AddScore(int amount)
    {
        GameData.Score += amount;
        scoreText.text = GameData.Score.ToString();
    }
}
