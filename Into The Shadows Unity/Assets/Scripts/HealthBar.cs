using UnityEngine;
using UnityEngine.UI;  

public class HealthBar : MonoBehaviour
{
    public PlayerInfection PlayerInfection;
    public Image healthBarImage; //health bar
    public Image infectionBarImage; //infection bar
    public float maxHealth = 100f;
    public float currentHealth;
    
    // Infection variables
    public float maxInfection = 100f;  // Max infection amount
    public 

    void Start()
    {
        currentHealth = maxHealth;  // Initialize the health
        UpdateHealthBar();  //Update health bar to match the initial health
        UpdateInfectionBar(0);  //start infection at 0
    }

    public void TakeDamage(float damage)//function that will decrease health when button is clicked
    {
        currentHealth -= damage;
        currentHealth = Mathf.Clamp(currentHealth, 0f, maxHealth);  // Ensure health doesn't go below 0
        UpdateHealthBar();  // Update the health bar
    }

    public void StartInfection()
    {
        // PlayerInfection.currentInfection += 10f;  //
        // PlayerInfection.currentInfection = Mathf.Clamp(PlayerInfection.currentInfection, 0f, maxInfection);  

        // if (PlayerInfection.currentInfection >= maxInfection)
        // {
        //     ApplyInfectionDamage();//applies infection damage
        // }

        // UpdateInfectionBar(PlayerInfection.currentInfection);//updates infection bar filling
    }

    public void StopInfection()
    {
        PlayerInfection.currentInfection = 0f;
        UpdateInfectionBar((int)PlayerInfection.currentInfection);//fixes filling to empty
    }

    public void UpdateHealthBar()
    {
        // Set the fill amount based on the current health
        healthBarImage.fillAmount = currentHealth / maxHealth;
    }

    // Function to update the infection bar fill amount
    public void UpdateInfectionBar(float infection)
    {
        // Set the fill amount based on current infection
        //PlayerInfection.PlayerInfection.currentInfection = infection;
        infectionBarImage.fillAmount =  infection / maxInfection;
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
