using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPositionHandling : MonoBehaviour
{
    GameObject agent;
    void Awake()
    {
        agent = GameObject.FindGameObjectWithTag("agent");
    }

    private void Start()
    {
        agent.transform.position = this.transform.position;
    }
}
