using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private readonly float speed = 1.5f;
    GameObject player;

    // Start is called before the first frame update
    [System.Obsolete]
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //use Object.Destroy instead
        GameObject.DestroyObject(gameObject, 2.3f);
    }

    private void Move()
    {
        Vector3 dir = new Vector3();
        if (player != null)
        {
            dir = player.transform.position - transform.position;
            //追终弹设置
            transform.position += speed * dir * Time.deltaTime;
        }
    }
    
    //为子弹添加触发器，碰撞检测
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //与玩家碰撞检测
        {
            if (collision.GetComponent<Player>().GetCurState() == false)
            {
                return;
            }
            collision.GetComponent<Player>().Hurt();
            GameObject.Destroy(gameObject);
        }
    }
    // Update is called once per frame
    void Update()
    {
        Move();
    }
}
