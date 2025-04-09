using UnityEngine;
using System.Collections;
public enum NPCState {
    Idle,
    TrackingPlayer,
    DialogueFear,
    DialogueUneasy,
    DialogueEmpathy,
    DialoguePostItemUse,
    DialogueNeutral
}

public class NPC : MonoBehaviour
{
    public DialogueUI dialogue;
    public float detectionRadius = 10f;
    private bool isPlayerNearby = false;
    private Transform nearbyPlayer;
    public HealthBar status;
    public NPCState currentState;
    private Animator animator;

    private bool hasGivenItem = false;
    private bool respondedToItemUse = false;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        CheckPlayerDistance();

        if (isPlayerNearby)
        {
            Vector3 targetDirection = nearbyPlayer.position - transform.position;
            targetDirection.y = 0;

            if (targetDirection != Vector3.zero)
            {
                Quaternion targetRotation = Quaternion.LookRotation(targetDirection);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, Time.deltaTime * 3f);
            }
            if(Input.GetKeyDown(KeyCode.E))
            {
                Debug.Log("E Pressed");
                UpdateStateBasedOnPlayer();
            }
        }
        else
        {
            currentState = NPCState.Idle;
        }
        
    }

    void CheckPlayerDistance()
    {
        GameObject playerObj = GameObject.FindGameObjectWithTag("Player");

        if (playerObj == null) return;

        float distance = Vector3.Distance(transform.position, playerObj.transform.position);

        if (distance < detectionRadius)
        {
            isPlayerNearby = true;
            nearbyPlayer = playerObj.transform;
        }
        else
        {
            isPlayerNearby = false;
            nearbyPlayer = null;
            currentState = NPCState.Idle;
        }
    }

    void UpdateStateBasedOnPlayer()
    {
        if(nearbyPlayer == null){
            Debug.Log("Player missing error");
        }
        else if(status == null){
            Debug.Log("Status missing error");
        }

        if (HealthBar.currentInfection >= 80f)
        {
            currentState = NPCState.DialogueFear;
        }
        else if (status.currentHealth < 50f && !hasGivenItem)
        {
            currentState = NPCState.DialogueEmpathy;
        }
        else if (HealthBar.currentInfection >= 50f)
        {
            currentState = NPCState.DialogueUneasy;
        }
        else if (hasGivenItem && !respondedToItemUse)
        {
            currentState = NPCState.DialoguePostItemUse;
            respondedToItemUse = true;
        }
        else
        {
            currentState = NPCState.DialogueNeutral;
        }
        HandleAnimations();
        HandleDialogue();
    }

    void HandleDialogue()
    {
        switch (currentState)
        {
            case NPCState.DialogueFear:
                ShowDialogue("Hey are you infected!? Stay away from me!");
                break;
            case NPCState.DialogueUneasy:
                ShowDialogue("You look a little off... feeling okay? " + "You should look for some items to heal up. " +
                "There are buildings around here you can enter that will have them. Be careful though, there might be zombies gaurding them.");
                break;
            case NPCState.DialogueEmpathy:
                if(hasGivenItem)
                {
                    ShowDialogue("Sorry, I don't have any more things to give you." + " You should look for some items to heal up. " +
                    "There are buildings around here you can enter that will have them. Be careful though, there might be zombies gaurding them.");
                    break;
                }
                GiveHealingItem(nearbyPlayer);
                ShowDialogue("You’re hurt. Here, take this." + " You should look for some items to heal up. " +
                "There are buildings around here you can enter that will have them. Be careful though, there might be zombies gaurding them.");
                break;
            case NPCState.DialoguePostItemUse:
                ShowDialogue("Glad that helped. Be careful.");
                break;
            case NPCState.DialogueNeutral:
                ShowDialogue("You should look for some items to heal up. " +
                "There are buildings around here you can enter that will have them. Be careful though, there might be zombies gaurding them.");
                break;
        }
    }

    void HandleAnimations()
    {
        switch (currentState)
        {
            case NPCState.DialogueFear:
                StartCoroutine(WaitForAnimationThenReset("Fear"));
                break;
            case NPCState.DialogueUneasy:
                StartCoroutine(WaitForAnimationThenReset("Uneasy"));
                break;
            case NPCState.DialogueEmpathy:
                if (hasGivenItem)
                {
                    StartCoroutine(WaitForAnimationThenReset("HandItem"));
                }
                else
                {
                    StartCoroutine(WaitForAnimationThenReset("ShakeHead"));
                }
                break;
            case NPCState.DialogueNeutral:
                StartCoroutine(WaitForAnimationThenReset("Neutral"));
                break;
        }
    }

    IEnumerator WaitForAnimationThenReset(string stateName)
    {
        animator.SetBool(stateName, true);

        while (!animator.GetCurrentAnimatorStateInfo(0).IsName(stateName))
        {
            yield return null;
        }

        animator.SetBool(stateName, false);
    }

    void ShowDialogue(string text)
    {
        dialogue.ShowDialogue(text);
    }

    void GiveHealingItem(Transform player)
    {
        hasGivenItem = true;
        Debug.Log("NPC gave healing item");
        // Inventory logic to be implemented
        // player.GetComponent<PlayerInventory>().AddItem("HealingKit");
    }
}
