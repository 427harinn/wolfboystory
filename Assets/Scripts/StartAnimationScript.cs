using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.SceneManagement;

public class StartAnimationScript : MonoBehaviour
{
    public bool musicstartflag;
    private PlayableDirector playableDirector;

    [SerializeField] GameObject girl;
    [SerializeField] GameObject tree;
    [SerializeField] GameObject startanimation;
    // Start is called before the first frame update
    void Start()
    {
        playableDirector = startanimation.GetComponent<PlayableDirector>();
    }

    // Update is called once per frame
    void Update()
    {
        if(musicstartflag)
        {
            startanimation.SetActive(true);
            if (playableDirector.state != PlayState.Playing)
            {
                //girl.SetActive(false);
                Destroy(tree);
            }
        }


    }

}
