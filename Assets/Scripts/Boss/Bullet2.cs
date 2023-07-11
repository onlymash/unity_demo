using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet2 : MonoBehaviour
{
    private float speed = 1.5f;
    GameObject player;

    //子弹方向
    public Vector3 dir;
    
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
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        //use Object.Destroy instead
        GameObject.DestroyObject(gameObject, 2.3f);
        dir = player.transform.position - transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += speed * dir * Time.deltaTime;
    }
}
