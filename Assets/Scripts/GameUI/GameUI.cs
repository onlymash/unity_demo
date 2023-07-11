using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class GameUI : MonoBehaviour
{
    public Text health;

    public GameObject Start;
    public GameObject win;

    public void UpdateHealth(int num)
    {
        health.text = num.ToString();
    }

    public void ShowStart()
    {
        Start.SetActive(true);
        StartCoroutine(Hide());
    }

    public IEnumerator Hide()
    {
        SoundManege.Instance.PlayerMusicName("Mission 1 Start");
        yield return new WaitForSeconds(2f);
        Start.SetActive(false);
    }

    public void ShowWin()
    {
        win.SetActive(true);
        if (this.gameObject.activeInHierarchy)
        {
            StartCoroutine(Win());
        }
        else
        {
            Debug.Log("ShowWin()");
            return;
        }
    }

    public IEnumerator Win()
    {
        SoundManege.Instance.PlayerMusicName("Mission Complete");
        yield return new WaitForSeconds(3.5f);
        //加载界面 参数是场景编号
        SceneManager.LoadScene(0);
    }
}
