using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Inventory : MonoBehaviour
{
    public GameObject[] itemsInHand;
    public Transform hotbarParent;
    public TMP_Text foodAmount;
    public TMP_Text stabilizerAmount;
    public TMP_Text ammoAmount;
    private RectTransform[] hotbarSlots;
    public bool hasMelee;
    public bool hasGun;
    public bool hasFlashlight;
    public int food;
    public int stabilizers;
    public int ammo;
    private Color normalColor;
    private Color highlight;
    private float scale = 1.2f;
    public int currentItem;


    // 1: Melee, 2: Gun, 3: Food, 4: Stabilizers, 5: Ammo F: flashlight
    void Start()
    {
        hasMelee = true;
        hasGun = true;
        hasFlashlight = false;
        food = 0;
        stabilizers = 0;
        ammo = 0;
        normalColor = new Color32(103,103,103,100);
        highlight = new Color32(0,255,255,100);
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
        else if (Input.GetKeyDown(KeyCode.F))
        {
            Debug.Log("Pressed F");
            EquipItem(6);
        }
        else if (Input.GetKeyDown(KeyCode.Alpha6))
        {
            Debug.Log("Pressed 6");
            EquipItem(6);
        }
    }

    void EquipItem(int key){
        for (int i = 0; i < hotbarSlots.Length; i++)
        {
            var image = hotbarSlots[i].GetComponent<Image>();

            if (i == (key - 1))
            {
                if(key == currentItem) // Unequip current item
                {
                    hotbarSlots[i].localScale = Vector3.one * 1;
                    if (image != null) image.color = normalColor;
                    if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4)
                    {
                        itemsInHand[i].SetActive(false);
                    }
                    currentItem = -1;
                }
                else // Equip chosen item
                {
                    hotbarSlots[i].localScale = Vector3.one * scale;
                    if (image != null) image.color = highlight;

                    if (i == 0 && hasMelee)
                    {
                        itemsInHand[i].SetActive(true);
                    }

                    if (i == 1 && hasGun)
                    {
                        itemsInHand[i].SetActive(true);
                    }

                    if (i == 2 || i == 3 || i == 4)
                    {
                        itemsInHand[i].SetActive(true);
                    }
                    currentItem = key;
                }
            }
            else // Reset state of other items
            {
                hotbarSlots[i].localScale = Vector3.one * 1;
                if (image != null) image.color = normalColor;
                if (i == 0 || i == 1 || i == 2 || i == 3 || i == 4)
                {
                    itemsInHand[i].SetActive(false);
                }
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
