using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public double max_health;
    public double current_health;
    public double attack;
    public double special_fire;
    public double special_ice;
    public double bond;
    public int level;

    public void damage( double amount )
    {
        current_health -= amount;
    }

    public void heal( double amount )
    {
        current_health += amount;
    }
}
