using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet1 : MonoBehaviour
{
    private float speed = 1.5f;
    GameObject player;

    // Start is called before the first frame update
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
            //×·ÖÕµ¯ÉèÖÃ
            transform.position += speed * dir * Time.deltaTime;
        }
    }
    
    //Îª×Óµ¯Ìí¼Ó´¥·¢Æ÷£¬Åö×²¼ì²â
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")  //ÓëÍæ¼ÒÅö×²¼ì²â
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
