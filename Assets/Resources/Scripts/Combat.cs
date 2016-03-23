using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class Combat : MonoBehaviour {

    public GameObject self;
    public GameObject enemy;
    private Stats enemyStats;
    private Stats selfStats;

    private bool enemyDead = false;
    public bool enemyTurnComplete;
    public int specialCount = 0;

    private Text enemyHit;
    private Text selfHit;
    private Text enemyHealthTxt;
    private Text selfHealthTxt;

    private RectTransform healthBar;
    private RectTransform bondBar;

    private Image fadePanel;
    private GameObject canvas;

    void Start()
    {
        //Initializes stat veriables along with overhead text boxes
        enemyStats = enemy.GetComponent<Stats>();
        selfStats = self.GetComponent<Stats>();

        selfHit = GameObject.Find("txtSelfHit").GetComponent<Text>();
        enemyHit = GameObject.Find("txtEnemyHit").GetComponent<Text>();

        healthBar = GameObject.Find("barHealth").GetComponent<RectTransform>();
        bondBar = GameObject.Find("barBond").GetComponent<RectTransform>();

        enemyTurnComplete = true;

        fadePanel = GameObject.Find("fadePanel").GetComponent<Image>();
        canvas = GameObject.Find("FadeCanvas");
        canvas.SetActive(false);
    }

    void Update()
    {      
        if (enemyStats.current_health <= 0 && !enemyDead)
        {
            enemyDead = true;
            StartCoroutine(Finish(1));
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
        Debug.Log("Finish");
        canvas.SetActive(true);
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
