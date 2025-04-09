using UnityEngine;
using System;

public class PlayerAnimation : MonoBehaviour {
    private Animator anim;
    private float moveInputX;   // Variable to store input from movement keys
    private float moveInputY;
    void Start() {
        // Get an instance of the Animator component attached to the character.
        anim = GetComponent<Animator>();
    }

    void Update() {
        moveInputX = Input.GetAxisRaw("Horizontal"); // A/D or Left/Right Arrow keys
        moveInputY = Input.GetAxisRaw("Vertical");   // W/S or Up/Down Arrow keys

        // Check if there's any movement in either axis (left/right or forward/backward)
        if (moveInputX != 0 || moveInputY != 0)
        {
            anim.SetBool("isRunning", true); // Trigger running animation
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
