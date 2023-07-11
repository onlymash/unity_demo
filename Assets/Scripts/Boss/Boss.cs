using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    private int maxHealth = 6;
    private int curHealth;

    public bool isDie = false;
    public GameObject player;
    public float speed = 2f;

    public GameUI gameui;
    //private GameUI gameui;
    //飞机动画
    private Animator animator;

    public Transform[] tr;

    //计时
    private float timer = 0f;
    private float coolTime = 2f;

    //子弹预制体
    public GameObject Boom;

    public GameObject bossBullet;

    public GameObject bullet2;
    // Start is called before the first frame update
    void Start()
    {
        SoundManege.Instance.PlayerMusicName("boss战");
        curHealth = maxHealth;
        animator = GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player == null)
        {
            return;
        }
        if (player.GetComponent<Player>().GetCurState() == false)
        {
            return;
        }
        if (!Move())  //boss没有移动
        {
            Attack();
        }
    }
    public bool Move()
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (player != null)
        {
            if (Mathf.Abs(player.transform.position.x - transform.position.x) > 2f)
            {
                //Moves a value current towards target.
                float x = Mathf.MoveTowards(transform.position.x, player.transform.position.x, Time.deltaTime * speed);
                transform.position = new Vector3(x, transform.position.y, 0);
                return true;
            }
        } 
        return false;
    }
    public void Hurt()
    {
        curHealth--;
        if (curHealth < 0)
        {
            animator.SetTrigger("die");
        }
    }
    public void Die()
    {
        SoundManege.Instance.PlayerMusicName("jetExplosion");
        gameui.ShowWin();
        GameObject.Destroy(gameObject);
        SoundManege.Instance.StopMusic();
        SceneManager.LoadScene(0);  //加载到第1个场景
    }

    public void Skill1()
    {
        timer += Time.deltaTime;
        if (timer >= coolTime)
        {
            //生成炸弹预制体
            GameObject.Instantiate(Boom, tr[0].position, Quaternion.identity);
            GameObject.Instantiate(Boom, tr[1].position, Quaternion.identity);
            timer = 0;
        }
    }

    public void Skill2()
    {
        timer += Time.deltaTime;
        if (timer >= coolTime)
        {
            //生成炸弹预制体
            GameObject.Instantiate(bossBullet, tr[2].position, Quaternion.identity);
            GameObject.Instantiate(bossBullet, tr[3].position, Quaternion.identity);
            timer = 0;
        }
    }

    public void Skill3()
    {
        timer += Time.deltaTime;
        if (timer >= coolTime)
        {
            //生成炸弹预制体
            GameObject.Instantiate(bullet2, tr[2].position, Quaternion.identity);
            GameObject.Instantiate(bullet2, tr[3].position, Quaternion.identity);
            timer = 0;
        }
    }

    public void Attack()
    {
        int a = Random.Range(0, 101);       //[0,101)
        if (a < 33)
        {
            Skill1();
        }
        else if (a > 33 && a <= 66)
        {
            Skill2();
        }
        else
        {
            Skill3();
        }
    }
}
