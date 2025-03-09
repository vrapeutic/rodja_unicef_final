using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnClassifiedJewelsLogic : MonoBehaviour
{
    [SerializeField] GameEvent onClassifyCollectedJewelsCompleted;
    [SerializeField] IntVariable redJewelsNo;
    [SerializeField] IntVariable blueJewelsNo;
    [SerializeField] IntVariable goldJewelsNo;
    [SerializeField] GameObject[] redJewels;
    [SerializeField] GameObject[] blueJewels;
    [SerializeField] GameObject[] goldJewels;
    [SerializeField] StringVariable lastUnClassifiedJewelTag;
    List<GameObject> enabledJewels= new List<GameObject>();
    int lastJewelIndex;
    // Start is called before the first frame update
    void OnEnable()
    {
        lastJewelIndex = 0;
        foreach (var item in enabledJewels)
        {
            item.transform.GetChild(0).gameObject.SetActive(false);
        }
        enabledJewels.Clear();
        ShowUnClassifiedJewels();
    }

    void ShowUnClassifiedJewels()
    {
        for (int i = 0; i < redJewels.Length; i++)
        {
            if (i < redJewelsNo.Value) { redJewels[i].SetActive(true); enabledJewels.Add(redJewels[i]); }
            else redJewels[i].SetActive(false);
            if (i < blueJewelsNo.Value) { blueJewels[i].SetActive(true); enabledJewels.Add(blueJewels[i]); }
            else blueJewels[i].SetActive(false);
            if (i < goldJewelsNo.Value) { goldJewels[i].SetActive(true); enabledJewels.Add(goldJewels[i]); }
            else goldJewels[i].SetActive(false);
        }
        HighlightNextJewely();
    }

    public void HighlightNextJewely()
    {
        if (lastJewelIndex > 0) { enabledJewels[lastJewelIndex - 1].transform.GetChild(0).gameObject.SetActive(false);
            enabledJewels[lastJewelIndex - 1].SetActive(false);
        }
        if (lastJewelIndex >= enabledJewels.Count)
        {
            onClassifyCollectedJewelsCompleted.Raise();
            return;
        }
        enabledJewels[lastJewelIndex].transform.GetChild(0).gameObject.SetActive(true);
        lastUnClassifiedJewelTag.Value = enabledJewels[lastJewelIndex].tag;


        lastJewelIndex++;
    }

}
