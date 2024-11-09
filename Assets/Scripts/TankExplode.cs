using UnityEngine;
using UnityEngine.AI;

public class TankExplode : MonoBehaviour
{
    public GameObject Head; // The GameObject to which the force will be applied
    public float upwardForce = 5f;
    public AudioClip ExplosionClip;
    public GameObject explosion1;
    public GameObject explosion2;
    public AudioSource explosionSnd;
    private NavMeshAgent navMeshAgent;

    [Header("Force Settings")]
    [SerializeField] private float minForce = 10f;
    [SerializeField] private float maxForce = 30f;
    [SerializeField] private bool affectChildren = true;
    [SerializeField] private ForceMode forceMode = ForceMode.Impulse;

    [Header("Direction Settings")]
    [SerializeField] private float minVerticalAngle = -45f;
    [SerializeField] private float maxVerticalAngle = 45f;
    [SerializeField] private bool restrictHorizontalAngle = false;
    [SerializeField] private float minHorizontalAngle = 0f;
    [SerializeField] private float maxHorizontalAngle = 360f;

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
            ApplyRandomForces();
            explosion1.SetActive(true);
            explosion2.SetActive(true);
            explosionSnd.PlayOneShot(ExplosionClip);
            Destroy(gameObject, 3f);
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
    public void ApplyRandomForces()
    {
        // Apply to self if no children are to be affected
        if (!affectChildren)
        {
            SetupAndApplyForce(gameObject);
            return;
        }

        // Apply to all children
        foreach (Transform child in transform)
        {
            SetupAndApplyForce(child.gameObject);
        }
    }

    private void SetupAndApplyForce(GameObject obj)
    {
        // Add Rigidbody if it doesn't exist
        Rigidbody rb = obj.GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = obj.AddComponent<Rigidbody>();
        }

        // Add BoxCollider if it doesn't exist
        BoxCollider boxCollider = obj.GetComponent<BoxCollider>();
        if (boxCollider == null)
        {
            boxCollider = obj.AddComponent<BoxCollider>();
        }

        // Generate random direction
        Vector3 randomDirection = GetRandomDirection();

        // Generate random force magnitude
        float forceMagnitude = Random.Range(minForce, maxForce);

        // Apply force
        rb.AddForce(randomDirection * forceMagnitude, forceMode);
    }

    private Vector3 GetRandomDirection()
    {
        // Generate random vertical angle within constraints
        float verticalAngle = Random.Range(minVerticalAngle, maxVerticalAngle);

        // Generate random horizontal angle
        float horizontalAngle;
        if (restrictHorizontalAngle)
        {
            horizontalAngle = Random.Range(minHorizontalAngle, maxHorizontalAngle);
        }
        else
        {
            horizontalAngle = Random.Range(0f, 360f);
        }

        // Convert angles to direction vector
        Vector3 direction = Quaternion.Euler(verticalAngle, horizontalAngle, 0f) * Vector3.forward;
        return direction.normalized;
    }
}
