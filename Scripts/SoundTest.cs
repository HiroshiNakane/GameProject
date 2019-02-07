using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundTest : MonoBehaviour {

    // Resources内のSoundsフォルダに音楽ファイルが入ってないと機能しないので入れておく

    void Awake()
    {
        // サウンドのロード(各スクリプトでする必要あり)
        // SoundManager.LoadBGM/LoadSE("キー名", "ファイル名");

        SoundManager.LoadBGM("Stage1", "Stage1");
        SoundManager.LoadBGM("Stage2", "Stage2");
        SoundManager.LoadBGM("Stage3", "Stage3");
        SoundManager.LoadBGM("Title", "TitleBGM");
        SoundManager.LoadBGM("SceneSelect", "SceneSelect");

        SoundManager.LoadSE("Attack1", "Attack1");
        SoundManager.LoadSE("Attack2", "Attack2");
        SoundManager.LoadSE("bomb", "bomb1");
        SoundManager.LoadSE("Capsule", "Capsule");
        SoundManager.LoadSE("Dash1", "Dash1");
        SoundManager.LoadSE("Cash2", "Dash2");
        SoundManager.LoadSE("Decision", "Decision");
        SoundManager.LoadSE("Landing", "Landing");
        SoundManager.LoadSE("Select", "Select");

        SoundManager.LoadSE("Zombie-voice2", "Zombie-voice2");
        SoundManager.LoadSE("Zombie-voice3", "Zombie-voice3");
        SoundManager.LoadSE("Zombie-breath", "Zombie-breath1");
        SoundManager.LoadSE("ZombieDeath", "ZombieDeth");
        SoundManager.LoadSE("GeroVoice", "GeroVoice");
        SoundManager.LoadSE("CrowVoice1", "CrowVoice1");
        SoundManager.LoadSE("CrowVoice2", "CrowVoice2");
    }

    void Start ()
    {
        // BGM再生
        SoundManager.PlayBGM("Stage1");
	}
	
	void Update ()
    {
        if (Input.GetKeyDown("x"))
        {
            // BGM停止
            SoundManager.StopBGM();
        }

        if (Input.GetKeyDown("z"))
        {
            // SE再生
            SoundManager.PlaySE("Capsule");
        }
	}
}
