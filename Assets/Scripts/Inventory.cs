using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{

    public bool inventoryActive;

    public GameObject inventory;
    private int NumberOfSlots;
    private GameObject[] ArrayOfSlots;

    public GameObject slotHolder;

    public GameObject waterBottleSlot;
    private SelectItem waterBottleSelectItemScript;
    private Image waterBottleImageComponent;
    public Sprite fullWaterBottlePicture;

    public GameObject playerCamera;
    private LockView lockViewScript;

    


    // Start is called before the first frame update
    void Start()
    {
        inventoryActive = false;

        NumberOfSlots = 13;
        ArrayOfSlots = new GameObject[NumberOfSlots];

        for (int i = 0; i < NumberOfSlots; i++)
        {
            ArrayOfSlots[i] = slotHolder.transform.GetChild(i).gameObject;
        }

        waterBottleImageComponent = waterBottleSlot.GetComponent<Image>();
        lockViewScript = playerCamera.GetComponent<LockView>();

        waterBottleSelectItemScript = waterBottleSlot.GetComponent<SelectItem>();
    }

    public void addObtainedItemPictureToNextAvailableSlot(Sprite inventoryImageSprite)
    {
        for (int i = 0; i < NumberOfSlots; i++)
        {
            if (ArrayOfSlots[i].GetComponent<Image>().sprite == null)
            {
                ArrayOfSlots[i].GetComponent<Image>().sprite = inventoryImageSprite;
                return;
            }
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.I))
        {

            if (!inventory.active)
            {
                inventory.SetActive(true);
                inventoryActive = true;
                Time.timeScale = 0;
                Cursor.lockState = CursorLockMode.None;


            } else
            {
                inventory.SetActive(false);
                inventoryActive = false;
                Time.timeScale = 1;
                Cursor.lockState = CursorLockMode.Confined;
                Cursor.lockState = CursorLockMode.Locked;
            }
        }
        if (InventoryItemManager.playerHasFullWaterBottle)
        {
            
            waterBottleImageComponent.sprite = fullWaterBottlePicture;
            
        }
    }

	public void Close()
	{
		inventory.SetActive( false );
		inventoryActive = false;
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Confined;
		Cursor.lockState = CursorLockMode.Locked;
	}
}
