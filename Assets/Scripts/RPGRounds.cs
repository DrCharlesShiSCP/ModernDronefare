using UnityEngine;

public class RPGRounds : MonoBehaviour
{
    public GameObject collisionEffect; // The effect to spawn upon collision
    public float destroyDelay = 0f; // Delay before destroying the projectile

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("hitGround");
        // Spawn the collision effect at the point of impact
        if (collisionEffect != null)
        {
            Instantiate(collisionEffect, collision.contacts[0].point, Quaternion.identity);
        }

        // Destroy the projectile after the specified delay
        Destroy(gameObject, destroyDelay);
    }
}
