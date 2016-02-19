using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatButtons : MonoBehaviour {

    private Image defendIcon;
    private Image fightIcon;
    private Image itemIcon;
    private Image skillIcon;
    private int selectedIndex;
    private Combat combat;

	void Start () {
        selectedIndex = 2;

        //Initialization of the button images
        defendIcon = GameObject.Find("Defend").GetComponent<Image>();
        fightIcon = GameObject.Find("Fight").GetComponent<Image>();
        itemIcon = GameObject.Find("Item").GetComponent<Image>();
        skillIcon = GameObject.Find("Skill").GetComponent<Image>();

        //Sets the default opacity of the images
        defendIcon.color = new Color(1f, 1f, 1f, 0.5f);
        fightIcon.color = new Color(1f, 1f, 1f, 1f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);

        //Storing the combat script for ease of use
        combat = GameObject.Find("CombatManager").GetComponent<Combat>();
	}
	

	void Update () {
        //Update checking for key presses
        if (Input.GetKeyUp("left"))
        {
            //Debug.Log("Left Arrow");
            if (selectedIndex > 1)
            {
                selectedIndex--;
                ColorBrain(selectedIndex);
            }
        } 
        else if (Input.GetKeyUp("right"))
        {
            //Debug.Log("Right Arrow");
            if (selectedIndex < 4)
            {
                selectedIndex++;
                ColorBrain(selectedIndex);
            }
        }

        if (Input.GetKeyUp("return"))
        {
            ButtonBrain(selectedIndex);
        }
	}

    private void ColorBrain(int num)
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
                Skill();
                break;
        }
    }

    private void ButtonBrain(int num)
    {
        switch (num)
        {
            case 1:
                doDefend();
                break;
            case 2:
                doFight();
                break;
            case 3:
                doItem();
                break;
            case 4:
                doSkill();
                break;
        }
    }

    private void Defend()
    {
        defendIcon.color = new Color(1f, 1f, 1f, 1f);
        fightIcon.color = new Color(1f, 1f, 1f, 0.5f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void Fight()
    {
        defendIcon.color = new Color(1f, 1f, 1f, 0.5f);
        fightIcon.color = new Color(1f, 1f, 1f, 1f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void Item()
    {
        defendIcon.color = new Color(1f, 1f, 1f, 0.5f);
        fightIcon.color = new Color(1f, 1f, 1f, 0.5f);
        itemIcon.color = new Color(1f, 1f, 1f, 1f);
        skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
    }

    private void Skill()
    {
        defendIcon.color = new Color(1f, 1f, 1f, 0.5f);
        fightIcon.color = new Color(1f, 1f, 1f, 0.5f);
        itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
        skillIcon.color = new Color(1f, 1f, 1f, 1f);
    }

    private void doDefend()
    {

    }

    private void doFight()
    {
        Debug.Log("Fighting...");
        combat.Attack();
    }

    private void doItem()
    {

    }

    private void doSkill()
    {

    }
}
