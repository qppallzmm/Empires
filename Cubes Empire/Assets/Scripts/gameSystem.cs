using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class gameSystem : MonoBehaviour {

    bool b = true;
    bool bb = false;

    public void setB(bool trueOrFalse)
    {
        b = trueOrFalse;
    }
    public void setBB(bool trueOrFalse)
    {
        bb = trueOrFalse;
    }

    void Update()
    {

        if (b == false)
        {
            GameObject[] RedUnitList;
            RedUnitList = GameObject.FindGameObjectsWithTag("RedUnit");
            GameObject[] BlueUnitList;
            BlueUnitList = GameObject.FindGameObjectsWithTag("BlueUnit");
            GameObject[] RedBuildingList;
            RedBuildingList = GameObject.FindGameObjectsWithTag("RedBuilding");
            GameObject[] BlueBuildingList;
            BlueBuildingList = GameObject.FindGameObjectsWithTag("BlueBuilding");

            foreach(GameObject o in RedUnitList)
            {
                UnitStats stat = o.GetComponent(typeof(UnitStats)) as UnitStats;
                stat.active = false;
            }
            foreach (GameObject o in BlueUnitList)
            {
                UnitStats stat = o.GetComponent(typeof(UnitStats)) as UnitStats;
                stat.active = false;
            }
            foreach (GameObject o in RedBuildingList)
            {
                BuildingsStats stat = o.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
                stat.active = false;
            }
            foreach (GameObject o in BlueBuildingList)
            {
                BuildingsStats stat = o.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
                stat.active = false;
            }
        }
        else
        {
            GameObject[] RedUnitList;
            RedUnitList = GameObject.FindGameObjectsWithTag("RedUnit");
            GameObject[] BlueUnitList;
            BlueUnitList = GameObject.FindGameObjectsWithTag("BlueUnit");
            GameObject[] RedBuildingList;
            RedBuildingList = GameObject.FindGameObjectsWithTag("RedBuilding");
            GameObject[] BlueBuildingList;
            BlueBuildingList = GameObject.FindGameObjectsWithTag("BlueBuilding");

            foreach (GameObject o in RedUnitList)
            {
                UnitStats stat = o.GetComponent(typeof(UnitStats)) as UnitStats;
                stat.active = true;
            }
            foreach (GameObject o in BlueUnitList)
            {
                UnitStats stat = o.GetComponent(typeof(UnitStats)) as UnitStats;
                stat.active = true;
            }
            foreach (GameObject o in RedBuildingList)
            {
                BuildingsStats stat = o.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
                stat.active = true;
            }
            foreach (GameObject o in BlueBuildingList)
            {
                BuildingsStats stat = o.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
                stat.active = true;
            }
        }

        if (bb == true)
        {
            SceneManager.LoadScene("Main");
            bb = false;
        }

    }
}
