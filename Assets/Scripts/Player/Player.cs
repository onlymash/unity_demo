using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public enum Dir
{
    Up,
    Forward,
}

public class Player : MonoBehaviour
{
    private float speed = 300f;
    //刚体
    private Rigidbody2D rd;
    public PlayerAnim playerAnim;

    //默认在地面上
    private bool isOnGround = true;

    //限制跳跃数量
    private int jumpNum = 2;

    public Transform[] point; //0 向前发射    1向上发射

    public Transform curPoint; //当前子弹生成点

    //玩家最大血量
    public int maxHealth = 3;
    public int curHealth;
    
    //false未死亡
    public bool isDead = false;

    //false 表示玩家位处于重生状态
    public bool isResume = false;
    
    //设置攻击间隔，false表示没有间隔
    public bool wait = false;

    private GameUI gameUI;

    //间隔攻击的计时器
    private float timer = 0;

    //定义近战攻击类
    private PlayerAttack playerAttack;
    // Start is called before the first frame update
    void Start()
    {
        rd = GetComponent<Rigidbody2D>();
        playerAnim = GetComponent<PlayerAnim>();
        playerAttack = GetComponent<PlayerAttack>();
        curHealth = maxHealth;
        gameUI = GameObject.Find("Canvas").GetComponent<GameUI>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isResume == true || isDead == true)
        {
            return;
        }
        PlayerMove();
        PlayerJump();

        if (Input.GetKeyDown(KeyCode.L) && wait == false)
        {
            wait = true;
            Fire(Dir.Forward);
            playerAnim.PlayShootAnim();
        }
        if (Input.GetKeyDown(KeyCode.W) && wait == false)
        {
            wait = true;
            Fire(Dir.Up);
            playerAnim.PlayShootupAnim();
        }
        if (Input.GetKeyDown(KeyCode.I) && wait == false)
        {
            wait = true;
            Throw();
        }
        if (Input.GetKeyDown(KeyCode.J) && wait == false)
        {
            playerAttack.HurtEnemys();
            playerAnim.PlayAttackAnim();
            SoundManege.Instance.PlayerMusicName("tieguo");
        }
        if (wait)
        {
            timer += Time.deltaTime;
            if (timer >= 1)
            {
                wait = false;
                timer = 0;
            }
        }

    }
    public void PlayerMove()
    {
        if (isOnGround==false)
        {
            return;  //人物不再地上，则不可以进行左右移动
        }

        float h = Input.GetAxis("Horizontal");  //获取A D 键操作
        if (h > 0)
        {
            transform.rotation = Quaternion.Euler(0, 0, 0);
            rd.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rd.velocity.y);
            playerAnim.PlayWalkAnim();
        }
        else if(h<0)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            rd.velocity = new Vector2(h * speed * Time.fixedDeltaTime, rd.velocity.y);
            playerAnim.PlayWalkAnim();
        }
        else
        {
            playerAnim.PlayIdleAnim();
        }
        
    }

    public void PlayerJump()
    {
        if (Input.GetKeyDown(KeyCode.K) && jumpNum > 0)
        {
            isOnGround = false;  //不再地上
            jumpNum--;
            rd.AddForce(Vector2.up*250F); //给物体添加向上的力
            playerAnim.PlayJumpAnim();
        }
    }

    //获取系统碰撞体的组件
    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.transform.tag == "Ground") //接触碰撞体，状态重置
        {
            isOnGround = true;
            jumpNum = 2;
        }
    }

    public void Fire(Dir dir)
    {
        //播放相应的音乐
        SoundManege.Instance.PlayerMusicName("shoot");


        GameObject temp = Resources.Load<GameObject>("Prefabs/Bullet");  //加载Resources的Prefabs/Bullet下子弹预制体

        switch (dir)
        {
            case Dir.Forward:
                curPoint = point[0];    //0 向前发射
                break;
            case Dir.Up:
                curPoint = point[1];    //1向上发射
                break;
        }
        //生成相应的物体
        GameObject GO = Instantiate(temp, curPoint.transform.position, Quaternion.identity);

        //初始化子弹方向
        GO.GetComponent<Bullet>().InitDir(dir);
    }

    public void Throw()
    {
        GameObject temp = Resources.Load<GameObject>("Prefabs/Grenade");
        GameObject GO = Instantiate(temp, point[2].transform.position, Quaternion.identity);
    }
    public void Hurt()
    {
        curHealth--;
        if (gameUI != null)
        {
            gameUI.UpdateHealth(curHealth);
        }
        if (curHealth <= 0)
        {
            isDead = true;
            playerAnim.PlayDieAnim();
            SoundManege.Instance.PlayerMusicName("soliderDie");
            //
            SceneManager.LoadScene(1);
        }
        else
        {
            isResume = true;
            StartCoroutine(Resume());           //开启协程
        }
    }

    public IEnumerator Resume()
    {
        yield return new WaitForSeconds(1);  //等待一秒钟
        playerAnim.PlayResumeAnim();

        transform.position = new Vector3(transform.position.x - 3f, transform.position.y + 5f);

        playerAnim.PlayIdleAnim();

        StartCoroutine(ResetState());


    }

    public IEnumerator ResetState()
    {
        yield return new WaitForSeconds(1f);

        isResume = false;
    }

    public bool GetCurState()
    {
        if (isDead == true)
        {
            return false;
        }
        else if (isResume == true)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

}
