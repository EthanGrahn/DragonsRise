using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int max_health;
    public int current_health;
    public int healing_power;
    public int attack;
    public int speed;
    public int special_fire;
    public int special_ice;
    public int special_wind;
    public int special_electric;
    public int level;

    public void damage( int amount )
    {
        current_health -= amount;
    }

    public void heal( int amount )
    {
        current_health += amount;
    }

    public void max_healthChange( int amount )
    {
        max_health += amount;
    }

    public void base_damageChange( int amount )
    {
        attack += amount;
    }

    public void elementalDamageChange()
    {
    }

    public void healingChange( int amount )
    {
        healing_power += amount;
    }

    public void levelChange( int amount )
    {
        level += amount;
    }
}
