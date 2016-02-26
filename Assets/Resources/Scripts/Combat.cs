using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class Combat : MonoBehaviour {

    public GameObject self;
    public GameObject enemy;
    private Stats enemyStats;
    private Stats selfStats;
    private bool enemyDead = false;
    private bool selfDead = false;
    private bool maxHealth = false;
    private bool enemyMaxHealth = false;
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
    }

    void Update()
    {
        //Keeps track of various bools during stat checking
        if (selfStats.current_health == selfStats.max_health)
            maxHealth = true;
        else
            maxHealth = false;
        
        if (enemyStats.current_health == enemyStats.max_health)
            enemyMaxHealth = true;
        else
            enemyMaxHealth = false;
        
        if (enemyStats.current_health <= 0)
            enemyDead = true;
        else
            enemyDead = false;
        
        if (selfStats.current_health <= 0)
            selfDead = true;
        else
            selfDead = false;

        healthBar.sizeDelta = new Vector2(selfStats.current_health, (float)12.2);
    }

    private int attackCheck(Stats tmp)
    {
        int damage = tmp.attack;
        return damage;
    }

    public void Attack()
    {
        if (!enemyDead)
        {
            //Calls function to calculate amount of damage dealt
            int attackAmt = attackCheck(selfStats);
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

    public void Hit()
    {
        if (!selfDead)
        {
            //Calls function to calculate damage dealt
            int attackAmt = attackCheck(enemyStats);
            selfStats.damage(attackAmt);

            //Display attack damage
            selfHit.color = Color.red;
            selfHit.CrossFadeAlpha(1, 0, true);
            selfHit.text = attackAmt.ToString();
            selfHit.CrossFadeAlpha(0, 1, false);
        } 
        else
            Debug.Log("You are dead");
    }
}
