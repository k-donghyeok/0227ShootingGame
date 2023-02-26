using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBullet : MonoBehaviour
{
    float speed = 10f; // �Ѿ˼ӷ�
    public int bulletPower;
    Rigidbody2D rigid;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
        rigid.velocity = new Vector2(0,speed); //�Ѿ˼ӵ��� ������Ʈ ������ start�� ����
    }

    // Update is called once per frame
    private void OnTriggerEnter2D(Collider2D collision) //�Ѿ˻���
    {
        if (collision.gameObject.tag=="BulletBorder" )
        {
            Destroy(gameObject);
        }
    }
}
