using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject gunSmoke;

    private Animator animator;
    private ParticleSystem gunSmokeParticleSys;

    private readonly float bulletSpreadMultiplier = 0.18f;
    private readonly float bulletDelay = 0.2f;
    private readonly int bulletForce = 50;
    private float lastShot = 0;
    private int ammoCount = 30;


    private void Awake()
    {
        animator = GetComponent<Animator>();
        gunSmokeParticleSys = gunSmoke.GetComponent<ParticleSystem>();
        animator.GetBehaviour<CallReload>().playerWeapon = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammoCount != 30)
        {
            animator.SetTrigger("Reload");
            ammoCount = HUD.instace.SetAmmoCount(0);
            AudioManager.instance.Play("ARReload");
        }
    }

    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1") && ammoCount > 0)
        {
            animator.SetBool("isFiring", true);

            //Delay mellan skott
            if (Input.GetButton("Fire1") && Time.time - lastShot > bulletDelay)
            {
                lastShot = Time.time;
                Shoot();
                ammoCount = HUD.instace.SetAmmoCount(ammoCount - 1);
            }
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    private void Shoot()
    {
        Vector2 bulletDirection = new Vector2(firePoint.right.x + Random.Range(-bulletSpreadMultiplier, bulletSpreadMultiplier), firePoint.right.y + Random.Range(-bulletSpreadMultiplier, bulletSpreadMultiplier)).normalized;
        float angle = Mathf.Atan2(bulletDirection.y, bulletDirection.x) * Mathf.Rad2Deg;
        Quaternion bulletRotation = Quaternion.AngleAxis(angle, Vector3.forward);

        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, bulletRotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();       
        rigidbody.AddForce(bulletDirection * bulletForce, ForceMode2D.Impulse);

        gunSmokeParticleSys.Play();
        AudioManager.instance.Play("ARShot");
    }

    //Kallas från Animator
    public void OnReloadFinished()
    {
        ammoCount = HUD.instace.SetAmmoCount(30);
    }
}
