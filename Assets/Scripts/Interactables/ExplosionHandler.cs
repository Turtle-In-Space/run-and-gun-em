using UnityEngine;

public class ExplosionHandler : MonoBehaviour
{
    [SerializeField] private Animator explosion;

    private void Start()
    {
        explosion.Play("Explosion");
    }

    public void OnExplosionFinished()
    {
        Destroy(gameObject);
    }
    
}
