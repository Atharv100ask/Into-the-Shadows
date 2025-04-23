using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class Inventory : MonoBehaviour
{
    public Camera playerCamera;
    public HealthBar status;
    public BatAttack bat;
    public GameObject[] itemsInHand;
    public Transform hotbarParent;
    public TMP_Text foodAmount;
    public TMP_Text stabilizerAmount;
    public TMP_Text ammoAmount;
    private RectTransform[] hotbarSlots;
    public GameObject healthEffectParticles;
    public GameObject stabilizerEffectParticles;
    public Transform effectSpawnPoint;
    public AudioSource audioSource;
    public AudioClip consumableSound;
    public LayerMask targetMask;
    public ParticleSystem muzzleFlash;
    public AudioClip shotSound;
    public AudioClip emptyGunSound;
    public Canvas mapDisplay;
    public bool hasMelee;
    public bool hasGun;
    public bool hasMap;
    public int food;
    public int stabilizers;
    public int ammo;
    private Color normalColor;
    private Color highlight;
    private Color errorColor;
    private float scale = 1.2f;
    public int currentItem;
    public Canvas cooldownCanvas;
    public TMP_Text cooldownText;
    private bool isOnCooldown = false;
    private float attackCooldown = 1.8f;
    private bool canShoot = true;
    public float shootCooldown = 0.5f;
    public float shootRange = 100f;
    public int gunDamage = 50;


    // 1: Melee, 2: Gun, 3: Food, 4: Stabilizers, 5: Ammo 6: Map
    void Start()
    {
        hasMelee = false;
        hasGun = true;
        hasMap = false;
        food = 3;
        stabilizers = 3;
        ammo = 10;
        normalColor = new Color32(103,103,103,100);
        highlight = new Color32(0,255,255,100);
        errorColor = Color.red;
        hotbarSlots = new RectTransform[6];
        currentItem = -1;
        for (int i = 0; i < 6; i++)
        {
            hotbarSlots[i] = hotbarParent.GetChild(i).GetComponent<RectTransform>();
        }
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Debug.Log("Pressed 1");
            EquipItem(1);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Debug.Log("Pressed 2");
            EquipItem(2);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            Debug.Log("Pressed 3");
            EquipItem(3);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            Debug.Log("Pressed 4");
            EquipItem(4);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha5))
        {
            Debug.Log("Pressed 5");
            EquipItem(5);
        }
        else if (Input.GetKeyDown(KeyCode.M))
        {
            Debug.Log("Pressed M");
            EquipItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Pressed 6");
            EquipItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            UseItem();
        }
        else if (Input.GetKeyDown(KeyCode.Escape))
        {
            mapDisplay.gameObject.SetActive(false);
        }

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        int equip = 0;

        if (scroll < 0f)
        {
            Debug.Log("Scroll Down");
            mapDisplay.gameObject.SetActive(false);

            if (currentItem + 1 > 6)
            {
                equip = 1;
            }
            else
            {
                equip = currentItem + 1;
            }
            
            EquipItem(equip);
        }
        else if (scroll > 0f)
        {
            Debug.Log("Scroll Up");
            mapDisplay.gameObject.SetActive(false);

            if (currentItem - 1 < 1)
            {
                equip = 6;
            } 
            else
            {
                equip = currentItem - 1;
            }
            
            EquipItem(equip);
        }
    }

    IEnumerator AttackCooldownTimer()
    {
        isOnCooldown = true;
        bat.EnableDamage();

        cooldownCanvas.gameObject.SetActive(true);
        float timeRemaining = attackCooldown;

        var image = hotbarSlots[0].GetComponent<Image>();

        if (image != null) image.color = normalColor;

        while (timeRemaining > 0)
        {
            timeRemaining -= Time.deltaTime;
            cooldownText.text = Mathf.Max(timeRemaining, 0).ToString("F2");
            yield return null;
        }
        if (image != null) image.color = highlight;

        cooldownCanvas.gameObject.SetActive(false);
        isOnCooldown = false;
        bat.DisableDamage();
    }

    private IEnumerator Shoot()
    {
        Debug.Log("Shot");
        canShoot = false;
        ammo -= 1;
        UpdateItemCount();

        muzzleFlash.Play();
        audioSource.volume = 0.05f;
        audioSource.PlayOneShot(shotSound);

        Ray ray = playerCamera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0));
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, shootRange, targetMask))
        {
            Debug.Log("Hit: " + hit.collider.name);

            ZombieHealthBar zombie = hit.collider.GetComponentInChildren<ZombieHealthBar>();
            if (zombie != null)
            {
                zombie.TakeDamage(gunDamage);
            }
        }

        yield return new WaitForSeconds(shootCooldown);

        canShoot = true;
    }

    void UseItem()
    {
        Debug.Log(currentItem);
        switch (currentItem)
        {
            
            case 1:
                if (hasMelee && !isOnCooldown)
                {
                    Debug.Log("Swing");
                    StartCoroutine(AttackCooldownTimer());
                }
                break;
            case 2:
                if (canShoot && ammo > 0)
                {
                    StartCoroutine(Shoot());
                }
                else if (ammo == 0)
                {
                    audioSource.volume = 0.1f;
                    audioSource.PlayOneShot(emptyGunSound);
                }
                break;
            case 3:
                Debug.Log("Check");
                if (food > 0)
                {
                    PlayConsumableEffects(0);
                    food--;
                    if (status.currentHealth + 25 > status.maxHealth)
                    {
                        status.currentHealth = status.maxHealth;
                    }
                    else
                    {
                        status.currentHealth += 25;
                    }
                    UpdateItemCount();
                }
                break;
            case 4:
                Debug.Log("Check");
                if (stabilizers > 0)
                {
                    PlayConsumableEffects(1);
                    stabilizers--;
                    if (HealthBar.currentInfection - 25 < 0)
                    {
                        HealthBar.currentInfection = 0;
                    }
                    else
                    {
                        HealthBar.currentInfection -= 25;
                    }
                    UpdateItemCount();
                }
                break;
            case 5:
                if (ammo > 0)
                {
                    Debug.Log("Ammo Used");
                }
                break;
            case 6:
                if (hasMap)
                {
                    mapDisplay.gameObject.SetActive(true);
                }
                break;
        }
    }

    void PlayConsumableEffects(int effect)
    {
        if (effect == 0)
        {
            Vector3 spawnOffset = new Vector3(-0.5f, 0f, 0f);
            var effect1 = Instantiate(healthEffectParticles, effectSpawnPoint.position + spawnOffset, Quaternion.identity, effectSpawnPoint);
            Destroy(effect1, 3f);
        }
        else 
        {
            Vector3 spawnOffset = new Vector3(-0.5f, 0f, 0f);
            var effect2 = Instantiate(stabilizerEffectParticles, effectSpawnPoint.position + spawnOffset, Quaternion.identity, effectSpawnPoint);
            Destroy(effect2, 3f);
        }

        if (audioSource != null && consumableSound != null)
        {
            audioSource.volume = 0.05f;
            audioSource.PlayOneShot(consumableSound);
        }
    }


    bool hasItem(int key)
    {
        switch (key)
        {
            case 1:
                if (!hasMelee)
                {
                    return false;
                }
                break;
            case 2:
                if (!hasGun)
                {
                    return false;
                }
                break;
            case 3:
                if (food == 0)
                {
                    return false;
                }
                break;
            case 4:
                if (stabilizers == 0)
                {
                    return false;
                }
                break;
            case 5:
                if (ammo == 0)
                {
                    return false;
                }
                break;
            case 6:
                if (!hasMap)
                {
                    return false;
                }
                break;
        }
        return true;
    }

    private IEnumerator FlashRed(RectTransform slot, Image image)
    {
        if (image == null) yield break;

        image.color = errorColor;

        float flashDuration = 1f; 
        float elapsed = 0f;

        while (elapsed < flashDuration)
        {
            elapsed += Time.deltaTime;
            float t = elapsed / flashDuration;
            
            image.color = Color.Lerp(errorColor, normalColor, t);

            yield return null;
        }

        image.color = normalColor;
    }


    void EquipItem(int key){
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            var image = hotbarSlots[i].GetComponent<Image>();

            if (i == (key - 1))
            {
                if (!hasItem(key))
                {
                    StartCoroutine(FlashRed(hotbarSlots[i], image));
                    currentItem = -1;
                    continue;
                }
                if(key == currentItem) // Unequip current item
                {
                    hotbarSlots[i].localScale = Vector3.one * 1;
                    if (image != null) image.color = normalColor;
                    itemsInHand[i].SetActive(false);
                    currentItem = -1;
                }
                else // Equip chosen item
                {
                    hotbarSlots[i].localScale = Vector3.one * scale;
                    if (image != null) image.color = highlight;

                    itemsInHand[i].SetActive(true);
                    
                    currentItem = key;
                }
            }
            else // Reset state of other items
            {
                hotbarSlots[i].localScale = Vector3.one * 1;
                if (image != null) image.color = normalColor;
                itemsInHand[i].SetActive(false);
            }
        }
    }

    public void UpdateItemCount()
    {
        foodAmount.text = food.ToString();
        stabilizerAmount.text = stabilizers.ToString();
        ammoAmount.text = ammo.ToString();
    }
}
