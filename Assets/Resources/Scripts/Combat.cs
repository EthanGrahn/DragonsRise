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
    private double health;

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

        canvas = GameObject.Find("FadeCanvas");
        fadePanel = GameObject.Find("fadePanel").GetComponent<Image>();
        canvas.SetActive(false);

        health = selfStats.current_health;
        charAnimator = GameObject.Find("Character").GetComponent<Animator>();
    }

    void Awake()
    {
        
    }

    void Update()
    {
        if (currentBond != selfStats.bond)
        {
            if (currentBond < selfStats.bond)
                BondChange(true);
            else
                BondChange(false);

            currentBond = selfStats.bond;
        }

        if (enemyStats.current_health <= 0 && !enemyDead)
        {
            enemyDead = true;
            StartCoroutine(Finish(1));
        }

        if (selfStats.current_health < health)
        {
            health = selfStats.current_health;
            StartCoroutine(Hit());
        }

        healthBar.sizeDelta = new Vector2((float)selfStats.current_health, (float)12.2);
        bondBar.sizeDelta = new Vector2((float)selfStats.bond, (float)12.2);
    }

    private double attackCheck(Stats tmp)
    {
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
    {
        charAnimator.SetTrigger("Damage");
        healthFlash.color = new Color(255f, 0, 0);
        healthFlash.CrossFadeAlpha(1f, 0f, false);
        healthFlash.CrossFadeAlpha(0f, 1.5f, false);
        yield return new WaitForSeconds(0.583f);
        charAnimator.ResetTrigger("Damage");
    }

    private void BondChange(bool change)
    {
        if (change)
            bondFlash.color = new Color(0, 255f, 0);
        else
            bondFlash.color = new Color(255f, 0, 0);

        bondFlash.CrossFadeAlpha(1f, 0f, false);
        bondFlash.CrossFadeAlpha(0f, 1.5f, false);
    }

    public void Attack()
    {
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
    {
        if (!enemyDead && enemyTurnComplete && specialCount == 0)
        {
            specialCount++;
            GameObject.Find("specialSpawn").GetComponent<SpecialAttack>().Fire();
        }
    }

    public void SpecialHit()
    {
        enemyStats.damage(25);

        //Display attack damage
        enemyHit.color = Color.red;
        enemyHit.CrossFadeAlpha(1, 0, true);
        enemyHit.text = "25";
        enemyHit.CrossFadeAlpha(0, 1, false);

        specialCount--;
    }

    public IEnumerator Finish(int val)
    {
        canvas.SetActive(true);
        this.GetComponent<CombatButtons>().transitioning = true;
        GameObject defeat = GameObject.Find("txtDefeat");
        GameObject victory = GameObject.Find("txtVictory");
        defeat.SetActive(false);
        victory.SetActive(false);

        if (val == 1)
        {
            victory.SetActive(true);
            GameObject.Find("MapManager").GetComponent<MapCanvas>().defeated = true;
        }
        else
            defeat.SetActive(true);
            

        fadePanel.CrossFadeAlpha(0, 0, false);
        fadePanel.CrossFadeAlpha(1, 2, false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Map");
    }
}
