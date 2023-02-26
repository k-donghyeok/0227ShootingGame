using Mono.Cecil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    int i = 0;
    public float speed =5f;
    public int power = 1;
    public int playerHealth=2;
    public int playerLife=2;
    public float curDeadTime=0f;
    public float loadDeadTime;
    public float maxDeadTime=2f;
    public bool playerIsHit = false;
    public GameObject skillEffect;
    
    public GameObject[] Life;
    
    float curloadtime=0f;
    float maxloadtime=0.2f;

    float curSkillTime=0f;
    float maxSkillTime=15f;

    bool isTouchTop;
    bool isTouchBottom;
    bool isTouchLeft;
    bool isTouchRight;
    public GameObject playerBulletObj1; //power1 �ҷ�
    public GameObject playerBulletObj2; //power2 �ҷ�
    public GameManager gameManager;
    Animator anim;
    Rigidbody2D rigid;
    
    
    // Start is called before the first frame update
   
    void Start()
    {
        rigid= GetComponent<Rigidbody2D>(); 
        anim= GetComponent<Animator>();
        
    }

    // Update is called once per frame
    void Update()
    {
        curDeadTime += Time.deltaTime;
        move();
        Fire();
        Reload();
        Skill();
        ReloadSkill();
    }

    void ReloadSkill()
    {
        curSkillTime += Time.deltaTime;
    }
    void Skillstop()
    {
        skillEffect.SetActive(false);
    }
    void Skill()
    {
        if (Input.GetButton("Fire3"))
        {
            if (GameManager.Instance.BoomCount>0 && curSkillTime>maxSkillTime)
            {
                skillEffect.SetActive(true);
                Invoke(nameof(Skillstop),1f);
                curSkillTime = 0f;
                GameManager.Instance.BoomCount--;
                GameObject[] Enemy = GameObject.FindGameObjectsWithTag("Enemy");
                GameObject[] EnemyBullet = GameObject.FindGameObjectsWithTag("EnemyBullet");
                for (int j = 0; j <= Enemy.Length-1; j++)
                {
                    Destroy(Enemy[j]);
                }
                for (int j = 0; j <= EnemyBullet.Length - 1; j++)
                {
                    Destroy(EnemyBullet[j]);
                }
            }
        }

    }
    void Fire() //�Ѿ˹߻�
    {
        if (Input.GetButton("Fire1")) 
        {
            if (curloadtime>maxloadtime)
            {
                switch (power)
                {
                    case 1:
                        {
                            GameObject playerBullet = Instantiate(playerBulletObj1, transform.position, transform.rotation);
                            curloadtime = 0;
                        }
                        break;
                    case 2:
                        {
                            GameObject playerBulletL = Instantiate(playerBulletObj1, transform.position + Vector3.right*0.2f, transform.rotation);
                            curloadtime = 0;
                            GameObject playerBulletR = Instantiate(playerBulletObj1, transform.position + Vector3.left * 0.2f, transform.rotation);
                            curloadtime = 0;
                        }
                        break;
                    case 3:
                        {
                            GameObject playerBullet = Instantiate(playerBulletObj2, transform.position, transform.rotation);
                            curloadtime = 0;
                            GameObject playerBulletL = Instantiate(playerBulletObj1, transform.position + Vector3.right * 0.3f, transform.rotation);
                            curloadtime = 0;
                            GameObject playerBulletR = Instantiate(playerBulletObj1, transform.position + Vector3.left * 0.3f, transform.rotation);
                            curloadtime = 0;
                        }
                        break;
                }
               
            }
          
        }
    }



    void Reload() // �Ѿ˹߻�ӵ��� �����ϱ����� �ð�����
    {
        curloadtime += Time.deltaTime;
    }
    void move() //�÷��̾� �̵� ���� 
    {
        float h=Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");
        if ((isTouchLeft && h<0 ) || (isTouchRight && h>0))
        {
            h = 0;
        }
        if ((isTouchTop && v > 0) || (isTouchBottom && v < 0))
        {
            v = 0;
        }

        rigid.velocity = new Vector2(h * speed, v * speed);

        if (playerHealth==0)
        {
            rigid.velocity = new Vector2(0, 0);
        }
        

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonUp("Horizontal"))
        {
            anim.SetInteger("Input", (int)h);
        }
       
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.name=="Top")
        {
            isTouchTop = true;
        }
        if (collision.gameObject.name == "Bottom")
        {
            isTouchBottom=true;
        }
        if (collision.gameObject.name == "Left")
        {
            isTouchLeft=true;
        }
        if (collision.gameObject.name == "Right")
        {
            isTouchRight=true;
        }

        if (collision.gameObject.tag=="EnemyBullet")
        {
            if (playerHealth>=0)
            {
                isHit();
                Destroy(collision.gameObject);
                

            }

        }
        if (collision.gameObject.tag =="Coin")
        {
            GameManager.Instance.score += 50;
            Destroy(collision.gameObject);
        }
        if (collision.gameObject.tag == "Boom")
        {
            if (GameManager.Instance.BoomCount<= 1)
            {
                GameManager.Instance.BoomCount += 1;
                Destroy(collision.gameObject);
            }
            
        }
        if (collision.gameObject.tag == "Power")
        {
            if (power<3)
            {
                power++;
                Destroy(collision.gameObject);
            }
        }

    }
    void AnimReset()
    {

        anim.Play("Player");
    }

    void PlayerHealthReset()
    {
        playerHealth = 2;
    }
    void isHit()
    {
        if (curDeadTime>maxDeadTime)
        {
            if (curDeadTime>loadDeadTime)
            {
                loadDeadTime = curDeadTime;
            }
            curDeadTime = 0;

            if (playerHealth>0)
            {
                anim.SetTrigger("IsHit");//�����Ÿ��¾ִ�
                
                playerHealth -= 1;
                Invoke(nameof(AnimReset),1f);
            }
            
         
            
            if (playerHealth == 0)
            {
                maxloadtime = 100;
                anim.SetTrigger("Dead");//������ �ִϳ����� 1�ʵڿ� ������ ���������� ������ ������ 0 �̸� ��������
                playerLife -= 1;
                if (playerLife>=0 )
                {
                    Life[i].SetActive(false);
                    i++;
                }
                
                if (playerLife == 0)
                {
                    gameManager.GameOver();
                }


                Invoke(nameof(PlayerHealthReset), 1.2f);
                Invoke(nameof(AnimReset), 1f);
                gameManager.goRespawnPlayer();
                maxloadtime = 0.2f;
            }

            
            
           
        }
        

    }
    
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.name == "Top")
        {
            isTouchTop = false;
        }
        if (collision.gameObject.name == "Bottom")
        {
            isTouchBottom = false;
        }
        if (collision.gameObject.name == "Left")
        {
            isTouchLeft = false;
        }
        if (collision.gameObject.name == "Right")
        {
            isTouchRight = false;
        }
    }
}
