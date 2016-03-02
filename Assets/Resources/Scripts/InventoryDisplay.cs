using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class InventoryDisplay : MonoBehaviour {

    private Inventory inventory;
    private int currentIndex;
    private int inventoryCount;

    public void Start () {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        currentIndex = GameObject.Find("GameManager").GetComponent<CombatButtons>().itemIndex;

        Refresh();
	}

    public void Refresh()
    {
        GameObject.Find("ItemText").GetComponent<Text>().text = "";

        UpdateInventory(currentIndex);
    }

    public void UpdateInventory(int itemIndex)
    {
        string itemName = "";
        Text itemText = GameObject.Find("ItemText").GetComponent<Text>();
        itemText.text = itemName;
    }
}
