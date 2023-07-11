using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    //½üÕ½¹¥»÷¾àÀë
    private float dis = 2.5f;

    //
    private GameObject[] GetCanAttackTargets(float dis)
    {
        GameObject[] enemy = GameObject.FindGameObjectsWithTag("Enemy");
        Transform player = this.transform;
        if (enemy == null)
        {
            return null;
        }
        List<GameObject> list = new List<GameObject>();
        for(int i = 0; i < enemy.Length; i++)
        {
            float d = Vector2.Distance(enemy[i].transform.position, player.position);
            if (d < dis)
            {
                list.Add(enemy[i]);
            }        
        }
        return list.ToArray();
    }

    public void HurtEnemys()
    {
        GameObject[] enemys = GetCanAttackTargets(dis);
        foreach(var enemy in enemys)
        {
            enemy.GetComponent<Enemy>().Hurt();
        }
    }
}
