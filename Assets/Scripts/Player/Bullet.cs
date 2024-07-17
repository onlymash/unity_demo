using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private readonly int damage = 1;   //子弹伤害

    private readonly float speed = 6f;

    private Rigidbody2D rd;

    private void Awake()
    {
        rd = GetComponent<Rigidbody2D>();  //获取子弹上的Rigidbody2D组件
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject.Destroy(gameObject, 5f);     //每个5秒自动销毁
    }

    public void InitDir(Dir dir)
    {
        Vector2 v2;

        switch (dir)
        {
            case Dir.Up:
                transform.rotation = Quaternion.Euler(0, 0, -90);
                v2 = new Vector2(0, speed);
                rd.velocity = v2;       //刚体速度
                break;
            case Dir.Forward:
                //获取人物位置
                Transform player = GameObject.FindGameObjectWithTag("Player").transform;
                //人物往右走
                if (player.rotation.y == 0)
                {
                    v2 = new Vector2(speed, 0);
                    transform.rotation = Quaternion.Euler(0, 180, 0);
                }
                else
                {
                    v2 = new Vector2(-speed, 0);
                    transform.rotation = Quaternion.Euler(0, 0, 0);
                }
                rd.velocity = v2;       //刚体速度
                break;
        }
    }

    public void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            collision.GetComponent<Enemy>().Hurt();
            Destroy(gameObject);
        }
        if (collision.CompareTag("Boss"))
        {
            collision.GetComponent<Boss>().Hurt();
            Destroy(gameObject);
        }
    }
}
