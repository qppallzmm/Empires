using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangedUnitBehaviour : MonoBehaviour
{

    public Functions f;

    public GameObject itSelf;
    public Transform itsOwnTransform;
    public Rigidbody itsOwnRigidbody;

    public string enemyUnitTag;
    public string enemyBuildingTag;
    public string friendlyUnitTag;

    public UnitStats stats;
    UnitStats enemyUnitStats;
    BuildingsStats enemyBuildingStats;


    GameObject Enemy = null;
    float distance = 0.0f;
    float force;
    public Material color;
    Ray aim;
    bool lr;

    void Start()
    {
        color = f.findColor(itSelf);
        force = 2.0f * Mathf.Sqrt((5.0f * stats.range));
        if (Random.value > 0.5) lr = true;
        else lr = false;
    }

    void Update()
    {
        if (stats.active == true)
        {
            if (itsOwnTransform.position.x > 50 || itsOwnTransform.position.x < -50 || itsOwnTransform.position.z > 50 || itsOwnTransform.position.z < -50)
            {
                Destroy(itSelf);
            }
            if (stats.health <= 0)
            {
                Destroy(itSelf);
            }
            Enemy = f.closestEnemy(enemyUnitTag, itsOwnTransform.position, stats.attackBuildings, enemyBuildingTag);
            if (Enemy != null)
            {
                distance = f.distance(itsOwnTransform.position, Enemy);
                itsOwnTransform.LookAt(Enemy.transform);
                if (true)
                {
                    if (distance > stats.range)
                    {
                        itsOwnRigidbody.AddRelativeForce(Vector3.forward * Time.deltaTime * 720 * itsOwnRigidbody.mass);
                    }
                    if (distance < stats.range)
                    {
                        aim = new Ray(itsOwnTransform.position, itsOwnTransform.forward * 1000f);
                        RaycastHit info;
                        if (Physics.Raycast(aim, out info, Mathf.Infinity) && info.collider.gameObject.tag != friendlyUnitTag)
                        {
                            //itsOwnRigidbody.velocity = Vector3.zero;
                            stats.attackTimer += Time.deltaTime;
                            if (stats.attackTimer >= (stats.rateOf))
                            {
                                float angle = Mathf.Acos(Mathf.Sqrt((1.0f + Mathf.Sqrt(1 - ((20 * distance) / Mathf.Pow(force, 2.0f)))) / 2.0f));
                                stats.attackTimer -= stats.rateOf;
                                f.shoot(stats.projectile, itsOwnTransform.position, itSelf, angle, force, color);
                            }
                        }

                    }
                    if (distance < stats.range + 250)
                    {
                        aim = new Ray(itsOwnTransform.TransformDirection(Vector3.forward), itsOwnTransform.position + transform.forward);
                        RaycastHit info;
                        if (Physics.Raycast(aim, out info, 60.0f) && info.collider.gameObject.tag == friendlyUnitTag && info.collider.gameObject != itSelf)
                            f.swoop(itSelf, lr);
                    }
                }

            }
        }
    }
   void OnCollisionStay(Collision cl)
    {
        if(false)
        {
            Debug.Log("b");
        }
    }
    
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(aim);
        //Gizmos.DrawRay(itsOwnTransform.position, itsOwnTransform.forward);
    }

}

