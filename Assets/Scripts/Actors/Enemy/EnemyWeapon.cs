using UnityEngine;

public class EnemyWeapon : MonoBehaviour
{
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject bulletPrefab;

    private Animator animator;
    private ParticleSystem particleGunSmoke;

    private readonly float bulletForce = 40;
    private readonly float bulletDelay = 0.3f;
    private readonly float spreadMultiplier = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 15;
    

    private void Awake()
    {
        particleGunSmoke = transform.GetChild(0).GetChild(0).GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
        animator.GetBehaviour<EnemyCallReload>().enemyWeapon = this;
    }

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
            animator.SetBool("isShooting", false);
            animator.SetTrigger("Reload");
            AudioManager.instance.Play("PistolReload");
        }
    }

    public void OnReloadFinished()
    {
        ammoCount = 15;
    }
}
