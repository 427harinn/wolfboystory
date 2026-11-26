using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartbuttonScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    public void onClicked_startbutton()
    {
        fadeanimation();
        Invoke("loadscene", 2);
    }

    public void fadeanimation()
    {
        Instantiate((GameObject)Resources.Load("Prefab/fadePanel1"), new Vector3(0.0f, 0.0f, 0.0f), Quaternion.identity, GameObject.Find("backImage").transform);
    }

    public void loadscene()
    {
        SceneManager.LoadScene("OtogeScene");
    }
}
