using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossTrigger : MonoBehaviour
{
    private int num = 0;

    public GameObject boss;

    public Transform point;
    // Start is called before the first frame update
    void Start()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            num++;
        }
        if (num == 1)
        {
            GameObject temp = Resources.Load<GameObject>("Prefabs/Boss");
            /*GameObject temp = GameObject.FindGameObjectWithTag("Boss");
            temp.SetActive(true);*/
            boss = Instantiate(temp,point.position,Quaternion.identity);
            num=2;
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
