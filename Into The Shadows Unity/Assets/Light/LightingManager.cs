using UnityEngine;

public class LightingManager : MonoBehaviour
{
    [SerializeField] private Light DirectionalLight;
    [SerializeField] private Transform Player; // Player reference
    [SerializeField] private PlayerInfection playerInfection; // Reference to PlayerInfection
    [SerializeField] private float infectionInterval = 2f; // Interval to increase infection
    private float infectionTimer = 0f;

    private void Update()
    {
        infectionTimer += Time.deltaTime;

        if (infectionTimer >= infectionInterval)
        {
            if (IsPlayerInSunlight())
            {
                playerInfection.IncreaseInfection(1); // Increase infection level
            }
            infectionTimer = 0f; // Reset timer
        }
    }

    private bool IsPlayerInSunlight()
    {
        RaycastHit hit;
        Vector3 sunDirection = -DirectionalLight.transform.forward;
        Vector3 rayOrigin = Player.position + Vector3.up * 2f;

        return !Physics.Raycast(rayOrigin, sunDirection, out hit, Mathf.Infinity);
    }
}
