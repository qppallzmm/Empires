using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Functions : MonoBehaviour {

    public Material blue;
    public Material red;
    public Material Secondary;

    public GameObject warrior;
    public GameObject giant;
    public GameObject archer;

    public GameObject barrack;
    public GameObject giantBarrack;
    public GameObject archerBarrack;

    public GameObject projectile;

    //-------------------------------

    public GameObject closestEnemy(string tag, Vector3 tr, bool ed, string tag2)
    {
        GameObject[] EnemyList;
        EnemyList = GameObject.FindGameObjectsWithTag(tag);
        GameObject closest = null;
        float distance = Mathf.Infinity;
        foreach(GameObject En in EnemyList)
        {
            Vector3 diff = En.transform.position - tr;
            float curDistance = diff.sqrMagnitude;
            if (curDistance < distance && En.transform.position.y < 1.5f)
            {
                closest = En;
                distance = curDistance;
            }
        }
        if (ed == true)
        {
            GameObject[] EnemyList2;
            EnemyList2 = GameObject.FindGameObjectsWithTag(tag2);
            foreach (GameObject En in EnemyList2)
            {
                Vector3 diff2 = En.transform.position - tr;
                float curDistance2 = diff2.sqrMagnitude;
                if (curDistance2 < distance)
                {
                    closest = En;
                    distance = curDistance2;
                }
            }
        }
        return closest;

    }
    public float distance(Vector3 tr, GameObject closest)
    {
        Vector3 diff = closest.transform.position - tr;
        float curDistance = diff.sqrMagnitude;
        return curDistance;
    }

    public GameObject spawn(GameObject original, Vector3 position, Quaternion rotation, Material color, string name, string unitTag, char type, float offset)
    {
        GameObject newObject;
        position.y = offset;
        newObject = Instantiate(original, position, rotation) as GameObject;
        newObject.SetActive(true);
        Renderer[] hRend = newObject.GetComponentsInChildren<Renderer>();
        foreach (Renderer rnd in hRend)
        {
            Material[] col = rnd.materials;
            col[0] = color;
            rnd.materials = col;
        }
        newObject.name = name;
        newObject.tag = unitTag;
        return newObject;
    }
    public void setStats(GameObject newUnit, char type, string unitClass, int level)
    {
        UnitStats unitS = newUnit.GetComponent(typeof(UnitStats)) as UnitStats;
        BuildingsStats buildingS = newUnit.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
        if (type == 'u')
        {
            switch (unitClass)
            {
                case "warrior":
                    unitS.category = "warrior";
                    unitS.active = true;
                    unitS.health = 10;
                    unitS.attack = 3;
                    unitS.rateOf = 0.3;
                    unitS.speed = 650;
                    break;
                case "giant":
                    unitS.category = "giant";
                    unitS.active = true;
                    unitS.health = 180;
                    unitS.attack = 4;
                    unitS.rateOf = 3;
                    unitS.speed = 300;
                    unitS.areaDamage = true;
                    unitS.area = 6.0f;
                    break;
                case "archer":
                    unitS.category = "archer";
                    unitS.active = true;
                    unitS.health = 2;
                    unitS.attack = 2;
                    unitS.rateOf = 0.7;
                    unitS.speed = 500;
                    unitS.ranged = true;
                    unitS.range = 4000.0f;
                    unitS.projectile = projectile;
                    unitS.attackBuildings = false;
                    break;
            }

        }
        if (type == 'b')
        {
            switch (unitClass)
            {
                case "barrack":
                    buildingS.category = "Spawner";
                    buildingS.unit = "warrior";
                    buildingS.health = 50;
                    buildingS.active = true;
                    buildingS.attack = 0;
                    buildingS.rateOf = 0.7;
                    break;
                case "giantBarrack":
                    buildingS.category = "Spawner";
                    buildingS.unit = "giant";
                    buildingS.health = 120;
                    buildingS.active = true;
                    buildingS.attack = 0;
                    buildingS.rateOf = 5.0;
                    break;
                case "archerBarrack":
                    buildingS.category = "Spawner";
                    buildingS.unit = "archer";
                    buildingS.health = 40;
                    buildingS.active = true;
                    buildingS.attack = 0;
                    buildingS.rateOf = 1.3;
                    break;
            }
        }
    }
    public void setBehaviour(GameObject newUnit, string unitClass, Material color)
    {
        UnitStats stats = newUnit.GetComponent<UnitStats>();
        UnitBehaviour unitB = newUnit.GetComponent(typeof(UnitBehaviour)) as UnitBehaviour;
        RangedUnitBehaviour unitBR = newUnit.GetComponent(typeof(RangedUnitBehaviour)) as RangedUnitBehaviour;

        if (stats.ranged == false)
        {

            if (color == red)
            {
                unitB.enemyUnitTag = "BlueUnit";
                unitB.enemyBuildingTag = "BlueBuilding";
            }
            if (color == blue)
            {
                unitB.enemyUnitTag = "RedUnit";
                unitB.enemyBuildingTag = "RedBuilding";
            }
        }
        else
        {

            if (color == red)
            {
                unitBR.enemyUnitTag = "BlueUnit";
                unitBR.enemyBuildingTag = "BlueBuilding";
                unitBR.friendlyUnitTag = "RedUnit";
            }
            if (color == blue)
            {
                unitBR.enemyUnitTag = "RedUnit";
                unitBR.enemyBuildingTag = "RedBuilding";
                unitBR.friendlyUnitTag = "BlueUnit";
            }
        }
    }
    public string rename(string unitClass, Material color, char type)
    {
        string newName = " ";
        if(type == 'b')
        {
            if(color == blue)
            {
                switch (unitClass)
                {
                    case "barrack":
                        newName = "Blue Barrack";
                        break;
                    case "giantBarrack":
                        newName = "Blue Giant Barrack";
                        break;
                    case "archerBarrack":
                        newName = "Blue Archer Barrack";
                        break;
                }
            }
            if (color == red)
            {
                switch (unitClass)
                {
                    case "barrack":
                        newName = "Red Barrack";
                        break;
                    case "giantBarrack":
                        newName = "Red Giant Barrack";
                        break;
                    case "archerBarrack":
                        newName = "Red Archer Barrack";
                        break;
                }
            }
        }
        if (type == 'u')
        {
            if (color == blue)
            {
                switch (unitClass)
                {
                    case "warrior":
                        newName = "Blue Warrior";
                        break;
                    case "giant":
                        newName = "Blue Giant";
                        break;
                    case "archer":
                        newName = "Blue Archer";
                        break;
                }
            }
            if (color == red)
            {
                switch (unitClass)
                {
                    case "warrior":
                        newName = "Red Warrior";
                        break;
                    case "giant":
                        newName = "Red Giant";
                        break;
                    case "archer":
                        newName = "Red Archer";
                        break;
                }
            }
        }
        return newName;
    }
    public string tagger(Material color, char type)
    {
        string tagg = " ";
        if(type == 'u')
        {
            if (color == red)
            {
                tagg = "RedUnit";
            }
            if (color == blue)
            {
                tagg = "BlueUnit";
            }
        }
        if (type == 'b')
        {
            if (color == red)
            {
                tagg = "RedBuilding";
            }
            if (color == blue)
            {
                tagg = "BlueBuilding";
            }
        }

        return tagg;
    }

    public Material findColor(GameObject building)
    {
        Material mat = null;
        switch (building.name[0])
        {
            case 'R':
                mat = red;
                break;
            case 'B':
                mat = blue;
                break;
        }
        return mat;
    }

    public string keySensor()
    {
        string keyS = "Null";
        if (Input.GetKeyDown("q"))
        {
            keyS = "q";
        }
        if (Input.GetKeyDown("w"))
        {
            keyS = "w";
        }
        if (Input.GetKeyDown("a"))
        {
            keyS = "a";
        }
        if (Input.GetKeyDown("s"))
        {
            keyS = "s";
        }
        if (Input.GetKeyDown("z"))
        {
            keyS = "z";
        }
        if (Input.GetKeyDown("x"))
        {
            keyS = "x";
        }
        return keyS;
    }
    public int stateActivator(int oldState, string inp)
    {
        int state;
        switch (inp)
        {
            case "q":
                state = 1;
                break;
            case "w":
                state = 2;
                break;
            case "a":
                state = 3;
                break;
            case "s":
                state = 4;
                break;
            case "z":
                state = 5;
                break;
            case "x":
                state = 6;
                break;
            default:
                state = oldState;
                break;
        }
        return state;
    }

    public void shoot(GameObject projectile, Vector3 position, GameObject arch, float angle, float force, Material color)
    {
        GameObject arrow = Instantiate(projectile, position + arch.transform.forward, arch.transform.rotation);



        arrow.SetActive(true);
        arrow.name = "Arrow";

        ArrowBehaviour be = arrow.GetComponent<ArrowBehaviour>();
        be.shooter = arch;
        Rigidbody rb = arrow.GetComponent<Rigidbody>();

        Renderer rnd = arrow.GetComponentInChildren<Renderer>();
        Material[] col = rnd.materials;
        col[0] = color;
        rnd.materials = col;

        rb.velocity = ((arch.transform.forward * force * Mathf.Cos(angle) * 0.110f) + (arch.transform.up * force* Mathf.Sin(angle)) * 0.07f);
    }
    public void swoop(GameObject unit, bool lr)
    {
        Debug.Log("Swooping");

        if (lr)unit.GetComponent<Rigidbody>().AddRelativeForce(unit.GetComponent<Transform>().right * -2000 * Time.deltaTime);
        else unit.GetComponent<Rigidbody>().AddRelativeForce(unit.GetComponent<Transform>().right * 2000 * Time.deltaTime);
    }

}
