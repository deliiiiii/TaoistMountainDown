using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioPlayer : MonoBehaviour
{
    public static AudioPlayer instance;
    public AudioSource music;
    public AudioClip Mshot;//这里我要给主角添加跳跃的音效

    private void Awake()
    {
        instance = this;
        //给对象添加一个AudioSource组件
        music = GetComponent<AudioSource>();
        //设置不一开始就播放音效
        music.playOnAwake = false;
        //加载音效文件，我把跳跃的音频文件命名为jump
        //jump = Resources.Load<AudioClip>("music/jump");
    }
    void Update()
    {
		//if (Input.GetKeyDown(KeyCode.UpArrow))//如果输入↑
  //      {
            
  //      }

    }
}
