using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;

    private Transform firePoint;
    private Animator animator;
    private ParticleSystem gunSmokeParticleSys;

    private readonly float bulletSpreadMultiplier = 0.09f;
    private readonly float bulletDelay = 0.2f;
    private readonly int bulletForce = 50;
    private readonly int maxAmmoCount = 21;
    private float lastShot = 0;
    private int currentAmmo = 21;


    private void Awake()
    {
        firePoint = transform.GetChild(0);

        gunSmokeParticleSys = firePoint.GetChild(0).gameObject.GetComponent<ParticleSystem>();
        animator = GetComponent<Animator>();
    }

    private void Start()
    {
        HUD.instance.SetAmmoCount(maxAmmoCount);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && currentAmmo != maxAmmoCount && !animator.GetBool("isReloading"))
        {
            Reload();
        }
    }

    /*
     * Kollar om man kan skjuta
     */
    private void FixedUpdate()
    {
        if (Input.GetButton("Fire1"))
        {
            if (currentAmmo != 0)
            {
                animator.SetBool("isFiring", true);

                //Delay mellan skott
                if (Time.time - lastShot > bulletDelay)
                {
                    lastShot = Time.time;
                    Shoot();
                    currentAmmo = HUD.instance.SetAmmoCount(currentAmmo - 1);
                }
            }
            else
            {
                AudioManager.instance.Play("AREmpty");
            }
            
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    /*
     * Kallas från Animation
     * Ger ammo
     */
    public void OnReloadFinished()
    {
        animator.SetBool("isReloading", false);
        currentAmmo = HUD.instance.SetAmmoCount(maxAmmoCount);
    }

    /*
     * Skapar ett skott
     * Spelar bild och ljud effekter
     */
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

    /*
     * Startar animationer
     * Tar bort ammo
     */
    private void Reload()
    {
        animator.SetBool("isReloading", true);
        currentAmmo = HUD.instance.SetAmmoCount(0);
        AudioManager.instance.Play("ARReload");
    }
}
