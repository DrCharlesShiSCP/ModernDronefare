using UnityEngine;
using UnityEngine.AI;

public class TankExplode : MonoBehaviour
{
    public GameObject Head; // The GameObject to which the force will be applied
    public float upwardForce = 30f;
    public AudioClip ExplosionClip;
    public GameObject explosion1;
    public GameObject explosion2;
    public AudioSource explosionSnd;
    private NavMeshAgent navMeshAgent;

    private void Start()
    {
        explosion1.SetActive(false);
        explosion2.SetActive(false);
    }
    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is tagged "Projectiles"
        if (collision.gameObject.CompareTag("Projectiles"))
        {
            ApplyUpwardForce();
            explosion1.SetActive(true);
            explosion2.SetActive(true);
            explosionSnd.PlayOneShot(ExplosionClip);
            Destroy(gameObject, 5f);
            StopTankMovement();
        }
    }

    private void ApplyUpwardForce()
    {
        if (Head != null)
        {
            // Add a Rigidbody component if one is not already present
            Rigidbody rb = Head.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = Head.AddComponent<Rigidbody>();
            }

            // Enable gravity and apply upward force
            rb.useGravity = true;
            rb.AddForce(Vector3.up * upwardForce, ForceMode.Impulse);
        }
    }
    private void StopTankMovement()
    {
        // Disable the NavMeshAgent if it exists
        if (navMeshAgent != null)
        {
            navMeshAgent.isStopped = true;
        }

        GetComponent<EnemyPatrol>().enabled = false;
    }
}
