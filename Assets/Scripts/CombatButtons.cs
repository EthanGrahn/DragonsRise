using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatButtons : MonoBehaviour {

    private Image defendIcon;
    private Image fightIcon;
    private Image itemIcon;
    private Image magicIcon;
    private int selectedIndex;

	void Start () {
        selectedIndex = 2;

        //Initialization of the button images
        defendIcon = GameObject.Find("Defend").GetComponent<Image>();
        fightIcon = GameObject.Find("Fight").GetComponent<Image>();
        itemIcon = GameObject.Find("Item").GetComponent<Image>();
        magicIcon = GameObject.Find("Magic").GetComponent<Image>();

        //Sets the default opacity of the images
        defendIcon.color = new Color(1f, 1f, 1f, 0.5f);
        fightIcon.color = new Color(1f, 1f, 1f, 1f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        magicIcon.color = new Color(1f, 1f, 1f, 0.5f);
	}
	

	void Update () {
        if (Input.GetKeyUp("left"))
        {
            Debug.Log("Left Arrow");
            selectedIndex--;
            ButtonBrain(selectedIndex);
        } 
        else if (Input.GetKeyUp("right"))
        {
            Debug.Log("Right Arrow");
            selectedIndex++;
            ButtonBrain(selectedIndex);
        }
	}

    private void ButtonBrain(int num)
    {
        switch (num)
        {
            case 1:
                Defend();
                break;
            case 2:
                Fight();
                break;
            case 3:
                Item();
                break;
            case 4:
                Magic();
                break;
            default:
                Debug.Log("ERROR: ButtonBrain input out of range.");
        }
    }

    private void Defend()
    {
        
    }

    private void Fight()
    {

    }

    private void Item()
    {

    }

    private void Magic()
    {

    }
}
