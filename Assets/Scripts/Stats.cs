using UnityEngine;
using System.Collections;

public class Stats : MonoBehaviour {

    public int max_health;
    public int current_health;
    public int base_damage;
    public int element_fire;
    public int element_water;
    public int element_earth;
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
        base_damage += amount;
    }

    public void elementalDamageChange( int fire, int water, int earth )
    {
        element_fire += fire;
        element_water += water;
        element_earth += earth;
    }

    public void levelChange( int amount )
    {
        level += amount;
    }
}
