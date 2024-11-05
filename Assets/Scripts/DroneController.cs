using UnityEngine;
using UnityEngine.UI;
public class DroneController : MonoBehaviour
{
    public float maxSpeed = 10f;
    public float accelerationTime = 2f;
    public float rotationSpeed = 100f;
    public Transform cameraTransform;
    public float shakeIntensity = 0.1f;
    public float shakeDuration = 0.1f;

    private Vector3 originalCameraPosition;
    private float shakeTimer;
    private float currentSpeed = 0f;
    private float accelerationRate;

    public GameObject projectilePrefab; // The prefab to instantiate
    public Transform launchPosition; // The position to instantiate the prefab
    public float launchForce = 500f; // The force applied to the instantiated prefab
    public float spawnCooldown = 2f; // Cooldown time between spawns
    private float nextSpawnTime = 0f; // Time when the next spawn is allowed
    public Image cooldownProgressBar;

    void Start()
    {
        // Calculate the acceleration rate
        accelerationRate = maxSpeed / accelerationTime;

        // Store the original position of the camera
        if (cameraTransform != null)
        {
            originalCameraPosition = cameraTransform.localPosition;
        }
    }

    void Update()
    {
        UpdateCooldownProgressBar();
        if (Input.GetKeyDown(KeyCode.Space))
        {
            LaunchProjectile();
        }
    }

    // Method to accelerate and move the drone forward
    public void MoveForward()
    {
        Accelerate();
        transform.Translate(Vector3.forward * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to accelerate and move the drone backward
    public void MoveBackward()
    {
        Accelerate();
        transform.Translate(Vector3.back * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to accelerate and move the drone left
    public void MoveLeft()
    {
        Accelerate();
        transform.Translate(Vector3.left * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to accelerate and move the drone right
    public void MoveRight()
    {
        Accelerate();
        transform.Translate(Vector3.right * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to accelerate and move the drone upward
    public void MoveUp()
    {
        Accelerate();
        transform.Translate(Vector3.up * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to accelerate and move the drone downward
    public void MoveDown()
    {
        Accelerate();
        transform.Translate(Vector3.down * currentSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to rotate the drone left
    public void RotateLeft()
    {
        transform.Rotate(Vector3.up, -rotationSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to rotate the drone right
    public void RotateRight()
    {
        transform.Rotate(Vector3.up, rotationSpeed * Time.deltaTime);
        TriggerCameraShake();
    }

    // Method to handle acceleration
    private void Accelerate()
    {
        if (currentSpeed < maxSpeed)
        {
            currentSpeed += accelerationRate * Time.deltaTime;
            currentSpeed = Mathf.Min(currentSpeed, maxSpeed); // Clamp to max speed
        }
    }

    // Method to trigger camera shake
    private void TriggerCameraShake()
    {
        if (cameraTransform != null)
        {
            shakeTimer = shakeDuration;
        }
    }

    void LateUpdate()
    {
        if (cameraTransform != null && shakeTimer > 0)
        {
            shakeTimer -= Time.deltaTime;
            Vector3 shakeOffset = Random.insideUnitSphere * shakeIntensity;
            cameraTransform.localPosition = originalCameraPosition + shakeOffset;
        }
        else if (cameraTransform != null)
        {
            // Reset camera position when shake is finished
            cameraTransform.localPosition = originalCameraPosition;
        }
    }

    // New method to instantiate and launch the prefab with a cooldown
    public void LaunchProjectile()
    {
        // Check if the cooldown period has passed
        if (Time.time >= nextSpawnTime)
        {
            if (projectilePrefab != null && launchPosition != null)
            {
                // Instantiate the prefab at the launch position
                GameObject projectile = Instantiate(projectilePrefab, launchPosition.position, Quaternion.identity);

                // Add Rigidbody and apply force to launch it
                Rigidbody rb = projectile.GetComponent<Rigidbody>();
                if (rb == null)
                {
                    rb = projectile.AddComponent<Rigidbody>();
                }

                // Apply force towards the negative Y-axis
                rb.AddForce(Vector3.down * launchForce, ForceMode.Impulse);

                // Set the next spawn time
                nextSpawnTime = Time.time + spawnCooldown;
            }
        }
    }
    private void UpdateCooldownProgressBar()
    {
        if (cooldownProgressBar != null)
        {
            float cooldownRemaining = nextSpawnTime - Time.time;
            float progress = Mathf.Clamp01(1 - (cooldownRemaining / spawnCooldown));

            cooldownProgressBar.fillAmount = progress;
        }
    }
}
