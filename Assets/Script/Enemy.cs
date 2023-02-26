using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    public float speed;
    public  int health;
    public int scoreValue;
    public Sprite[] sprites;
    public GameObject EnemyBullet;
  
    public GameObject[] items;

    float curloadtime = 0f;
    float maxloadtime = 1f;
    Rigidbody2D rigid;
    SpriteRenderer sprite;

    private void Start()
    {
        
        
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
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag=="BulletBorder")
        {
            Destroy(gameObject);
        }

        if (collision.gameObject.tag=="PlayerBullet")
        {
            sprite.sprite = sprites[1];
            Invoke(nameof(spriteReset),0.1f);
            PlayerBullet playerBullet = collision.gameObject.GetComponent<PlayerBullet>();

            dieEnmey(playerBullet.bulletPower);
            
            Destroy(collision.gameObject);
           
        }

    }
    public void SpawnItem()
    {
        int itemType = Random.Range(0, 3);
        GameObject item = Instantiate(items[itemType], transform.position,Quaternion.identity);

    }
    void spriteReset()
    {
        sprite.sprite=sprites[0];
    }
   
    void dieEnmey(int power)
    {

        health -= power;
        if (health<=0)
        {
            GameManager.Instance.score += scoreValue;
            SpawnItem();
            Destroy(gameObject);
        }
    }
    
    void FireEnemyBullet()
    {
        if (curloadtime>maxloadtime)
        {
            curloadtime = 0;
            GameObject enemyBullet = Instantiate(EnemyBullet, transform.position, transform.rotation);
            
        }
       
    }
    
}
