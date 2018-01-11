using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingBehaviour : MonoBehaviour {

    public GameObject building;
    public BuildingsStats stats;
    public Functions f;

    double timer;

    string unitSpawned;
    Material color;
    GameObject original;
    GameObject newUnit;
    string unitName;
    string unitTag;

    void Start()
    {
        unitSpawned = stats.unit;
        color = f.findColor(building);
        unitName = f.rename(unitSpawned, color, 'u');
        unitTag = f.tagger(color, 'u');
        switch (unitSpawned)
        {
            case "warrior":
                original = f.warrior;
                break;
            case "giant":
                original = f.giant;
                break;
            case "archer":
                original = f.archer;
                break;
        }
    }

    void Update()
    {
        if (stats.active == true)
        {
            timer += Time.deltaTime;

            if (stats.health <= 0)
            {
                Destroy(building);
            }


            if (stats.category == "Spawner")
            {
                if (timer >= stats.rateOf)
                {
                    timer -= stats.rateOf;
                    newUnit = f.spawn(original, building.transform.position, building.transform.rotation, color, unitName, unitTag, 'u', 0.0f);
                    f.setStats(newUnit, 'u', unitSpawned, 1);
                    f.setBehaviour(newUnit, unitSpawned, color);
                    newUnit.GetComponent<Rigidbody>().AddForce(newUnit.transform.forward * 300);
                }
            }
        }
    }
}
