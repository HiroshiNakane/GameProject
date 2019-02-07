using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour {

    // SEチャンネル数
    const int SE_CHANNEL = 4;

    // サウンド種別
    enum SoundType
    {
        BGM,
        SE,
    }

    // シングルトン
    static SoundManager singleton = null;

    // インスタンス取得
    public static SoundManager GetInstance()
    {
        return singleton ?? (singleton = new SoundManager());
    }

    // サウンド再生のためのゲームオブジェクト
    GameObject soundObject = null;

    // サウンドリソース
    AudioSource sourceBGM = null; // BGM
    AudioSource sourceSE_Default = null; // SE(デフォルト)
    AudioSource[] sourceSE_Array; // SE(チャンネル)

    // BGMにアクセスするためのテーブル
    Dictionary<string, Data> poolBGM = new Dictionary<string, Data>();

    // SEにアクセスするためのテーブル
    Dictionary<string, Data> poolSE = new Dictionary<string, Data>();

    // 保持するデータ
    class Data
    {
        // アクセス用のキー
        public string Key;

        // リソース名
        public string ResName;

        // AudioClip
        public AudioClip Clip;

        // コンストラクタ
        public Data(string key, string res)
        {
            Key = key;
            ResName = "Sounds/" + res;

            // AudioClipの取得
            Clip = Resources.Load(ResName) as AudioClip;
        }
    }

    // コンストラクタ
    public SoundManager()
    {
        // チャンネル確保
        sourceSE_Array = new AudioSource[SE_CHANNEL];
    }

    // AudioSourceを取得
    AudioSource GetAudioSource(SoundType type, int channel = -1)
    {
        if(soundObject == null)
        {
            // GameObjectがなければ作る
            soundObject = new GameObject("Sound");

            // 破棄しないようにする
            GameObject.DontDestroyOnLoad(soundObject);

            // AudioSourceを作成
            sourceBGM = soundObject.AddComponent<AudioSource>();
            sourceSE_Default = soundObject.AddComponent<AudioSource>();
            for(int i = 0; i < SE_CHANNEL; i++)
            {
                sourceSE_Array[i] = soundObject.AddComponent<AudioSource>();
            }
        }

        if(type == SoundType.BGM)
        {   
            return sourceBGM; // BGM
        } else if(0 <= channel && channel < SE_CHANNEL) // SE
        {
            return sourceSE_Array[channel]; // チャンネル指定
        } else
        {
            return sourceSE_Default; // デフォルト 
        }
    }

    // サウンドのロード
    public static void LoadBGM(string Key, string resName)
    {
        GetInstance().loadBGM(Key, resName);
    }

    public static void LoadSE(string Key, string resName)
    {
        GetInstance().loadSE(Key, resName);
    }

    void loadBGM(string Key, string resName)
    {
        if (poolBGM.ContainsKey(Key))
        {
            // すでに登録済みなので一旦消す
            poolBGM.Remove(Key);
        }
        poolBGM.Add(Key, new Data(Key, resName));
    }

    void loadSE(string Key, string resName)
    {
        if (poolSE.ContainsKey(Key))
        {
            // すでに登録済みなので一旦消す
            poolSE.Remove(Key);
        }
        poolSE.Add(Key, new Data(Key, resName));
    }

    // BGM再生
    public static bool PlayBGM(string Key)
    {
        return GetInstance()._PlayBGM(Key);
    }
    bool _PlayBGM(string Key)
    {
        if(poolBGM.ContainsKey(Key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // 一旦止める
        StopBGM();

        // リソースの取得
        var data = poolBGM[Key];

        // 再生
        var source = GetAudioSource(SoundType.BGM);
        source.loop = true;
        source.clip = data.Clip;
        source.Play();

        return true;
    }

    // BGM停止
    public static bool StopBGM()
    {
        return GetInstance()._StopBGM();
    }
    bool _StopBGM()
    {
        GetAudioSource(SoundType.BGM).Stop();

        return true;
    }

    // SE再生
    public static bool PlaySE(string Key, int channel = -1)
    {
        return GetInstance()._PlaySE(Key, channel);
    }
    bool _PlaySE(string Key, int channel = -1)
    {
        if(poolSE.ContainsKey(Key) == false)
        {
            // 対応するキーがない
            return false;
        }

        // リソースの取得
        var data = poolSE[Key];

        if(0 <= channel && channel < SE_CHANNEL)
        {
            // チャンネル指定
            var source = GetAudioSource(SoundType.SE, channel);
            source.clip = data.Clip;
            source.Play();
        }
        else
        {
            // デフォルトで再生
            var source = GetAudioSource(SoundType.SE);
            source.PlayOneShot(data.Clip);
        }
        return true;
    }

}
