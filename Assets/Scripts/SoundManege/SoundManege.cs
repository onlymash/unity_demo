using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManege : MonoBehaviour
{
    private static SoundManege instance;

    public static SoundManege Instance;

    private AudioSource audioSource;    //用于播放声音


    private void Awake()
    {
        Instance = this;
    }
    // Start is called before the first frame update
    void Start()
    {
        audioSource = gameObject.AddComponent<AudioSource>();
        //不会开始时自动播放
        audioSource.playOnAwake = false;
    }

    private void PlayerAudio(AudioClip ac)
    {
        AudioSource.PlayClipAtPoint(ac, Camera.main.transform.position);            //在主摄像机的位置播放

    }

    public void PlayerMusicName(string name)
    {
        //播放什么音乐
        string path = "Sounds/" + name;
        AudioClip clip = Resources.Load<AudioClip>(path);
        PlayerAudio(clip);
    }

    public void StopMusic()
    {
        audioSource.Stop();
    }
}
