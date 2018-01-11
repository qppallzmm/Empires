using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitStats : MonoBehaviour {

    public string category;
    public bool active;

    public double health;
    public double attack;
    public double rateOf;
    public double speed;

    public bool attackBuildings = true;
    public bool areaDamage = false;
    public bool ranged = false;

    public float area;
    public float range;
    public GameObject projectile;
    

    public double attackTimer;
}
