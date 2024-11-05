using UnityEngine;

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
        // Example of calling methods based on external conditions
        // These methods should be called from another script or input handler
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
}
