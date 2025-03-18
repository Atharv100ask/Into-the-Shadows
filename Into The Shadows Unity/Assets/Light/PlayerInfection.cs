using TMPro; // Make sure you have this namespace for TextMeshPro
using UnityEngine;
using UnityEngine.UI; // Needed for UI elements
public class PlayerInfection : MonoBehaviour
{
    public int infectionLevel = 0; // Current infection level
    public int maxInfection = 100; // Max infection level
    public TextMeshProUGUI gameOverText; // Use TextMeshProUGUI for TextMeshPro

    // Increase the infection level
    public void IncreaseInfection(int amount)
    {
        infectionLevel += amount;
        infectionLevel = Mathf.Clamp(infectionLevel, 0, maxInfection); // Prevent overflow

        if (infectionLevel >= maxInfection)
        {
            Debug.Log("Player is fully infected!");
            TriggerGameOver();
        }
    }

    // Trigger the Game Over UI
    private void TriggerGameOver()
    {
        if (gameOverText != null)
        {
            gameOverText.enabled = true; // Enable the Game Over text
        }
        // Optionally, pause the game or handle further logic here
        Time.timeScale = 0f; // Freeze time (game pause)
    }
}
