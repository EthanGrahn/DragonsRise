using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CombatButtons : MonoBehaviour {

    private Image fightIcon;
    private Image itemIcon;
    private Image skillIcon;
    private int selectedIndex;
    public int itemIndex;
    public int toneIndex;
    private Combat combat;
    private GameObject itemBox;
    private bool started = false;
    public bool transitioning = false;
    private Animator charAnimator;

    void Start () {
        selectedIndex = 2;
        itemIndex = 1;
        toneIndex = 1;
    }

	void Update () {

        if (!started)
        {
            //Storing combat script
            combat = GameObject.Find("GameManager").GetComponent<Combat>();

            charAnimator = GameObject.Find("Character").GetComponent<Animator>();

            itemBox = GameObject.Find("ItemBox");
            itemBox.SetActive(false);
            started = true;

            //Initialization of the button images
            fightIcon = GameObject.FindGameObjectWithTag("fightIcon").GetComponent<Image>();
            itemIcon = GameObject.FindGameObjectWithTag("itemIcon").GetComponent<Image>();
            skillIcon = GameObject.FindGameObjectWithTag("skillIcon").GetComponent<Image>();

            //Sets the default opacity of the images
            fightIcon.color = new Color(1f, 1f, 1f, 1f);
            itemIcon.color = new Color(1f, 1f, 1f, 0.5f);
            skillIcon.color = new Color(1f, 1f, 1f, 0.5f);
        }
        
        //Update checking for key presses
        if (Input.GetKeyUp("left") && !itemBox.activeSelf)
        {
            if (selectedIndex > 1)
            {
                selectedIndex--;
                ColorBrain(selectedIndex);
            }
        } 
        else if (Input.GetKeyUp("right") && !itemBox.activeSelf)
        {
            if (selectedIndex < 3)
            {
                selectedIndex++;
                ColorBrain(selectedIndex);
            }
        }
        else if (Input.GetKeyUp("s") && !itemBox.activeSelf)
        {
            toneIndex--;

            if (toneIndex == 0)
                toneIndex = 3;

            ToneAdjust(toneIndex);
        }
        else if (Input.GetKeyUp("d") && !itemBox.activeSelf)
        {
            toneIndex++;

            if (toneIndex == 4)
                toneIndex = 1;

            ToneAdjust(toneIndex);
        }



        if (itemBox.activeSelf && Input.GetKeyUp("up") && itemIndex > 1)
        {
            itemIndex--;
            ItemBrain(itemIndex);
        }
        else if (itemBox.activeSelf && Input.GetKeyUp("down") && itemIndex < 4)
        {
            itemIndex++;
            ItemBrain(itemIndex);
        }

        if (Input.GetKeyUp("x"))
        {
            if (!itemBox.activeSelf)
                ButtonBrain(selectedIndex);
            else if (itemBox.activeSelf)
                GameObject.Find("GameManager").GetComponent<InventoryDisplay>().UseItem(itemIndex);
        }



        if (Input.GetKeyUp("z") && itemBox.activeSelf)
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
        if (!transitioning)
        {
            switch (num)
            {
                case 1:
                    doItem();
                    break;
                case 2:
                    StartCoroutine(doFight());
                    break;
                case 3:
                    StartCoroutine(doSkill());
                    break;
            }
        }
    }

    private void ItemBrain(int num)
    {
        Text item1 = GameObject.Find("txtItem1").GetComponent<Text>();
        Text item2 = GameObject.Find("txtItem2").GetComponent<Text>();
        Text item3 = GameObject.Find("txtItem3").GetComponent<Text>();
        Text item4 = GameObject.Find("txtItem4").GetComponent<Text>();

        item1.color = new Color(r: 255, g: 255, b: 255);
        item2.color = new Color(r: 255, g: 125, b: 255);
        item3.color = new Color(r: 255, g: 255, b: 255);
        item4.color = new Color(r: 255, g: 125, b: 255);

        switch (num)
        {
            case 1:
                item1.color = new Color(r: 0, g: 125, b: 255);
                break;
            case 2:
                item2.color = new Color(r: 0, g: 125, b: 255);
                break;
            case 3:
                item3.color = new Color(r: 0, g: 125, b: 255);
                break;
            case 4:
                item4.color = new Color(r: 0, g: 125, b: 255);
                break;
        }

        GameObject.Find("GameManager").GetComponent<InventoryDisplay>().UpdateDescription(itemIndex);
    }

    private void ToneAdjust(int num)
    {
        Text leftTone = GameObject.Find("txtTone_left").GetComponent<Text>();
        Text centerTone = GameObject.Find("txtTone_center").GetComponent<Text>();
        Text rightTone = GameObject.Find("txtTone_right").GetComponent<Text>();

        switch (num)
        {
            case 1:
                leftTone.text = "Harsh";
                centerTone.text = "Cautious";
                rightTone.text = "Encouraging";
                break;
            case 2:
                leftTone.text = "Cautious";
                centerTone.text = "Encouraging";
                rightTone.text = "Harsh";
                break;
            case 3:
                leftTone.text = "Encouraging";
                centerTone.text = "Harsh";
                rightTone.text = "Cautious";
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

    private IEnumerator doFight()
    {
        charAnimator.SetTrigger("Attack");
        yield return new WaitForSeconds(0.5f);
        combat.Attack();
        charAnimator.ResetTrigger("Attack");
    }

    private void doItem()
    {
        itemIndex = 1;
        itemBox.SetActive(true);
        ItemBrain(itemIndex);
        GameObject.Find("GameManager").GetComponent<InventoryDisplay>().Refresh();
        GameObject.Find("GameManager").GetComponent<InventoryDisplay>().UpdateDescription(1);
    }

    private IEnumerator doSkill()
    {
        if (combat.specialCount == 0)
        {
            charAnimator.SetTrigger("Special");
            yield return new WaitForSeconds(0.75f);
            combat.Special();
            yield return new WaitForSeconds(0.25f);
            charAnimator.ResetTrigger("Special");
        }
    }
}
