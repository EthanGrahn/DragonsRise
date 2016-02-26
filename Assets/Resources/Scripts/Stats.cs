using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int max_health;
    public int current_health;
    public int attack;
    public int special_fire;
    public int special_ice;
    public int bond;
    public int level;

    public void damage( int amount )
    {
        current_health -= amount;
    }

    public void heal( int amount )
    {
        current_health += amount;
    }
}
