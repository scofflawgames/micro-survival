﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DeepWolf.HungerThirstSystem;

public class ToolBelt : MonoBehaviour
{
    public List<GameObject> toolBeltSlots = new List<GameObject>();
  

    public int selectedItem = 0;
    public int previousSelected = 0;
    public int currentItemID;
    public int currentItemAMT;
    //private int maxItems = 5;

    private HungerThirst hungerThirst;
    private PlayerManager playerManager;
    private Inventory inventory;
    private ItemDatabase database;
    private WeaponChange weaponChange;


    Image selectorImage = null;
        
    // Start is called before the first frame update
    void Start()
    {
        playerManager = FindObjectOfType<PlayerManager>();
        hungerThirst = FindObjectOfType<HungerThirst>();
        weaponChange = FindObjectOfType<WeaponChange>();
        database = FindObjectOfType<ItemDatabase>();
        inventory = FindObjectOfType<Inventory>();

        selectorImage = toolBeltSlots[selectedItem].transform.GetChild(2).GetComponent<Image>();
        selectorImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        MouseScrollSelector();
        //ItemSelector();

        if (Input.GetMouseButtonDown(0) && !Inventory.inventoryActive && !PauseMenu.isPaused)
        {
            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            if (currentSlot.myItem != null && currentSlot.myItem.isConsumable && currentSlot.myItem.isFood) //eating food
            {
                hungerThirst.AddHunger(-currentSlot.myItem.foodAmount);
                playerManager.RefreshHunger();
                print("You just ate 1 " + currentSlot.myItem.itemName + " that you found by the toilet.. Congratulations!");
                currentSlot.RemoveItem(1);
            }
            else if (currentSlot.myItem != null && currentSlot.myItem.isConsumable && currentSlot.myItem.isWater) //drinking water
            {
                hungerThirst.AddThirst(-currentSlot.myItem.waterAmount);
                playerManager.RefreshThirst();
                print("You just drank 1 " + currentSlot.myItem.itemName + " that you got from the nasty ass toilet. SICK FUCK!!");
                inventory.AddItem(database.GetItemById(6), 1);
                currentSlot.RemoveItem(1);             
            }
        }
    }

    public void ItemSelector()
    {
        if (Input.GetKeyDown(KeyCode.E) && !GameManager.devConsoleActive) // select next item
        {
            if (selectedItem < 6)
            {
                previousSelected = selectedItem;
                selectedItem += 1;
            }

            if (selectedItem == 6)
            {
                previousSelected = 5;
                selectedItem = 0;
            }

            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            //SPACE
            OutlineSelector(previousSelected, selectedItem);

            if (currentSlot.myItem != null && currentSlot.myItem.isWieldable)
            {
                WieldableEquip(currentSlot.myItem.itemName, currentSlot.myItem.itemID, currentSlot.myAmount);
            }
            else
            {
                WieldableEquip("null", 0, 0);
            }
        }

        if (Input.GetKeyDown(KeyCode.Q) && !GameManager.devConsoleActive) // select previous item
        {
            if (selectedItem > -1)
            {
                previousSelected = selectedItem;
                selectedItem -= 1;
            }

            if (selectedItem == -1)
            {
                previousSelected = 0;
                selectedItem = 5;
            }

            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            //SPACE
            OutlineSelector(previousSelected, selectedItem);


        }

    }

    public void WieldableEquip(string ItemName, int itemID, int itemAMT)
    {
        print("Current item equipped is " + ItemName + " ItemID: " + itemID + " Amount: " + itemAMT);
        currentItemID = itemID;
        currentItemAMT = itemAMT;
        WeaponChange.equippableItemID = currentItemID;
        weaponChange.playerAnimation();
    }

    public void MouseScrollSelector()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f && !GameManager.devConsoleActive) //scroll forward
        {
            if (selectedItem < 6)
            {
                previousSelected = selectedItem;
                selectedItem += 1;
            }

            if (selectedItem == 6)
            {
                previousSelected = 5;
                selectedItem = 0;
            }

            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            //SPACE
            OutlineSelector(previousSelected, selectedItem);

            if (currentSlot.myItem != null && currentSlot.myItem.isWieldable)
            {
                WieldableEquip(currentSlot.myItem.itemName, currentSlot.myItem.itemID, currentSlot.myAmount);
                
            }
            else
            {
                WieldableEquip("null", 0, 0);
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f && !GameManager.devConsoleActive) //scroll backward
        {
            if (selectedItem > -1)
            {
                previousSelected = selectedItem;
                selectedItem -= 1;
            }

            if (selectedItem == -1)
            {
                previousSelected = 0;
                selectedItem = 5;
            }

            Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
            //SPACE
            OutlineSelector(previousSelected, selectedItem);

            if (currentSlot.myItem != null && currentSlot.myItem.isWieldable)
            {
                WieldableEquip(currentSlot.myItem.itemName, currentSlot.myItem.itemID, currentSlot.myAmount);
            }
            else
            {
                WieldableEquip("null", 0, 0);
            }

        }

    }

    public void OutlineSelector(int prevItem, int curItem)
    {
        //remove outline from past item
        selectorImage = toolBeltSlots[prevItem].transform.GetChild(2).GetComponent<Image>();
        selectorImage.enabled = false;

        //outline current item
        selectorImage = toolBeltSlots[curItem].transform.GetChild(2).GetComponent<Image>();
        selectorImage.enabled = true;
    }

    public void DragAndDropCheck()
    {
        Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();

        if (currentSlot.myItem != null)
        {
            WieldableEquip(currentSlot.myItem.itemName, currentSlot.myItem.itemID, currentSlot.myAmount);
        }
        else
        {
            WieldableEquip("null", 0, 0);
        }
    }

    public void DestroyCurrentItem()
    {
        Slot currentSlot = toolBeltSlots[selectedItem].GetComponent<Slot>();
        currentSlot.RemoveItem(currentSlot.myAmount);
    }

}
