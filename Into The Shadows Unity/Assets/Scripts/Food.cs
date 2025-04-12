using UnityEngine;

public class Food : MonoBehaviour
{

    public HealthBar status;
    public Inventory inventory;
    private void OnCollisionEnter (Collision collision)
    {
        Destroy(gameObject);
        // Debug.Log("Health: " + status.currentHealth);
        // status.currentHealth += 25f;
        // Debug.Log("Health: " + status.currentHealth);
        // status.UpdateHealthBar();
        inventory.food += 1;
        inventory.UpdateItemCount();
    }
}
