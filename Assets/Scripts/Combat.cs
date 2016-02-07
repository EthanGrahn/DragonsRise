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
        enemyHealthTxt.text = "Health: " + enemyStats.current_health;
        selfHealthTxt.text = "Health: " + selfStats.current_health;
    }

    private int attackCheck(Stats tmp)
    {
        int damage = tmp.base_damage;
        return damage;
    }

    public void Attack()
    {
        if (!enemyDead)
        {
            enemyStats.damage(attackCheck(selfStats));
            int health = enemyStats.current_health;
            //Debug.Log("Enemy health = " + health);
            enemyHit.color = Color.red;
            enemyHit.CrossFadeAlpha(1, 0, true);
            enemyHit.text = "-20";
            enemyHit.CrossFadeAlpha(0, 1, false);
        } 
        else
            Debug.Log("Enemy dead");
    }

    public void Heal()
    {
        if (!selfDead && !maxHealth)
        {
            int health = selfStats.current_health;
            selfStats.heal(10);
            //Debug.Log("Character health = " + health);
            selfHit.color = Color.green;
            selfHit.CrossFadeAlpha(1, 0, true);
            selfHit.text = "+10";
            selfHit.CrossFadeAlpha(0, 1, false);
        } else if (maxHealth)
            Debug.Log("You are at max health");
        else
            Debug.Log("You are dead");
    }

    public void enemyHeal()
    {
        if (!enemyDead && !enemyMaxHealth)
        {
            int health = enemyStats.current_health;
            enemyStats.heal(10);
           // Debug.Log("Enemy health = " + health);
            enemyHit.color = Color.green;
            enemyHit.CrossFadeAlpha(1, 0, true);
            enemyHit.text = "+10";
            enemyHit.CrossFadeAlpha(0, 1, false);
        } else if (maxHealth)
            Debug.Log("Enemy at max health");
        else
            Debug.Log("Enemy is dead");
    }

    public void Hit()
    {
        if (!selfDead)
        {
            selfStats.damage(attackCheck(enemyStats));
            selfHit.color = Color.red;
            selfHit.CrossFadeAlpha(1, 0, true);
            selfHit.text = "-5";
            selfHit.CrossFadeAlpha(0, 1, false);
        } 
        else
            Debug.Log("You are dead");
    }
}
