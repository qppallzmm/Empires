using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowBehaviour : MonoBehaviour {

    public double arrowDamage = 10;
    public GameObject itSelf;
    public GameObject shooter;

    bool aa = true;

    private void Update()
    {
        if(aa == true)
            itSelf.transform.forward = Vector3.Slerp(itSelf.transform.forward, itSelf.GetComponent<Rigidbody>().velocity.normalized, Time.deltaTime);
    }

    void OnCollisionEnter(Collision cl)
    {
        if ((cl.gameObject.tag == "RedUnit" || cl.gameObject.tag == "BlueUnit") && cl.gameObject.name != "Ground" && cl.gameObject.name != "Arrow" && cl.gameObject != shooter && aa == true)
        {
            UnitStats stat = cl.gameObject.GetComponent<UnitStats>();
            stat.health -= arrowDamage;
            //Destroy(itSelf);
        }
        if (cl.gameObject.name != "Arrow" && cl.gameObject != shooter)
        {
            aa = false;
            itSelf.transform.SetParent(cl.transform);
            itSelf.transform.localScale = itSelf.transform.localScale / cl.transform.localScale.y;
            Destroy(itSelf.GetComponent<Rigidbody>());
            Destroy(itSelf.GetComponent<BoxCollider>());
        }
    }
}