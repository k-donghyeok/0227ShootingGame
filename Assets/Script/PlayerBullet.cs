using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 10f; // 총알속력
    public int bulletPower;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0,speed); //총알속도를 오브젝트 생성시 start로 지정
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) //총알삭제
    {
        if (collision.gameObject.tag=="BulletBorder" )
        {
            Destroy(gameObject);
        }
    }
}
