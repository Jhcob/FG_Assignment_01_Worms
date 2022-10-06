using UnityEngine;

public class BulletController : MonoBehaviour
{
    [SerializeField] private ParticleSystem shootImpactFX, shootFX;
    [SerializeField] private float speed = 50f;
    private float timeToDestroy = 0.3f;
    public Vector3 target { get; set; }
    public bool hit { get; set; }
    
    private ParticleSystem myParticleSystem;

    private void OnEnable()
    {
        shootImpactFX.Stop();
    }

    void Update()
    {
        BulletMovement();
    }

    private void BulletMovement()
    {
        transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);
        if (!hit && Vector3.Distance(transform.position, target) < 1f)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        shootFX.Stop();
        shootImpactFX.Play();
        Destroy(gameObject, timeToDestroy);
    }
}
