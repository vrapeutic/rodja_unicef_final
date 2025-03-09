using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LimitedInterapution : MonoBehaviour
{
    float hitsCounter=0;

    private void Start()
    {
        StartCoroutine(PlayerTracker());
    }

    public IEnumerator PlayerTracker()
    {
        while (true)
        {
            if (!this.GetComponentInChildren<Renderer>().IsVisibleFrom(Camera.main))
            {
                hitsCounter++;

                if (hitsCounter >= 3)
                { 
                    hitsCounter = 0;
                }
            }
            yield return new WaitForSeconds(3);

        }
    }
}
