using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    public void PlayIdleAnim()
    {
        anim.SetBool("walk",false);
    }
    public void PlayWalkAnim()
    {
        anim.SetBool("walk", true);
    }

    public void PlayKillAnim()
    {
        anim.SetTrigger("kill");
    }
    public void PlayDieAnim()
    {
        anim.SetTrigger("die");
    }
}
