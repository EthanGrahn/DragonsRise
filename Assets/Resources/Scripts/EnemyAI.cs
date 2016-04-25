using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class EnemyAI : MonoBehaviour {

    private BoxCollider2D enemyCollider;
    private Stats stats;
    private Stats charStats;
    private Text charHit;
    private bool charDead;
    private double health;
    public bool turnComplete;


	public void EnemyStart () 
    {
        stats = this.GetComponent<Stats>();
        charStats = GameObject.Find("Character").GetComponent<Stats>();
        enemyCollider = GameObject.Find("EnemyCollider").GetComponent<BoxCollider2D>();
        charDead = false;
        turnComplete = true;
        charHit = GameObject.Find("txtSelfHit").GetComponent<Text>();
        StatCheck();
	}
	

	void Update () 
    {
        if (charStats.current_health <= 0 && !charDead)
        {
            charDead = true;
            StartCoroutine(GameObject.Find("GameManager").GetComponent<Combat>().Finish(2));
        }

        if (stats.current_health < health)
        {
            turnComplete = false;
            GameObject.Find("GameManager").GetComponent<Combat>().enemyTurnComplete = false;
            health = stats.current_health;
            StartCoroutine(Hit());
        }
	}

    private void StatCheck()
    {
        health = stats.current_health;
    }

    private double AttackCheck(Stats tmp)
    {
        double attack = tmp.attack;
        return attack;
    }

    private IEnumerator Hit()
    {
        if (!charDead && stats.current_health > 0)
        {
            yield return new WaitForSeconds(1f);
            double attackAmt = AttackCheck(stats);
            charStats.damage(attackAmt);

            //Display attack damage
            charHit.color = Color.red;
            charHit.CrossFadeAlpha(1, 0, true);
            charHit.text = attackAmt.ToString();
            charHit.CrossFadeAlpha(0, 1, false);
        }

        yield return new WaitForSeconds(1f);
        turnComplete = true;
        GameObject.Find("GameManager").GetComponent<Combat>().enemyTurnComplete = true;
    }
}
