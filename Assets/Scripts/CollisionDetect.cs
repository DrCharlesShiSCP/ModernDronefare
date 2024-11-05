using UnityEngine;

public class CollisionDetect : MonoBehaviour
{
    private Rigidbody rb;

    void Start()
    {
        // Get the Rigidbody component attached to this GameObject
        rb = GetComponent<Rigidbody>();

        // Ensure gravity is initially turned off
        if (rb != null)
        {
            rb.useGravity = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        // Log the tag of the other object involved in the collision
        Debug.Log("Collided with object tagged: " + collision.gameObject.tag);

        // Enable gravity on the Rigidbody
        if (rb != null)
        {
            rb.useGravity = true;
        }
    }
}
