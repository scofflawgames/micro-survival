﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ToolBelt : MonoBehaviour
{
    public List<GameObject> toolBeltSlots = new List<GameObject>();

    

    public int selectedItem = 0;
    public int previousSelected = 0;
    //private int maxItems = 5;

    Image selectorImage = null;
        
    // Start is called before the first frame update
    void Start()
    {
        selectorImage = toolBeltSlots[selectedItem].transform.GetChild(2).GetComponent<Image>();
        selectorImage.enabled = true;
    }

    // Update is called once per frame
    void Update()
    {
        MouseScrollSelector();
        ItemSelector();
    }

    public void ItemSelector()
    {
        if (Input.GetKeyDown(KeyCode.E)) // select next item
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
                WieldableEquip(currentSlot.myItem.itemName);
            }
            else
            {
                WieldableEquip("null");
            }
        }

        if (Input.GetKeyDown(KeyCode.Q)) // select previous item
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

    public void WieldableEquip(string ItemName)
    {

    }

    public void MouseScrollSelector()
    {
        if (Input.GetAxis("Mouse ScrollWheel") > 0f) //scroll forward
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
                WieldableEquip(currentSlot.myItem.itemName);
            }
            else
            {
                WieldableEquip("null");
            }
        }
        else if (Input.GetAxis("Mouse ScrollWheel") < 0f) //scroll backward
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
                WieldableEquip(currentSlot.myItem.itemName);
            }
            else
            {
                WieldableEquip("null");
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
            WieldableEquip(currentSlot.myItem.itemName);
        }
        else
        {
            WieldableEquip("null");
        }
    }

}