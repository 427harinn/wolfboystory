using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectroopScript : MonoBehaviour
{
    Vector3 initiatepos;
    Vector3 finishpos;

    [SerializeField] GameObject fieldobj1;
    [SerializeField] GameObject fieldobj2;

    [SerializeField] GameObject kare_start;

    [SerializeField]TimelinefinishScript timeline;

    public float speed;

    int count;
    // Start is called before the first frame update
    void Start()
    {
        initiatepos = fieldobj2.transform.position;
        finishpos = fieldobj1.transform.position;
        speed = -0.1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if(timeline.timelinefinishflag)
        {
            if(count < GManager.instance.lifeimage.Length)
            {
                for (int i = 0; i < GManager.instance.lifeimage.Length; i++)
                {
                    count++;
                    if (!GManager.instance.lifeimage[i].activeSelf)
                    {
                        GManager.instance.lifeimage[i].SetActive(true);
                    }

                }
            }

            
            Debug.Log(speed);
            if(GameObject.Find("tree_kare_start"))
            {
                GameObject.Find("tree_kare_start").transform.Translate(-0.05f, 0f, 0f);
            }
            
            fieldobj1.transform.Translate(speed, 0f, 0f);
            fieldobj2.transform.Translate(speed, 0f, 0f);
            GameObject tmp;
            if (fieldobj2.transform.position.x < finishpos.x - 2)
            {
                if(kare_start)
                {
                    Destroy(kare_start);
                }
                fieldobj1.transform.position = initiatepos;
                tmp = fieldobj1;
                fieldobj1 = fieldobj2;
                fieldobj2 = tmp;
            }
        }

    }
}
