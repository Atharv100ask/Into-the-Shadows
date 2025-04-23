using UnityEngine;

public class Pickup : MonoBehaviour
{

    public HealthBar status;
    public Inventory inventory;
    private void OnCollisionEnter (Collision collision)
    {
        Destroy(gameObject);

        if(gameObject.tag == "Food")
        {
            Debug.Log("Food Picked up");
            inventory.food += 1;
            inventory.UpdateItemCount();
        }
        else if(gameObject.tag == "Ammo")
        {
            Debug.Log("Ammo Picked up");
            inventory.ammo += 6;
            inventory.UpdateItemCount();
        }
        else if(gameObject.tag == "Syringe")
        {
            Debug.Log("Stabilizer Picked up");
            inventory.stabilizers += 1;
            inventory.UpdateItemCount();
        }
        else if (gameObject.tag == "Bat")
        {
            Debug.Log("Bat picked up");
            inventory.hasMelee = true;
        }
        else if (gameObject.tag == "Gun")
        {
            Debug.Log("Gun picked up");
            inventory.hasGun = true;
        }
        // Debug.Log("Health: " + status.currentHealth);
        // status.currentHealth += 25f;
        // Debug.Log("Health: " + status.currentHealth);
        // status.UpdateHealthBar();
        inventory.ItemPickupSound();
    }
}
