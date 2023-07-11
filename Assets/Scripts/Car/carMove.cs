using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class carMove : MonoBehaviour
{
    public GameObject player;

    public Transform bornPoint;

    public GameUI gameUI;

    // Start is called before the first frame update
    void Start()
    {
        SoundManege.Instance.PlayerMusicName("Car");
        transform.DOLocalMoveX(-2.4f, 1.5f).OnComplete(
            () =>
            {
                GameObject.Instantiate(player, bornPoint.position, Quaternion.identity);
                Camera.main.GetComponent<CameraMove>().enabled = true;
                Destroy(gameObject, 0.5f);
            });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
