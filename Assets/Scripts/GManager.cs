using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GManager : MonoBehaviour
{
    public static GManager instance = null;
    [SerializeField] public GameObject[]lifeimage;
    [SerializeField] public GameObject fadeobj;
    public int lifecount;

    public bool musicstop;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            /*
            DontDestroyOnLoad(this.gameObject);
            */
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    private void Update()
    {
        if(musicstop)
        {
            fadeanimation();
            Invoke("clear_loadscene",2);
            musicstop = false;
        }
    }

    public void fadeanimation()
    {
        fadeobj.SetActive(true);
    }

    void clear_loadscene()
    {
        SceneManager.LoadScene("GameclearScene");
    }
}