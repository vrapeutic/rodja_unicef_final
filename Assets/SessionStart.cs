using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SessionStart : MonoBehaviour
{
    [SerializeField]
    GameObject langPanel;
    [SerializeField]
    GameObject elementsPanel;

    void Awake()
    {
        if(FindObjectOfType<MenuManger>().menu.language!="")
        {
            langPanel.SetActive(false);
            elementsPanel.SetActive(true);
        }
        else
        {
            langPanel.SetActive(true);
            elementsPanel.SetActive(false);
        }

    }


}
