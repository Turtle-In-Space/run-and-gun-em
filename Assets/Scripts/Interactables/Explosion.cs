using UnityEngine;

public class Explosion : MonoBehaviour
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
