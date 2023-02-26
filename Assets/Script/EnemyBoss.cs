using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBoss : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public  int health;
    public int scoreValue;
    public Sprite[] sprites;
    public GameObject EnemyBullet;
     public GameObject EnemyBulletfast;
  
  
    public GameObject[] items;

    float curloadtime = 0f;
    float maxloadtime = 1f;
    Rigidbody2D rigid;
    SpriteRenderer sprite;
    Animator anim;

    private void Start()
    {
        anim= GetComponent<Animator>(); 
        rigid.velocity = new Vector2(0, -2f);
        Invoke(nameof(stop), 2f);
    }
    private void Awake()
    {
        rigid= GetComponent<Rigidbody2D>();
        sprite= GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        curloadtime += Time.deltaTime;
        FireEnemyBullet();
    }

    void stop()
    {
        rigid.velocity = new Vector2(0, 0);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
       

        if (collision.gameObject.tag=="PlayerBullet")
        {
            anim.SetTrigger("Hit");
            Invoke(nameof(spriteReset),0.2f);
            PlayerBullet playerBullet = collision.gameObject.GetComponent<PlayerBullet>();

            dieEnmey(playerBullet.bulletPower);
            
            Destroy(collision.gameObject);
           
        }

    }
   
    void spriteReset()
    {
        anim.Play("Boss");
    }
   
    void dieEnmey(int power)
    {

        health -= power;
        if (health<=0)
        {
            GameManager.Instance.score += scoreValue;
            
            Destroy(gameObject);
            GameManager.Instance.GameWin();
        }
    }
    
    void FireEnemyBullet()
    {
        if (curloadtime>maxloadtime)
        {
            curloadtime = 0;
            GameObject enemyBulletL = Instantiate(EnemyBullet, transform.position+Vector3.left*0.2f, transform.rotation);
            GameObject enemyBulletR = Instantiate(EnemyBullet, transform.position + Vector3.right * 0.2f, transform.rotation);

        }
       
    }
    
}
