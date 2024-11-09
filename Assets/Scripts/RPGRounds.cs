using UnityEngine;

public class RPGRounds : MonoBehaviour
{
    public GameObject collisionEffect; // The effect to spawn upon collision
    public float destroyDelay = 2f; // Delay before destroying the projectile
    public AudioClip bombSound;
    public AudioSource bombsoundSource;

    private void OnCollisionEnter(Collision collision)
    {
        Debug.LogWarning("hitGround");
        // Spawn the collision effect at the point of impact
        if (collisionEffect != null)
        {
            Instantiate(collisionEffect, collision.contacts[0].point, Quaternion.identity);
        }
        bombsoundSource.PlayOneShot(bombSound);

        // Destroy the projectile after the specified delay
        Destroy(gameObject, destroyDelay);
    }
}
