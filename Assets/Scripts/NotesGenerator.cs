using UnityEngine;
using System;

public class NotesGenerator : MonoBehaviour
{
    [SerializeField]
    private GameObject notesPre;
    [SerializeField]
    private GameObject audioplayPre;
    [SerializeField]
    private GameObject specialnotesPre;
    [SerializeField]
    private GameObject finishPre;

    private float moveSpan = 0.01f;
    private float nowTime;// 音楽の再生されている時間
    private int beatNum;// 今の拍数
    private int beatCount;// json配列用(拍数)のカウント
    private bool isBeat;// ビートを打っているか(生成のタイミング)

    [SerializeField]
    private AudioSource gameAudio;
    public static bool isAudioPlay=false;



     
    public class InputJson
    {
        public Notes[] notes;
        public int BPM;
    }

    [Serializable]
    public class Notes
    {
        public int num;
        public int block;
        public int LPB;
    }

    private int[] scoreNum;//ノーツの番号を順に入れる
    private int[] scoreBlock;//ノーツの種類を順に入れる
    private int BPM;
    private int LPB;

    void Awake()
    {
        MusicReading();
        InvokeRepeating("NotesIns", 0f, moveSpan);
        
    }



    private void Update()
    {
        AudioPlay();
    }
    /// <summary>
    /// 譜面の読み込み
    /// </summary>
    void MusicReading()
    {
        string inputString = Resources.Load<TextAsset>("maou_short_04_tsuki_to_okami").ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);

        scoreNum = new int[inputJson.notes.Length];
        scoreBlock = new int[inputJson.notes.Length];
        BPM = inputJson.BPM;
        LPB = inputJson.notes[0].LPB;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            //ノーツがある場所を入れる
            scoreNum[i] = inputJson.notes[i].num;
            //ノーツの種類を入れる(scoreBlock[i]はscoreNum[i]の種類)
            scoreBlock[i] = inputJson.notes[i].block;
        }

    }

    /// <summary>
    /// 譜面上の時間とゲームの時間のカウントと制御
    /// </summary>
    void GetScoreTime()
    {
        //今の音楽の時間の取得
        nowTime += moveSpan; //(1)

        //ノーツが無くなったら処理終了
        if (beatCount > scoreNum.Length) return;

        //楽譜上でどこかの取得
        beatNum = (int)(nowTime * BPM / 60 * LPB); //(2)
    }

    /// <summary>
    /// ノーツを生成する
    /// </summary>
    void NotesIns()
    {
        GetScoreTime();

        //json上でのカウントと楽譜上でのカウントの一致
        if (beatCount < scoreNum.Length)
        {
            isBeat = (scoreNum[beatCount] == beatNum); //(3)
        }

        //生成のタイミングなら
        if (isBeat)
        {
            //ノーツ0の生成
            if (scoreBlock[beatCount] == 0)
            {
                Instantiate(notesPre, GameObject.Find("Canvas").transform);
            }

            //ノーツ1の生成
            if (scoreBlock[beatCount] == 1)
            {
                Instantiate(specialnotesPre, GameObject.Find("Canvas").transform);
            }

            if (scoreBlock[beatCount] == 2)
            {
                Instantiate(audioplayPre, GameObject.Find("Canvas").transform);
            }

            if (scoreBlock[beatCount] == 3)
            {
                Instantiate(finishPre, GameObject.Find("Canvas").transform);
            }

            beatCount++; //(5)
            isBeat = false;

        }
    }

    void AudioPlay()
    {
        gameAudio.enabled = isAudioPlay;
    }

}
