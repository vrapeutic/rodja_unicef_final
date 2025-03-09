using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChooseRoad : MonoBehaviour
{
    [SerializeField] bool test;
    [SerializeField] GameObject[] roads;
    // Start is called before the first frame update
    void Awake()
    {
        if (test) return;
        int rand = Random.Range(0, 3);
        for (int i = 0; i < 3; i++)
        {
            if (i == rand) roads[i].SetActive(true);
            else roads[i].SetActive(false);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

}
