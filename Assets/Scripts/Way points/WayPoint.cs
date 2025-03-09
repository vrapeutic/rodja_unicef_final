using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(Collider))]
public class WayPoint : MonoBehaviour
{
    // Start is called before the first frame update
    public Vector3 myPosition;
    WayPointsPath wayPointParent;
    public bool isStopPoint;

    void Start()
    {
        myPosition = this.gameObject.transform.position;
        this.GetComponent<Rigidbody>().isKinematic = true;
        this.GetComponent<Rigidbody>().useGravity = false;
        this.GetComponent<Collider>().isTrigger = true;
        if(wayPointParent == null) wayPointParent = this.GetComponentInParent<WayPointsPath>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("agent"))
        {
            if(isStopPoint)
            wayPointParent.AgentArrivedAtStopPoint();
            else
                wayPointParent.AgentArrivedAtNoneStopPoint();

            this.GetComponent<Collider>().enabled = false;
        }           
    }
}
