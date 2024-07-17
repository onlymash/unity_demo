using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grenade : MonoBehaviour
{
    private new Rigidbody2D rigidbody2D;

    private Animator anim;

    //判断是否发生接触，false是未发生接触
    private bool isTouched = false;
    // Start is called before the first frame update
    void Start()
    {
        //获取手榴弹的Rigidbody2D
        rigidbody2D = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        Init();
    }

    private void Update()
    {
        //让炸弹绕自己旋转
        if (isTouched == false)
        {
            rigidbody2D.transform.Rotate(new Vector3(0, 0, 1), Space.Self);
        }
    }

    private void Init()
    {
        //隐藏炸弹动画
        anim.enabled = false;
        //通过标签查找玩家
        Transform player = GameObject.FindGameObjectWithTag("Player").transform;

        if (player.rotation.y == 0)  //玩家朝向右
        {
            rigidbody2D.AddForce(new Vector2(200f, 300f));//添加一个力
        }
        else        //玩家朝向左
        {
            rigidbody2D.AddForce(new Vector2(-200f, 300f));
        }
    }

    //触发检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Enemy")
        {
            collision.GetComponent<Enemy>().Hurt();
        }
        if (collision.tag == "Boss")
        {
            collision.GetComponent<Boss>().Hurt();
        }
        //接触到物体
        isTouched = true;
        //将炸弹速度设置为0
        rigidbody2D.velocity = Vector2.zero;
        //调整炸弹播放角度
        rigidbody2D.transform.rotation = Quaternion.Euler(0, 0, 0);
        //设置重力大小
        gameObject.GetComponent<Rigidbody2D>().gravityScale = 0;
        //播发爆炸动画
        anim.enabled = true;
        //播放音乐
        SoundManege.Instance.PlayerMusicName("GrenadeExplosion");
    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }
}
