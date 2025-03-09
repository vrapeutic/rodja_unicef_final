using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelryBox : MonoBehaviour
{
    [SerializeField] StringVariable lastUnClassifiedJewelTag;
    [SerializeField] string jewelryTag;
    [SerializeField] GameEvent OnCorrectBoxSelected;
    GameObject [] jewels = new GameObject[5];
    int lastJewelIndex;

    private void Awake()
    {
        for (int i = 0; i < 5; i++)
        {
            jewels[i] =transform.GetChild(0).GetChild(i).gameObject;
        }
    }

    private void OnEnable()
    {
        lastJewelIndex = 0;
        foreach (var item in jewels)
        {
            item.SetActive(false);
        }
    }

    public void OnBoxSelected()
    {
        if (lastUnClassifiedJewelTag.Value == jewelryTag&& lastJewelIndex<5)
        {
            OnCorrectBoxSelected.Raise();
            jewels[lastJewelIndex].SetActive(true);
            lastJewelIndex++;
        }
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
