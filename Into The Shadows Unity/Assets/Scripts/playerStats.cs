using UnityEngine;
using UnityEngine.UI;  

public class HealthBar : MonoBehaviour
{
    public Image healthBarImage; //health bar
    public Image infectionBarImage; //infection bar
    public float maxHealth = 100f;
    private float currentHealth;
    
    // Infection variables
    public float maxInfection = 100f;  // Max infection amount
    private float currentInfection = 0f; 

    void Start()
    {
        currentHealth = maxHealth;  // Initialize the health
        UpdateHealthBar();  //Update health bar to match the initial health
        UpdateInfectionBar();  //start infection at 0
    }

    public void TakeDamage(float damage)//function that will decrease health when button is clicked
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Ensure health doesn't go below 0
        UpdateHealthBar();  // Update the health bar
    }

    public void StartInfection()
    {
        currentInfection += 10f;  //
        currentInfection = Mathf.Clamp(currentInfection, 0f, maxInfection);  

        if (currentInfection >= maxInfection)
        {
            ApplyInfectionDamage();//applies infection damage
        }

        UpdateInfectionBar();//updates infection bar filling
    }

    public void StopInfection()
    {
        currentInfection = 0f;
        UpdateInfectionBar();//fixes filling to empty
    }

    private void UpdateHealthBar()
    {
        // Set the fill amount based on the current health
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }

    // Function to update the infection bar fill amount
    private void UpdateInfectionBar()
    {
        // Set the fill amount based on current infection
        infectionBarImage.fillAmount = currentInfection / maxInfection;
    }

    private void ApplyInfectionDamage() //damage is inflicted when we have max infection rate
    {
        currentHealth -= 10f;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  
        UpdateHealthBar();  

        if (currentHealth <= 0f)
        {
            StopInfection();  //Stop the infection when the player dies
           
        }
    }
}
