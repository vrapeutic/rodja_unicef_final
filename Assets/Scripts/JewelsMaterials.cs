using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JewelsMaterials : MonoBehaviour
{
    [SerializeField]
    List<Material> materialsJewel;
    Material currentMat;

    private void Awake()
    {
        currentMat = materialsJewel[1];
        foreach(Material mat in materialsJewel)
        {
            if (mat.name == FindObjectOfType<MenuManger>().menu.Collectable_name)
                currentMat = mat;
        }
        GetComponent<MeshRenderer>().material = currentMat;

    }
}
