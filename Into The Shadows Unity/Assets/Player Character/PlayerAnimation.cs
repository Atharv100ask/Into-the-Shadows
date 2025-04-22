using UnityEngine;
using System;
using System.Collections;

public class PlayerAnimation : MonoBehaviour {
    private Animator anim;
    private float moveInputX;   // Variable to store input from movement keys
    private float moveInputY;
    public Inventory inventory;
    public BatAttack bat;
    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    IEnumerator WaitForSwingThenReset()
    {
        anim.SetBool("Swing", true);
        bat.EnableDamage();

        yield return new WaitUntil(() => anim.GetCurrentAnimatorStateInfo(0).IsName("Swing"));

        yield return new WaitWhile(() => anim.GetCurrentAnimatorStateInfo(0).normalizedTime < 1f);

        anim.SetBool("Swing", false);
        bat.DisableDamage();
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

        if (Input.GetKeyDown(KeyCode.Mouse0) && inventory.currentItem == 1)
        {
            StartCoroutine(WaitForSwingThenReset());
        }

        // Check if there's any movement in either axis (left/right or forward/backward)
        if ((moveInputY != 0))
        {  
            anim.SetBool("isRunning", true);
        }
        else
        {
            anim.SetBool("isRunning", false); // Transition back to idle
        }
        //StrafeLeft
        if (moveInputX < -0.1f)
        {  
            anim.SetBool("StrafeLeft", true);
        }
        else
        {
            anim.SetBool("StrafeLeft", false); // Transition back to idle
        }
        //StrafeRight
        if (moveInputX > 0.1f)
        {  
            anim.SetBool("StrafeRight", true);
        }
        else
        {
            anim.SetBool("StrafeRight", false); // Transition back to idle
        }
        //jumping
        if (Input.GetKeyDown(KeyCode.Space) == true) //&& (PlayerMovement.isGrounded == false))
        {
            anim.SetTrigger("isJumping");
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
