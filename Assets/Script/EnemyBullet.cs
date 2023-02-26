using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class EnemyBullet : MonoBehaviour
{
    Rigidbody2D rigid;
    
    public float speed = 4f;
    // Start is called before the first frame update
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>();
        
        rigid.velocity=new Vector2(GameManager.Instance.Player.transform.position.x - transform.position.x, GameManager.Instance.Player.transform.position.y - transform.position.y).normalized*speed;
       
    }

    // Update is called once per frame
   

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.gameObject.tag=="BulletBorder") 
        {
            Destroy(gameObject);
        }
    }
}
