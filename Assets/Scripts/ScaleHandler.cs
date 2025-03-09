using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ScaleHandler : MonoBehaviour
{
    int index = 0;
    Vector3 scaleChange;
    bool runScaleUp = true;
    // Start is called before the first frame update
    void Start()
    {
        scaleChange = new Vector3(.5f, .5f, .5f);

    }
    private void Update()
    {
        if (runScaleUp && this.gameObject.transform.localScale.x <= 2f)
        {
            this.gameObject.transform.localScale += scaleChange * Time.deltaTime;
        }
    }
    public void ScaleJewelry()
    {
        runScaleUp = true;
    }
    public void CloseInteractor()
    {
        this.gameObject.transform.GetChild(0).gameObject.SetActive(false);

    }

}
