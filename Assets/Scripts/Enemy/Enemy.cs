using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    //最大生命值
    private readonly int maxHealth = 2;
    private int curHealth;

    //获取敌人动画状态机
    private readonly Animator animator;

    //通过enemyAnim脚本控制
    private EnemyAnim enemyAnim;

    //与玩家距离大于6时，播放站立动画
    public float idleDis = 6f;

    //与玩家距离在【2，6】之间播放行走动画
    public float walkdis = 2f;

    public bool canAttack = true;
    //false 表示未死亡
    public bool isDie = false;

    //设置敌人攻击时间
    private float timer = 0;

    //设置攻击间隔 1s
    private readonly float attackTime = 1f;


    // Start is called before the first frame update
    void Start()
    {
        enemyAnim = GetComponent<EnemyAnim>();
        curHealth = maxHealth;
    }

    // Update is called once per frame
    void Update()
    {
        if (Patrol())
        {
            enemyAnim.PlayWalkAnim();
        }
        else if (Attack() && canAttack == true)
        {
            enemyAnim.PlayKillAnim();
        }
        else
        {
            enemyAnim.PlayIdleAnim();
        }

    }

    public void Hurt()
    {
        canAttack = false;
        curHealth--;
        if (curHealth <= 0)
        {
            isDie = true;
            SoundManege.Instance.PlayerMusicName("enemyDie");
            enemyAnim.PlayDieAnim();
        }
    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }

    //
    public bool Patrol()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        if (GO != null)
        {
            if (GO.GetComponent<Player>().GetCurState() == false)
            {
                return false;
            }
            Transform player = GO.transform;
            Vector2 r = new Vector2(0, 0);
            if (player.transform.position.x - transform.position.x > 0)
            {
                r.y = 180;
            }
            //修改敌人角度
            transform.rotation = Quaternion.Euler(r);

            if (Mathf.Abs(player.position.x - transform.position.x) > idleDis)
            {
                return false;   //敌人不再巡逻
            }
            else if(Mathf.Abs(player.position.x - transform.position.x) < walkdis)
            {
                return false;   //敌人开始攻击
            }
            else if (isDie==false)
            {
                //敌人没有死亡，向玩家移动
                transform.position = Vector2.MoveTowards(transform.position, player.position, Time.deltaTime);
            }
            return true;
        }
        else
        {
            return false;
        }
    }
    
    public bool Attack()
    {
        timer += Time.deltaTime;
        if (timer >= attackTime)
        {
            GameObject GO = GameObject.FindGameObjectWithTag("Player");
            if (GO != null)
            {
                if (canAttack == false || GO.GetComponent<Player>().GetCurState()==false)
                {
                    return false;
                }
                else if ((this.transform.position - GO.transform.position).magnitude <= 1.5f)
                {
                    //TODO
                    HurtPlayer();
                    SoundManege.Instance.PlayerMusicName("knife");

                    timer = 0;
                    return true;
                }
            }
        }


        return false;
    }
    public void HurtPlayer()
    {
        GameObject GO = GameObject.FindGameObjectWithTag("Player");
        GO.GetComponent<Player>().Hurt();
    }
}
