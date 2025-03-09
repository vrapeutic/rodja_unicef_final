using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaiseEventWhenTriggerStay : MonoBehaviour
{
    float stayedTime ;
    [SerializeField]GameEvent eventToRaise;
    [SerializeField] float TimeToStay;
    [SerializeField] string wantedTag;
    bool canRaiseEvent = true;

    private void OnEnable()
    {
        stayedTime = 0;
        canRaiseEvent = true;
            
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.CompareTag(wantedTag)) 
            stayedTime += Time.deltaTime;
        if (stayedTime >= TimeToStay) RaiseEvent();
    }

    void RaiseEvent()
    {
        if (canRaiseEvent)
        {
            canRaiseEvent = false;
            eventToRaise.Raise();
        } 

    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.CompareTag(wantedTag))
        {
            stayedTime = 0;
            canRaiseEvent = true;
        }
    }

}
