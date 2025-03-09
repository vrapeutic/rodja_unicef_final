using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.XR.Interaction.Toolkit;


public class OpenNextJewelry : MonoBehaviour
{

    [SerializeField]
    List<WayPoint> jewelries;
    [SerializeField]
    List<XRSimpleInteractable> jewleryInteractor;
    [SerializeField] IntVariable index;


    void Start()
    {
        jewelries = this.GetComponentsInChildren<WayPoint>().ToList();
        jewleryInteractor = this.GetComponentsInChildren<XRSimpleInteractable>().ToList();
        for (int i = 0; i < jewleryInteractor.Count - 1; i++)
        {
            jewleryInteractor[i + 1].GetComponent<Collider>().enabled = false;
            jewelries[i + 1].transform.GetChild(0).gameObject.SetActive(false);

        }
        AfterAgentArrived();
    }
    //on arrived at point 
    public void ActivateNextJewelry()
    {     
        Debug.Log("next jewelry index : "+index.Value);
        if (index.Value <= jewelries.Count)
        {
            jewleryInteractor[index.Value-1].GetComponent<Collider>().enabled = true;
            jewelries[index.Value-1].GetComponent<ScaleHandler>().enabled = true;
        }
    }
    //the index for upcoming jewelry
    //on move to next on click
    public void AfterAgentArrived()
    {
        //Debug.Log("AfterAgentArrived");
        int diff = 0;//for non stop points
        if (index.Value < jewelries.Count)
        {
            //Debug.Log("AfterAgentArrived index.Value < jewelries.Count "+"index.value "+index.Value);
            PlayLastJewelryEffect();
            if (!jewelries[index.Value].gameObject.GetComponent<WayPoint>().isStopPoint)
            {
                diff = 1;
                if (index.Value + diff >= jewelries.Count) return;
                if (!jewelries[index.Value+diff].gameObject.GetComponent<WayPoint>().isStopPoint) diff = 2;
            }
            if (index.Value < 1) return;
            //Debug.Log("last jewelry index : " + (index.Value-1));
            //PlayLastJewelryEffect();
            if (index.Value + diff >= jewelries.Count) return;
            jewelries[index.Value+diff].transform.GetChild(0).gameObject.SetActive(true);
            jewelries[index.Value + diff].gameObject.GetComponentInChildren<Light>().enabled = true;
        }
        else if (index.Value== jewelries.Count)
        {
            PlayLastJewelryEffect();
        }

    }

    void PlayLastJewelryEffect()
    {
        jewelries[index.Value - 1].gameObject.GetComponentInChildren<Light>().enabled = false;
        jewelries[index.Value - 1].gameObject.GetComponentInChildren<ParticleSystem>().Play();
        jewelries[index.Value - 1].transform.GetChild(0).gameObject.SetActive(false);
    }

    public void StopJewlMove()
    {
        List<ObjectMovement> jewelries = this.GetComponentsInChildren<ObjectMovement>().ToList();

        jewelries[index.Value-1].gameObject.GetComponentInChildren<ScaleHandler>().CloseInteractor();

        if (FindObjectOfType<MenuManger>().menu.level == 3)
        {
            List<ObjectMovement> _jewelries = this.GetComponentsInChildren<ObjectMovement>().ToList();
            _jewelries[index.Value-1].gameObject.GetComponent<ObjectMovement>().Stop();
            Debug.Log(index.Value - 1 + " " + _jewelries.Count);
        }
    }
}
