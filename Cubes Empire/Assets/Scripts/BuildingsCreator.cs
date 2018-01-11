using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingsCreator : MonoBehaviour {

    public Functions f;

    public Transform ground;

    GameObject original;
    Renderer rend;

    Camera cam;
    Ray toM;
    RaycastHit rhInfo;
    int buildingB = 0;
    Quaternion rotation = Quaternion.identity;

    string buildingSpawned;
    string newName;
    string newTag;

    Material colorUsed;
    float offset;


    void Start()
    {
        cam = Camera.main;
        rotation = ground.rotation;
    }

	void Update () {
        string inp = f.keySensor();
        buildingB = f.stateActivator(buildingB, inp);

        switch (buildingB)
        {
            case 1:
                buildingSpawned = "giantBarrack";
                original = f.giantBarrack;
                offset = 1.5f;
                break;
            case 2:
                buildingSpawned = "giantBarrack";
                original = f.giantBarrack;
                offset = 1.5f;
                break;
            case 3:
                buildingSpawned = "barrack";
                original = f.barrack;
                offset = 2.80f;
                break;
            case 4:
                buildingSpawned = "barrack";
                original = f.barrack;
                offset = 2.80f;
                break;
            case 5:
                buildingSpawned = "archerBarrack";
                original = f.archerBarrack;
                offset = 1.4f;
                break;
            case 6:
                buildingSpawned = "archerBarrack";
                original = f.archerBarrack;
                offset = 1.4f;
                break;
        }

        if (buildingB % 2 == 0)
        {
            colorUsed = f.blue;
            rotation.Set(0, 180, 0, 0);
        }
        else
        {
            colorUsed = f.red;
            rotation.Set(0, 0, 0, 0);

        }
        if (Input.GetMouseButtonDown(0) && buildingB != 0)
            {
            newName = f.rename(buildingSpawned, colorUsed, 'b');
            newTag = f.tagger(colorUsed, 'b');
                toM = cam.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(toM, out rhInfo, 500.0f) == true && rhInfo.transform == ground)
                {
                    GameObject newBuilding;
                    newBuilding = f.spawn(original, rhInfo.point, rotation, colorUsed, newName, newTag, 'b', offset);
                    f.setStats(newBuilding, 'b', buildingSpawned, 1);
                }
            }
	}

    
}
