using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Combat : MonoBehaviour {

    public GameObject self;
    public GameObject enemy;
    private Stats enemyStats;
    private Stats selfStats;
    private bool enemyDead = false;
    private bool enemyTurnComplete;
    public int specialCount = 0;
    private Text enemyHit;
    private Text selfHit;
    private Text enemyHealthTxt;
    private Text selfHealthTxt;
    private RectTransform healthBar;
    private RectTransform bondBar;

    void Start()
    {
        //Initializes stat veriables along with overhead text boxes
        enemyStats = enemy.GetComponent<Stats>();
        selfStats = self.GetComponent<Stats>();

        selfHit = GameObject.Find("txtSelfHit").GetComponent<Text>();
        enemyHit = GameObject.Find("txtEnemyHit").GetComponent<Text>();

        healthBar = GameObject.Find("barHealth").GetComponent<RectTransform>();
        bondBar = GameObject.Find("barBond").GetComponent<RectTransform>();

        enemyTurnComplete = GameObject.Find("Enemy").GetComponent<EnemyAI>().turnComplete;
    }

    void Update()
    {
        //Keeps track of various bools during stat checking        
        if (enemyStats.current_health <= 0)
            enemyDead = true;
        else
            enemyDead = false;

        enemyTurnComplete = GameObject.Find("Enemy").GetComponent<EnemyAI>().turnComplete;

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
                    GameObject.Find("Character").GetComponent<Stats>().bond += 2;
                break;
            case 2:
                if (bondBar.rect.width < 100)
                    GameObject.Find("Character").GetComponent<Stats>().bond += 5;
                damage = damage * 0.9;
                break;
            case 3:
                if (bondBar.rect.width > 0)
                    GameObject.Find("Character").GetComponent<Stats>().bond -= 8;
                damage = damage * 1.5;
                break;
        }

        return damage;
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
        else
            Debug.Log("Enemy dead");
    }

    public void Special()
    {
        if (!enemyDead && enemyTurnComplete)
        {
            specialCount++;
            GameObject.Find("specialSpawn").GetComponent<SpecialAttack>().Fire();
        }
    }

    public void SpecialHit()
    {
        if (!enemyDead)
        {
            enemyStats.damage(25);

            //Display attack damage
            enemyHit.color = Color.red;
            enemyHit.CrossFadeAlpha(1, 0, true);
            enemyHit.text = "25";
            enemyHit.CrossFadeAlpha(0, 1, false);
        }
        else
            Debug.Log("Enemy dead");

        specialCount--;
    }
}
