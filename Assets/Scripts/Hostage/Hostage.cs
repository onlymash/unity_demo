using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hostage : MonoBehaviour
{
    private Animator animator;
    private new Rigidbody2D rigidbody2D;
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2D = GetComponent<Rigidbody2D>();

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            animator.SetTrigger("released");
        }
    }

    public void Move()
    {
        rigidbody2D.velocity = new Vector2(-2, 0);

    }

    public void Destory()
    {
        GameObject.Destroy(gameObject);
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
