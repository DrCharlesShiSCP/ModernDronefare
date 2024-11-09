using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class DroneCollision : MonoBehaviour
{
    public GameObject Normal1;
    public GameObject Normal2;
    public GameObject Normal3;
    public GameObject Static1;
    public GameObject Static2;
    public GameObject Static3;
    public GameObject DestructionText;
    public GameObject resetButton;
    public GameObject resPawnButton;
    public GameObject Drone;
    public GameObject SpawnPosition;
    public int livesLeft;
    public TextMeshProUGUI liveText;
    public float explosionForce = 500f;
    public GameObject ExplosionEffect;
    public float countdownTime = 3f;
    public TextMeshProUGUI countdownText;
    private float currentTime;
    private GameObject[] failObjects;
    private bool isCountingDown = false;
    private bool CollidedAlreday = false;
    private Rigidbody rb;

    public AudioClip metalPipe;
    public AudioSource pipeSource;
    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        resPawnButton.SetActive(false);
        resetButton.SetActive(false);
        DestructionText.SetActive(false);
        failObjects = GameObject.FindGameObjectsWithTag("failroom");
        updateLivesText();
        if (Static1 != null) Static1.SetActive(false);
        if (Static2 != null) Static2.SetActive(false);
        if (Static3 != null) Static3.SetActive(false);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!CollidedAlreday)
        {
            livesLeft--;
            CollidedAlreday=true;
        }
        updateLivesText();

        if (Normal1 != null) Normal1.SetActive(false);
        if (Normal2 != null) Normal2.SetActive(false);
        if (Normal3 != null) Normal3.SetActive(false);

        if (Static1 != null) Static1.SetActive(true);
        if (Static2 != null) Static2.SetActive(true);
        if (Static3 != null) Static3.SetActive(true);

        if (livesLeft <= 0 && !isCountingDown)
        {
            isCountingDown = true;
            currentTime = countdownTime;
            DestructionText.SetActive(true);
        }
        resPawnButton.SetActive(true);
    }

    public void SpawnAnotherDrone()
    {
        // Reset the objects' states as needed
        if (Normal1 != null) Normal1.SetActive(true);
        if (Normal2 != null) Normal2.SetActive(true);
        if (Normal3 != null) Normal3.SetActive(true);

        if (Static1 != null) Static1.SetActive(false);
        if (Static2 != null) Static2.SetActive(false);
        if (Static3 != null) Static3.SetActive(false);
        rb.useGravity = false;
        gameObject.transform.position = SpawnPosition.transform.position;
        gameObject.transform.rotation = SpawnPosition.transform .rotation;
        isCountingDown = false;
        currentTime = countdownTime;
        DestructionText.SetActive(false);
        CollidedAlreday = false;
        resPawnButton.SetActive(false);
    }

    public void updateLivesText()
    {
        if (liveText != null)
        {
            liveText.text = livesLeft.ToString();
        }
    }

    private void Update()
    {
        if (isCountingDown && currentTime > 0)
        {
            currentTime -= Time.deltaTime;
            UpdateCountdownText();

            if (currentTime <= 0)
            {
                currentTime = 0;
                ExplodeEverything();
            }
        }
    }

    private void UpdateCountdownText()
    {
        if (countdownText != null)
        {
            int seconds = Mathf.CeilToInt(currentTime);
            countdownText.text = "THEY WILL FIND YOU IN " + seconds.ToString();
        }
    }

    public void ExplodeEverything()
    {
        foreach (GameObject tagged in failObjects)
        {
            if (ExplosionEffect != null)
            {
                Instantiate(ExplosionEffect, tagged.transform.position, Quaternion.identity);
            }

            Rigidbody rb = tagged.GetComponent<Rigidbody>();
            if (rb == null)
            {
                rb = tagged.AddComponent<Rigidbody>();
            }

            rb.useGravity = true;

            Vector3 randomDirection = new Vector3(
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f),
                Random.Range(-1f, 1f)
            ).normalized;

            rb.AddForce(randomDirection * explosionForce, ForceMode.Impulse);
        }
        pipeSource.PlayOneShot(metalPipe);
        isCountingDown = false;
        Invoke("ActivateResetButton", 3f);
    }
    public void ActivateResetButton()
    {
        resetButton.SetActive(true);
    }
}
