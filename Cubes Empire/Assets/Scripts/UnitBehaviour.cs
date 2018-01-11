using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitBehaviour : MonoBehaviour {

    public Functions fn;

    public GameObject itself;
    public Transform itsOwnTrasform;
    public Rigidbody itsOwnRigidbody;

    public string enemyUnitTag;
    public string enemyBuildingTag;

    public UnitStats stats;
    UnitStats enemyUnitStats;
    BuildingsStats enemyBuildingStats;
    bool isGrounded = true;
    

    GameObject Enemy = null;

    void Update()
    {
        if (stats.active == true)
        {
            if (itsOwnTrasform.position.x > 50 || itsOwnTrasform.position.x < -50 || itsOwnTrasform.position.z > 50 || itsOwnTrasform.position.z < -50)
            {
                Destroy(itself);
            }
            if (stats.health <= 0)
            {
                Destroy(itself);
            }
            Enemy = fn.closestEnemy(enemyUnitTag, itsOwnTrasform.position, stats.attackBuildings, enemyBuildingTag);
            if (Enemy != null)
            {
                itsOwnTrasform.LookAt(Enemy.transform);
            }
            if (isGrounded == true)
            {
                itsOwnRigidbody.AddRelativeForce(Vector3.forward * Time.deltaTime * 720 * itsOwnRigidbody.mass);
            }

        }
    }
    void OnCollisionStay(Collision cl)
    {
        if (stats.areaDamage == false)
        {
            if (cl.gameObject.tag == enemyBuildingTag)
            {
                stats.attackTimer += Time.deltaTime;

                if (stats.attackTimer >= (stats.rateOf))
                {
                    stats.attackTimer -= stats.rateOf;

                    enemyBuildingStats = cl.gameObject.GetComponent(typeof(BuildingsStats)) as BuildingsStats;
                    enemyBuildingStats.health -= stats.attack;

                }
            }
            else if (cl.gameObject.tag == enemyUnitTag)
            {
                stats.attackTimer += Time.deltaTime;

                if (stats.attackTimer >= (stats.rateOf))
                {
                    stats.attackTimer -= stats.rateOf;

                    enemyUnitStats = cl.gameObject.GetComponent(typeof(UnitStats)) as UnitStats;
                    enemyUnitStats.health -= stats.attack;

                }
            }
        }
        else
        {
            stats.attackTimer += Time.deltaTime;

            if (stats.attackTimer >= (stats.rateOf))
            {
                stats.attackTimer -= stats.rateOf;
                Collider[] units = Physics.OverlapSphere(itsOwnTrasform.position, stats.area);
                foreach (Collider col in units)
                {
                    if (col.attachedRigidbody != null && (col.gameObject.tag == enemyUnitTag || col.gameObject.tag == enemyBuildingTag))
                    {
                        col.attachedRigidbody.AddExplosionForce(3000.0f, itsOwnTrasform.position, stats.area, 3.0f);
                        if (col.gameObject.tag == enemyUnitTag)
                        {
                            enemyUnitStats = col.gameObject.GetComponent(typeof(UnitStats)) as UnitStats;
                            enemyUnitStats.health -= stats.attack;
                        }

                    }
                }
            }
        }
    }
    void OnDrawGizmos()
    {
        Gizmos.color = new Color(1, 1, 0, 0.75F);
        Gizmos.DrawSphere(itsOwnTrasform.position, stats.area);
    }

}
