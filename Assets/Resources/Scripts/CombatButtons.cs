using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatButtons : MonoBehaviour {

    private Image fightIcon;
    private Image itemIcon;
    private Image skillIcon;
    private int selectedIndex;
    private int itemIndex;
    private Combat combat;
    private GameObject itemBox;
    private GameObject itemHighlight;
    private Inventory inventory;

	void Start () {
        selectedIndex = 2;
        itemIndex = 1;

        //Initialization of the button images
        fightIcon = GameObject.Find("Fight").GetComponent<Image>();
        itemIcon = GameObject.Find("Item").GetComponent<Image>();
        skillIcon = GameObject.Find("Skill").GetComponent<Image>();

        //Sets the default opacity of the images
        fightIcon.color = new Color(1f, 1f, 1f, 1f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);

        //Storing the combat script for ease of use
        combat = GameObject.Find("CombatManager").GetComponent<Combat>();

        itemHighlight = GameObject.Find("barItemHighlight");
        itemHighlight.GetComponent<Image>().color = new Color(1f, 1f, 1f, 0.5f);

        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();

        itemBox = GameObject.Find("ItemBox");
        itemBox.SetActive(false);
    }
	

	void Update () {
        //Update checking for key presses
        if (Input.GetKeyUp("left") && !itemBox.activeSelf)
        {
            //Debug.Log("Left Arrow");
            if (selectedIndex > 1)
            {
                selectedIndex--;
                ColorBrain(selectedIndex);
            }
        } 
        else if (Input.GetKeyUp("right") && !itemBox.activeSelf)
        {
            //Debug.Log("Right Arrow");
            if (selectedIndex < 3)
            {
                selectedIndex++;
                ColorBrain(selectedIndex);
            }
        }



        if (itemBox.activeSelf && Input.GetKeyUp("up") && itemIndex > 1)
        {
            itemIndex--;
            HighlightMovement(1);
        }
        else if (itemBox.activeSelf && Input.GetKeyUp("down") && itemIndex < 5)
        {
            itemIndex++;
            HighlightMovement(2);
        }



        if (Input.GetKeyUp("return"))
        {
            if (!itemBox.activeSelf)
                ButtonBrain(selectedIndex);
            else if (itemBox.activeSelf)
                ItemBrain(itemIndex);
        }



        if (Input.GetKeyUp("escape") && itemBox.activeSelf)
            itemBox.SetActive(false);
    }

    private void ColorBrain(int num)
    {
        switch (num)
        {
            case 1:
                Item();
                break;
            case 2:
                Fight();
                break;
            case 3:
                Skill();
                break;
        }
    }

    private void ButtonBrain(int num)
    {
        switch (num)
        {
            case 1:
                doItem();
                break;
            case 2:
                doFight();
                break;
            case 3:
                doSkill();
                break;
        }
    }

    private void ItemBrain(int num)
    {
        string itemName = "";
        Text itemText = GameObject.Find("ItemText").GetComponent<Text>();

        switch (num)
        {
            case 1:
                itemName = inventory.item1.GetComponent<ItemStats>().itemName;
                itemText.text = "Using " + itemName + "...";
                break;
            case 2:
                itemName = inventory.item2.GetComponent<ItemStats>().itemName;
                itemText.text = "Using " + itemName + "...";
                break;
            case 3:
                itemName = inventory.item3.GetComponent<ItemStats>().itemName;
                itemText.text = "Using " + itemName + "...";
                break;
            case 4:
                itemName = inventory.item4.GetComponent<ItemStats>().itemName;
                itemText.text = "Using " + itemName + "...";
                break;
            case 5:
                itemName = inventory.item5.GetComponent<ItemStats>().itemName;
                itemText.text = "Using " + itemName + "...";
                break;
        }
    }

    private void HighlightMovement(int num)
    {
        switch (num)
        {
            case 1:
                itemHighlight.GetComponent<RectTransform>().localPosition += new Vector3(0, 25, 0);
                break;
            case 2:
                itemHighlight.GetComponent<RectTransform>().localPosition += new Vector3(0, -25, 0);
                break;
        }
    }

    private void Fight()
    {
        fightIcon.color = new Color(1f, 1f, 1f, 1f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void Item()
    {
        fightIcon.color = new Color(1f, 1f, 1f, 0.5f);
        itemIcon.color = new Color(1f, 1f, 1f, 1f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void Skill()
    {
        fightIcon.color = new Color(1f, 1f, 1f, 0.5f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 1f);
    }

    private void doFight()
    {
        Debug.Log("Fighting...");
        combat.Attack();
    }

    private void doItem()
    {
        itemIndex = 1;
        itemHighlight.GetComponent<RectTransform>().localPosition = new Vector3(-89.67501f, -90.02f, 0);
        itemBox.SetActive(true);
        GameObject.Find("ItemText").GetComponent<Text>().text = "";
    }

    private void doSkill()
    {

    }
}
