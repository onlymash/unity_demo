using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BronTrigger : MonoBehaviour
{
    public GameUI gameui;

    private int num = 0;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        {
            num++;
            if (num == 1)
            {
                gameui.ShowStart();
            }
        }
    }
}
