using System.Collections.Generic;
using UnityEngine;
using TMPro; // Make sure to include this for TextMeshPro
using UnityEditor.SceneManagement;
using UnityEngine.SceneManagement;

public class EnemyTracker : MonoBehaviour
{
    public TextMeshProUGUI enemiesLeftText; // Reference to the TextMeshPro text displaying the number of enemies left
    public GameObject winMessage; // The GameObject to activate when all enemies are destroyed

    public List<GameObject> enemies; // List of enemy GameObjects with TankExplode script

    void Start()
    {
        // Find all GameObjects with the TankExplode script and add them to the enemies list
        enemies = new List<GameObject>();
        foreach (TankExplode tank in FindObjectsByType<TankExplode>(FindObjectsSortMode.None))
        {
            enemies.Add(tank.gameObject);
        }

        // Ensure the winMessage is initially inactive
        if (winMessage != null)
        {
            winMessage.SetActive(false);
        }

        // Update the enemies left text at the start
        UpdateEnemiesLeftText();
    }

    void Update()
    {
        // Remove any null entries from the list (i.e., destroyed enemies)
        enemies.RemoveAll(enemy => enemy == null);

        // Update the enemies left text
        UpdateEnemiesLeftText();

        // Check if all enemies are destroyed
        if (enemies.Count == 0)
        {
            if (winMessage != null)
            {
                winMessage.SetActive(true);
            }
            Invoke("LoadWin", 3f);
        }
    }
    public void LoadWin()
    {
        SceneManager.LoadScene("WinScene");
    }
    public void resetDemoScene()
    {
        SceneManager.LoadScene("Demo1");
    }
    // Updates the text to show the current number of enemies left
    private void UpdateEnemiesLeftText()
    {
        if (enemiesLeftText != null)
        {
            enemiesLeftText.text = enemies.Count.ToString();
        }
    }
}
