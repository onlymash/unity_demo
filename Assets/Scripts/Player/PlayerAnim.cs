using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnim : MonoBehaviour
{
    public Animator[] animators; //0 up 1 down 
    public void PlayIdleAnim()
    {
        foreach(var a in animators)
        {
            a.SetBool("Walk", false);
        }
        animators[0].SetBool("Shoot", false);
        animators[0].SetBool("ShootUp", false);
    }
    public void PlayWalkAnim()
    {
        foreach (var a in animators)
        {
            a.SetBool("Walk", true);
        }
    }
    public void PlayJumpAnim()
    {
        foreach (var a in animators)
        {
            a.SetTrigger("Jump");
        }
    }
    public void PlayAttackAnim()
    {
        animators[0].SetTrigger("Attack");
    }
    public void PlayThrowAnim()
    {
        animators[0].SetTrigger("Throw");
    }
    public void PlayShootAnim()
    {
        animators[0].SetBool("Shoot", true);
    }
    public void PlayShootupAnim()
    {
        animators[0].SetBool("ShootUp", true);
    }
    public void PlayDieAnim()
    {
        animators[0].gameObject.SetActive(false);   //上半身隐藏
        animators[1].gameObject.SetActive(false);   //下半身隐藏
        animators[1].SetTrigger("Die");
        //销毁主角 todo
    }
    public void PlayResumeAnim()        //复活动画
    {
        animators[0].gameObject.SetActive(true); 
        animators[1].gameObject.SetActive(true);
        animators[1].SetTrigger("Idle");

    }
}
