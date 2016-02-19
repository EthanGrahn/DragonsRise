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

    void Start()
    {
        //Initializes stat veriables along with overhead text boxes
        enemyStats = enemy.GetComponent<Stats>();
        selfStats = self.GetComponent<Stats>();

        //Initializes text components for the overhead displays
        enemyHit = GameObject.Find("enemyHitTxt").GetComponent<Text>();
        selfHit = GameObject.Find("playerHitTxt").GetComponent<Text>();
        enemyHealthTxt = GameObject.Find("enemyHealthTxt").GetComponent<Text>();
        selfHealthTxt = GameObject.Find("playerHealthTxt").GetComponent<Text>();
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

        //Displays constant health above characters' head
        //Adjust to use with health bars
        enemyHealthTxt.text = "Health: " + enemyStats.current_health;
        selfHealthTxt.text = "Health: " + selfStats.current_health;
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

    public void Defend()
    {/* Rethink this for defense mechanics
        if (!selfDead && !maxHealth)
        {
            selfHit.color = Color.green;
            selfHit.CrossFadeAlpha(1, 0, true);
            selfHit.text = "+10";
            selfHit.CrossFadeAlpha(0, 1, false);
        } else if (maxHealth)
            Debug.Log("You are at max health");
        else
            Debug.Log("You are dead");*/
    }

    public void enemyDefend()
    {
        
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
