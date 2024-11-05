using UnityEngine;

public class TankExplode : MonoBehaviour
{
    public GameObject Head; // The GameObject to which the force will be applied
    public float upwardForce = 500f;

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the colliding object is tagged "Projectiles"
        if (collision.gameObject.CompareTag("Projectiles"))
        {
            ApplyUpwardForce();
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
}
