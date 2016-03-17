using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class InventoryDisplay : MonoBehaviour {

    private List<GameObject> items;
    private int currentIndex;
    private int inventoryCount;

    void Awake() {
        items = GameObject.Find("Inventory").GetComponent<Inventory>().items;
        currentIndex = GameObject.Find("GameManager").GetComponent<CombatButtons>().itemIndex;
	}

    void Update()
    {
        currentIndex = GameObject.Find("GameManager").GetComponent<CombatButtons>().itemIndex;
    }

    public void Refresh()
    {
        Text itemBox = GameObject.Find("ItemText").GetComponent<Text>();
        string itemName = items[currentIndex - 1].GetComponent<ItemStats>().itemName.ToUpper();
        string description = items [currentIndex - 1].GetComponent<ItemStats>().description;

        itemBox.text = itemName + ": " + description + ".";

        UpdateInventory(currentIndex);
    }

    public void UpdateInventory(int itemIndex)
    {
        Text itemBox = GameObject.Find("ItemText").GetComponent<Text>();
        string itemName = "";

        for (int i = 0; i < items.Count; i++)
        {
            itemName = items [i].GetComponent<ItemStats>().itemName;
            GameObject.Find("txtItem" + (i + 1)).GetComponent<Text>().text = itemName;
        }
    }

    public void UpdateDescription(int itemIndex)
    {
        Text itemBox = GameObject.Find("ItemText").GetComponent<Text>();
        string itemName = items[itemIndex - 1].GetComponent<ItemStats>().itemName.ToUpper();
        string description = items [itemIndex - 1].GetComponent<ItemStats>().description;

        itemBox.text = itemName + ": " + description + ".";
    }

    public void UseItem(int itemIndex)
    {
        Text itemBox = GameObject.Find("ItemText").GetComponent<Text>();
        string itemName = items[itemIndex - 1].GetComponent<ItemStats>().itemName;

        itemBox.text = "using " + itemName + "...";
    }
}
