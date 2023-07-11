using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using UnityEngine.SceneManagement;

public class LoadPlay : MonoBehaviour
{
    public Button btn1;
    public Button btn2;
    public GameObject bg2;

    // Start is called before the first frame update
    void Start()
    {
        SoundManege.Instance.PlayerMusicName("background");
        btn1.onClick.AddListener(() =>
        {
            BgMove();
        });
        btn2.onClick.AddListener(() =>
        {
            LoadScene();
        });
    }

    public void BgMove()
    {
        if (bg2 != null)
        {
            bg2.transform.DOLocalMoveY(-50, 2);
        }
    }

    public void LoadScene()
    {
        SoundManege.Instance.StopMusic();
        SceneManager.LoadScene(1);  //加载到第二个场景
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
