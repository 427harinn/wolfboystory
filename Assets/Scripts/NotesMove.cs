using UnityEngine;
using System;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NotesMove : MonoBehaviour
{
    private float notesSpeed = 0.1f;

    [SerializeField]
    private Vector2 startPos;//ノーツの開始位置
    [SerializeField]
    private Vector2 judgePos;//判定したい場所

    public static float moveSpan = 0.01f;//回すスパン 

    private float notesTime;

    StartAnimationScript startanimation;

    bool freezeflag;

    Animator boy_anim;
    Animator girl_anim;

    AudioSource source;
    [SerializeField] AudioClip jump;
    [SerializeField] AudioClip sliding;
    [SerializeField] AudioClip damage;

    bool seflag;
    bool pass;
    bool clear;

    Image boy_image;

    // 点滅周期[s]
    private float _cycle = 0.1f;
    private double _time;

    GameObject girl;
    GameObject boy;

    //背景速度
    float velocity = -0.1f;

    void Start()
    {
        boy = GameObject.Find("Boy");
        girl = GameObject.Find("Girl");
        if (this.gameObject.tag == "Notes")
        {
            Animator anim = gameObject.transform.GetChild(0).gameObject.GetComponent<Animator>();
            anim.SetBool("treedownflag", true);
        }

        boy_anim = boy.GetComponent<Animator>();
        girl_anim = girl.GetComponent<Animator>();

        notesTime = (startPos.x - judgePos.x) / (notesSpeed * 100);

        InvokeRepeating("NotesMoving", 0, moveSpan);
        startanimation = GameObject.Find("Canvas").GetComponent<StartAnimationScript>();


        source = this.gameObject.GetComponent<AudioSource>();

        boy_image = boy.GetComponent<Image>();
    }

    void NotesMoving()
    {
        if(this.gameObject.tag == "AudioPlay")
        {
            //Debug.Log(this.gameObject.transform.position.x);
        }

        if(!freezeflag)
        {
            transform.position += new Vector3(-notesSpeed, 0f, 0f);
        }

        
        notesTime -= moveSpan;
        NotesJudge();
    }

    void NotesJudge()
    {

        if (Math.Abs(notesTime) < 0.15f)
        {
            pass = true;
            if (this.gameObject.tag == "Notes")
            {
                if (Input.GetKeyDown(KeyCode.UpArrow))
                {
                    source.PlayOneShot(jump);
                    boy_anim.SetTrigger("jumptrigger");
                    seflag = true;
                    clear = true;
                    //Destroy(this.gameObject);
                }
            }
            if (this.gameObject.tag == "SpecialNotes")
            {
                if (Input.GetKeyDown(KeyCode.DownArrow))
                {
                    source.PlayOneShot(sliding,0.8f);
                    boy_anim.SetTrigger("slidingtrigger");
                    seflag = true;
                    clear = true;
                    //Destroy(this.gameObject);
                }
            }
        } else
        {
            if(pass && this.gameObject.tag != "AudioPlay" && this.gameObject.tag != "finish")
            {
                if(!seflag)
                {
                    source.PlayOneShot(damage);
                    for(int i = 0; i < GManager.instance.lifeimage.Length;i++) 
                    {
                        if(GManager.instance.lifeimage[i].activeSelf)
                        {
                            GManager.instance.lifecount++;
                            GManager.instance.lifeimage[i].SetActive(false);
                            break;
                        }
                    }
                    if(GManager.instance.lifecount == 3)
                    {
                        GManager.instance.fadeanimation();
                        Invoke("bad_loadscene", 2);
                    }
                    seflag = true;
                }
                if(_time < 1 && !clear)
                {
                    // 内部時刻を経過させる
                    _time += Time.deltaTime;

                    // 周期cycleで繰り返す値の取得
                    // 0～cycleの範囲の値が得られる
                    var repeatValue = Mathf.Repeat((float)_time, _cycle);

                    // 内部時刻timeにおける明滅状態を反映
                    boy_image.enabled = repeatValue >= _cycle * 0.5f;
                } else if(_time > 1)
                {
                    clear = true;
                    boy_image.enabled = true;
                }


               
            }
        }

        if (this.gameObject.tag == "AudioPlay")
        {
            if (notesTime <= 0)//判定位置に来たら
            {
                freezeflag = true;
                startanimation.musicstartflag = true;
                NotesGenerator.isAudioPlay = true;
            }

        }
        
        if (this.gameObject.tag == "finish")
        {
            if (notesTime <= 0)//判定位置に来たら
            {
                //ここで少女が入ってくる演出を入れる
                //GameObject.Find("Field").GetComponent<ObjectroopScript>().enabled = false;
                
                if(velocity < 0)
                {
                    velocity += 0.0002f;
                }
                GameObject.Find("Field").GetComponent<ObjectroopScript>().speed = velocity;
                

                if(GameObject.Find("lineImage"))
                {
                    GameObject.Find("lineImage").SetActive(false);
                }
                

                boy_anim.SetBool("stopflag", true);
                girl_anim.SetBool("finishflag", true);
            }

        }
    }


    public void bad_loadscene()
    {
        SceneManager.LoadScene("GameoverScene");
    }


}
