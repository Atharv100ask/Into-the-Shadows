using UnityEngine;
using TMPro; // For TextMeshPro UI

public class MissionCompleteTrigger : MonoBehaviour
{
    public TextMeshProUGUI missionText; // Reference to the UI text

    private void OnTriggerEnter(Collider other)
    {
        // Check if the player is the one that collided with the trigger
        if (other.CompareTag("Player"))  // Ensure the player has the "Player" tag
        {
            // Show "Mission Complete" text in the UI
            missionText.text = "Mission Complete";
            missionText.enabled = true; // Enable the TextMeshPro UI element

            // Pause the game
            Time.timeScale = 0f;

            // Optionally, you can trigger any other end-of-game behavior here, like showing a menu
            // For example, you might show a "Play Again" or "Exit" button on the UI
        }
    }
}
