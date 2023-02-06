using UnityEngine;


public class PlayerWeapon : MonoBehaviour
{
    [SerializeField] private GameObject bulletPrefab;
    [SerializeField] private Transform firePoint;
    [SerializeField] private Animator animator;
    [SerializeField]private HUD playerHUD;

    private readonly int bulletForce = 45;
    private readonly float bulletDelay = 0.2f;
    private float lastShot = 0;
    private int ammoCount = 30;


    private void Start()
    {
        animator.GetBehaviour<CallReload>().playerWeapon = this;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.R) && ammoCount != 30)
        {
            animator.SetTrigger("Reload");
            ammoCount = playerHUD.SetAmmoCount(0);            
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
                ammoCount = playerHUD.SetAmmoCount(ammoCount - 1);
            }
        }
        else
        {
            animator.SetBool("isFiring", false);
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.AddForce(firePoint.right * bulletForce, ForceMode2D.Impulse);

        //TODO: lägg in bullet spread
    }

    //Kallas från Animator
    public void OnReloadFinished()
    {
        ammoCount = playerHUD.SetAmmoCount(30);
    }
}
