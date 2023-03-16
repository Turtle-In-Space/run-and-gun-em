using UnityEngine;

public class Explosion : MonoBehaviour
{
    [SerializeField] private Animator explosion;

    private void Start()
    {
        if (explosion)
        {
            explosion.Play("Explosion");
        }
    }

    public void OnExplosionFinished()
    {
        Destroy(gameObject);
    }
    
}
