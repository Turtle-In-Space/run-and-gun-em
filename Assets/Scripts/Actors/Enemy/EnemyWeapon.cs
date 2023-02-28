using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Transform firePoint;
    private Animator animator;
    private ParticleSystem particleGunSmoke;

    private readonly float bulletForce = 40;
    private readonly float bulletDelay = 0.3f;
    private readonly float spreadMultiplier = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 15;
    

    private void Awake()
    {
        firePoint = transform.GetChild(0);
        particleGunSmoke = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    /*
     * Skapar ett skott
     * Lägger till ljud och bild effkter
     * Om inga skott finns, ladda om
     */
    public void Shoot()
    {
        if (Time.time - lastShot > bulletDelay && ammoCount > 0)
        {
            particleGunSmoke.Play();
            animator.SetBool("isShooting", true);
            lastShot = Time.time;
            ammoCount -= 1;

            Vector2 bulletDirection = new Vector2(firePoint.right.x + Random.Range(-spreadMultiplier, spreadMultiplier), firePoint.right.y + Random.Range(-spreadMultiplier, spreadMultiplier)).normalized;

            GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();
            rigidbody.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);

            AudioManager.instance.Play("PistolShot");
        }
        else if (ammoCount == 0)
        {
            Reload();
        }
    }

    public void OnReloadFinished()
    {
        ammoCount = 15;
    }

    /*
     * Bild och ljud effkter
     */
    private void Reload()
    {
        animator.SetBool("isShooting", false);
        animator.SetTrigger("Reload");
        AudioManager.instance.Play("PistolReload");
    }
}
