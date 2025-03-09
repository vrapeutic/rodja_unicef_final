﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//three types of attention determine which distractor will appear sustained(no distractor),selective(distractor with no action needed)
//and adaptive (distractor with action needed)
public class DistractorManager : MonoBehaviour
{
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable noOfDistractors;
    [SerializeField] BoolValue canPlay;
    //selective attention 
    [SerializeField] GameEvent OnSelectiveTask1;
    [SerializeField] GameEvent OnSelectiveTask2;
    [SerializeField] GameEvent OnSelectiveTask3;
    //adaptive attention
    [SerializeField] GameEvent OnAdaptiveTask1;
    [SerializeField] GameEvent OnAdaptiveTask2;
    [SerializeField] GameEvent OnAdaptiveTask3;
    bool JewelsCollected;
    int lastRand = 0 ;

    // Start is called before the first frame update
    void Start()
    {
        JewelsCollected = false;
        if (typeOfAttention.Value == "selective") SelectiveAttention();
        else if (typeOfAttention.Value == "adaptive") AdaptiveAttention();
    }
  

    async void SelectiveAttention()
    {
        await new WaitForSeconds(20);
        while (canPlay.Value)
        {
            int rand = RandomNember();
            if (rand == 1) OnSelectiveTask1.Raise();
            else if (rand == 2) OnSelectiveTask2.Raise();
            else if (rand == 3) OnSelectiveTask3.Raise();
            await new WaitForSeconds(30);
        }
    }

    public async void AdaptiveAttention()
    {
        int rand = RandomNember();
        //Debug.Log("AdaptiveAttention"+rand);
        await new WaitForSeconds(20);
        if (canPlay.Value)
        {
            Debug.Log("canPlay.Value = "+canPlay.Value);
            if (rand == 1) OnAdaptiveTask1.Raise();
            else if (rand == 2) OnAdaptiveTask2.Raise();
            else if (rand == 3) OnAdaptiveTask3.Raise();
        }
    }

    int RandomNember()
    {
        if (noOfDistractors.Value == 1) return 1;
        int maxRange = noOfDistractors.Value + 1;
        int rand = Random.Range(1, maxRange);
        while (rand == lastRand)
        {
            rand = Random.Range(1, maxRange);
        }
        lastRand = rand;
        return rand;
    }

    public void EnableOfferingJewelsDistractor()
    {
        JewelsCollected = true;
    }

}
