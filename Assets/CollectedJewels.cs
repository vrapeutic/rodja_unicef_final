using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectedJewels : MonoBehaviour
{
    [SerializeField] StringVariable typeOfAttention;
    [SerializeField] IntVariable redJewelsNo;
    [SerializeField] IntVariable blueJewelsNo;
    [SerializeField] IntVariable goldJewelsNo;
    string lastJewelTag="";
    // Start is called before the first frame update
    void Awake()
    {
        if (typeOfAttention.Value != "adaptive") Destroy(this);
        redJewelsNo.Value = blueJewelsNo.Value = goldJewelsNo.Value = 0;

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("RedJewel")) lastJewelTag= "RedJewel";
        else if (other.gameObject.CompareTag("BlueJewel")) lastJewelTag = "BlueJewel";
        else if (other.gameObject.CompareTag("GoldJewel")) lastJewelTag = "GoldJewel";
    }

    public void CountingJeweleries()
    {
        if (lastJewelTag == "RedJewel") redJewelsNo.Value++;
        else if (lastJewelTag == "BlueJewel") blueJewelsNo.Value++ ;
        else if (lastJewelTag == "GoldJewel") goldJewelsNo.Value++;
    }
}
