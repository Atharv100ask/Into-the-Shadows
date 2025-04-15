using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour {
    private Animator anim;
    private float moveInputX;   // Variable to store input from movement keys
    private float moveInputY;
    public Inventory inventory;
    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        moveInputX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow keys
        moveInputY = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow keys

        if (inventory.currentItem == 1)
        {
            anim.SetBool("hasMelee", true);
            anim.SetBool("hasGun", false);
            anim.SetBool("hasConsumable", false);
        }
        else if (inventory.currentItem == 2)
        {
            anim.SetBool("hasMelee", false);
            anim.SetBool("hasGun", true);
            anim.SetBool("hasConsumable", false);
        }
        else if (inventory.currentItem == 3 || inventory.currentItem == 4 || inventory.currentItem == 5){
            anim.SetBool("hasMelee", false);
            anim.SetBool("hasGun", false);
            anim.SetBool("hasConsumable", true);
        }
        else
        {
            anim.SetBool("hasMelee", false);
            anim.SetBool("hasConsumable", false);
            anim.SetBool("hasGun", false);
        }

        // Check if there's any movement in either axis (left/right or forward/backward)
        if (moveInputX != 0 || moveInputY != 0)
        {  
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false); // Transition back to idle
        }

        if((HealthBar.currentInfection > 9) && (Math.Abs(HealthBar.currentInfection % 10) < 0.0001f))
        {
            anim.SetTrigger("infection_10");
        }

        if(Math.Abs(HealthBar.currentInfection - 75) < 0.0001f)
        {
            anim.SetTrigger("infection_75");
        }
    }
}
