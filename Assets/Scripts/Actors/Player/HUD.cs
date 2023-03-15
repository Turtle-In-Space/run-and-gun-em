using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class HUD : MonoBehaviour
{
    public static HUD instance;

    [SerializeField] private TextMeshProUGUI bulletText;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private Slider HBslider;

    private Animator heartAnimator;

    readonly float baseHeartSpeed = 2f;


    private void Awake()
    {
        instance = this;
        heartAnimator = transform.GetChild(0).GetChild(2).gameObject.GetComponent<Animator>();
    }

    private void Start()
    {
        heartAnimator.speed = baseHeartSpeed;
        AddScore(0);
    }

    public int SetAmmoCount(int newAmmo)
    {
        bulletText.text = newAmmo.ToString() + "/21";

        return newAmmo;
    }

    /*
     * Gör så att hjätat slår snabbare ju mindre hp
     */
    public void SetHealth(int currentHP)
    {
        HBslider.value = currentHP;

        heartAnimator.speed = (float)(baseHeartSpeed + (Mathf.Abs(currentHP - GameData.MaxPlayerHP) * 0.3f));
    }

    public void AddScore(int amount)
    {
        GameData.Score += amount;
        scoreText.text = GameData.Score.ToString();
    }
}
