using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightScript : MonoBehaviour
{
    Light lt;
    [SerializeField] GameObject postprocessing;
    // Start is called before the first frame update
    void Start()
    {
        lt = gameObject.GetComponent<Light>();

    }

    // Update is called once per frame
    void Update()
    {
        if(GManager.instance.lifecount == 2)
        {
            lt.color = Color.red;
            postprocessing.SetActive(true);
        }
        
    }
}
