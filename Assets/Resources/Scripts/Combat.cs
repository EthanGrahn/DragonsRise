using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour {

    public GameObject self;
    public GameObject enemy;
    private Stats enemyStats;
    private Stats selfStats;

    private double currentHealth;
    private double currentBond;

    private bool enemyDead = false;
    public bool enemyTurnComplete;
    public int specialCount = 0;

    private Text enemyHit;
    private Text selfHit;
    private Text enemyHealthTxt;
    private Text selfHealthTxt;

    private RectTransform healthBar;
    private RectTransform bondBar;
    private Image healthFlash;
    private Image bondFlash;

    private Image fadePanel;
    private GameObject canvas;
    private GameObject itemBox;
    private Animator charAnimator;

    public void CombatStart()
    {
        self = GameObject.Find("Character");

        //Stat variables
        enemyStats = enemy.GetComponent<Stats>();
        selfStats = self.GetComponent<Stats>();

        //Current stats to monitor changes in variable
        currentBond = selfStats.bond;
        currentHealth = selfStats.current_health;

        //Hit text boxes
        selfHit = GameObject.Find("txtSelfHit").GetComponent<Text>();
        enemyHit = GameObject.Find("txtEnemyHit").GetComponent<Text>();

        //Health bars
        healthBar = GameObject.Find("barHealth").GetComponent<RectTransform>();
        bondBar = GameObject.Find("barBond").GetComponent<RectTransform>();

        //Bars behind health and bond that emphasize the change
        healthFlash = GameObject.Find("barHealthFlash").GetComponent<Image>();
        bondFlash = GameObject.Find("barBondFlash").GetComponent<Image>();
        healthFlash.color = new Color(255f, 255f, 255f, 0f);
        bondFlash.color = new Color(255f, 255f, 255f, 0f);

        //The following are self explanatory
        enemyTurnComplete = true;

        //Canvas and panel needed for the fade effect
        canvas = GameObject.Find("FadeCanvas");
        fadePanel = GameObject.Find("fadePanel").GetComponent<Image>();
        canvas.SetActive(false);

        //Character animator for triggering events
        charAnimator = GameObject.Find("Character").GetComponent<Animator>();
    }

    void Update()
    {
        if (currentBond != selfStats.bond)
        { //Notifies of a bond change
            if (currentBond < selfStats.bond)
                BondChange(true);
            else
                BondChange(false);

            currentBond = selfStats.bond;
            bondBar.sizeDelta = new Vector2((float)selfStats.bond, (float)12.2);
        }

        if (enemyStats.current_health <= 0 && !enemyDead)
        { //Checks to see if enemy was just killed
            enemyDead = true;
            StartCoroutine(Finish(1));
        }

        if (selfStats.current_health < currentHealth)
        { //Checks for a health change
            currentHealth = selfStats.current_health;
            StartCoroutine(Hit());
            healthBar.sizeDelta = new Vector2((float)selfStats.current_health, (float)12.2);
        }

    }

    private double attackCheck(Stats tmp)
    { //Checks tone and characters attack to decide damage and bond change
        int toneIndex = GameObject.Find("GameManager").GetComponent<CombatButtons>().toneIndex;
        double damage = tmp.attack;

        switch (toneIndex)
        {
            case 1:
                if (bondBar.rect.width < 100)
                    GameObject.Find("Character").GetComponent<Stats>().bond += 5;
                break;
            case 2:
                if (bondBar.rect.width < 100)
                    GameObject.Find("Character").GetComponent<Stats>().bond += 10;
                damage = damage * 0.9;
                break;
            case 3:
                if (bondBar.rect.width > 0)
                    GameObject.Find("Character").GetComponent<Stats>().bond -= 12;
                damage = damage * 1.5;
                break;
        }

        return damage;
    }

    private IEnumerator Hit()
    { //Is triggered when the character is damaged
        charAnimator.SetTrigger("Damage");
        healthFlash.color = new Color(255f, 0, 0);
        healthFlash.CrossFadeAlpha(1f, 0f, false);
        healthFlash.CrossFadeAlpha(0f, 1.5f, false);
        yield return new WaitForSeconds(0.583f);
        charAnimator.ResetTrigger("Damage");
    }

    private void BondChange(bool change)
    { //Creates the flash effect when the bond changes
        if (change)
            bondFlash.color = new Color(0, 255f, 0);
        else
            bondFlash.color = new Color(255f, 0, 0);

        bondFlash.CrossFadeAlpha(1f, 0f, false);
        bondFlash.CrossFadeAlpha(0f, 1.5f, false);
    }

    public void Attack()
    { //Self explanatory
        if (!enemyDead && enemyTurnComplete)
        {
            //Calls function to calculate amount of damage dealt
            double attackAmt = attackCheck(selfStats);
            enemyStats.damage(attackAmt);

            //Display attack damage
            enemyHit.color = Color.red;
            enemyHit.CrossFadeAlpha(1, 0, true);
            enemyHit.text = attackAmt.ToString();
            enemyHit.CrossFadeAlpha(0, 1, false);
        }
    }

    public void Special()
    { //Special attack
        if (!enemyDead && enemyTurnComplete && specialCount == 0)
        {
            specialCount++;
            GameObject.Find("specialSpawn").GetComponent<SpecialAttack>().Fire();
        }
    }

    public void SpecialHit()
    { //Triggers when the special projectile hits the enemy
        enemyStats.damage(25);

        //Display attack damage
        enemyHit.color = Color.red;
        enemyHit.CrossFadeAlpha(1, 0, true);
        enemyHit.text = "25";
        enemyHit.CrossFadeAlpha(0, 1, false);

        specialCount--;
    }

    public IEnumerator Finish(int val)
    { //When either the player or enemy dies, this is called
        canvas.SetActive(true);
        this.GetComponent<CombatButtons>().transitioning = true;
        GameObject defeat = GameObject.Find("txtDefeat");
        GameObject victory = GameObject.Find("txtVictory");
        defeat.SetActive(false);
        victory.SetActive(false);

        //Will activate the correct text for either winning or losing
        if (val == 1)
        {
            victory.SetActive(true);
            GameObject.Find("MapManager").GetComponent<MapCanvas>().defeated = true;
        }
        else
            defeat.SetActive(true);
            
        //This section will be modified when death event is decided
        fadePanel.CrossFadeAlpha(0, 0, false);
        fadePanel.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Map");
    }
}
