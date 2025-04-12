using UnityEngine;

public class Food : MonoBehaviour
{

    public HealthBar status;
    private void OnCollisionEnter (Collision collision)
    {
        Destroy(gameObject);
        Debug.Log("Health: " + status.currentHealth);
        status.currentHealth += 25f;
        Debug.Log("Health: " + status.currentHealth);
        status.UpdateHealthBar();
    }
}
