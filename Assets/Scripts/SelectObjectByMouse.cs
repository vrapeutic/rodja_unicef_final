using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectObjectByMouse : MonoBehaviour
{
    private void OnMouseDown()
    {
        Debug.Log("Mouse clicked at Box" + gameObject.name);
        GetComponent<JewelryBox>().OnBoxSelected();
    }

}
