using UnityEngine;
using UnityEngine.UI;

public class MineController : MonoBehaviour
{
    [SerializeField] private int mineDamage = 2;
    private ParticleSystem myParticleSystem;
    [SerializeField] private ParticleSystem explosionFX;
    private float timeToDestroy = 0.6f;

    private void OnEnable()
    {
        explosionFX.Stop();
    }

    private void OnTriggerEnter(Collider other)
    {
        ActivePlayerHealth activePlayerHealth = other.GetComponent<ActivePlayerHealth>();
        // if (activePlayerHealth != null && other.gameObject.tag == "Player01")
        // {
        //     activePlayerHealth.TakeDamage(mineDamage);
        //     gameObject.GetComponent<MeshRenderer>().enabled = false;
        //     explosionFX.Play();
        //     Destroy(gameObject, timeToDestroy);
        // }        
        if (other.gameObject.tag == "Player01")
        {
            activePlayerHealth.TakeDamage(mineDamage);
            gameObject.GetComponent<MeshRenderer>().enabled = false;
            explosionFX.Play();
            Destroy(gameObject, timeToDestroy);
        }
    }
}
