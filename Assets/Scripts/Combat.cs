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
    private Text enemyHit;

    void Start()
    {
        enemyStats = enemy.GetComponent<Stats>();
        selfStats = self.GetComponent<Stats>();
        enemyHit = GameObject.Find("enemyHitTxt").GetComponent<Text>();
    }

    void Update()
    {
        if (selfStats.current_health == selfStats.max_health)
            maxHealth = true;
        if (enemyStats.current_health <= 0)
            enemyDead = true;
        if (selfStats.current_health <= 0)
            selfDead = true;
    }

    public void Attack()
    {
        if (!enemyDead)
        {
            enemyStats.damage(20);
            int health = enemyStats.current_health;
            Debug.Log("Enemy health = " + health);
            enemyHit.color = Color.red;
            enemyHit.text = "-20";

            enemyHit.text = "";
        } 
        else
            Debug.Log("Enemy dead");
    }

    public void Heal()
    {
        if (!selfDead && !maxHealth)
        {
            selfStats.heal(10);
            int health = selfStats.current_health;
            Debug.Log("Character health = " + health);
        } else if (maxHealth)
            Debug.Log("You are at max health");
        else
            Debug.Log("You are dead");
    }

    public void Hit()
    {
        if (!selfDead)
        {
            selfStats.damage(5);
        }
    }
}
