using UnityEngine;

public class BatAttack : MonoBehaviour
{
    public int damage = 20;
    private bool canDamage = false;

    void OnTriggerEnter(Collider other)
    {
        Debug.Log("Check");
        Debug.Log(canDamage);
        if (canDamage && other.CompareTag("Zombie"))
        {
            Debug.Log("Hit");
            ZombieHealthBar zombie = other.GetComponentInChildren<ZombieHealthBar>();
            if (zombie != null)
            {
                Debug.Log("Check");
                zombie.TakeDamage(damage);
                DisableDamage();
            }
        }
    }

    public void EnableDamage()
    {
        canDamage = true;
    }

    public void DisableDamage()
    {
        canDamage = false;
    }
}
