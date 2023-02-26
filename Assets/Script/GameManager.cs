using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public Transform[] spawnPosition;
    public GameObject[] spawnEnemyType;
    public GameObject Player;
    public GameObject RestartButton;
    public GameObject GameOverText;
    public GameObject Boss;
    public GameObject LastScore;
    public GameObject Victory;

    public float curBossTime=0;
    public float maxBossTime=30;
    public int BoomCount=0;
    public int score=0;
    Rigidbody2D rigid;
    float curSpawnTime=0f;
    float maxSpawnTime=2f;
    
    // Start is called before the first frame update
    private void Awake()
    {
        Instance = this;
        rigid = GetComponent<Rigidbody2D>();
    }

   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        EnemySpawnDelay();
        BossSpawnDelay();
        BossSpawn();

    }
    void BossSpawnDelay()
    {
        curBossTime += Time.deltaTime;
    }
    void EnemySpawnDelay()
    {
        curSpawnTime += Time.deltaTime;
        if (curSpawnTime > maxSpawnTime)
        {
            curSpawnTime = 0f;
            EnemySpawn();

        }
    }
    void BossSpawn()
    {
        if (curBossTime>maxBossTime)
        {
            Boss.SetActive(true);
        }
    }
    
    void EnemySpawn()
    {
        int enemyType = Random.Range(0, 3);
        int spawnPositionType = Random.Range(0, 7);
        GameObject enemys = Instantiate(spawnEnemyType[enemyType], spawnPosition[spawnPositionType].position,transform.rotation);
        Enemy enemyLogic = enemys.GetComponent<Enemy>();
        Rigidbody2D rigid= enemys.GetComponent<Rigidbody2D>();
        if (spawnPositionType >= 0 && spawnPositionType <= 2)
        {
          rigid.velocity= new Vector2(0, -enemyLogic.speed);
        }
        else if (spawnPositionType == 3 || spawnPositionType == 5)
        {
            enemys.transform.Rotate(Vector3.back*45);
            rigid.velocity = new Vector2(-enemyLogic.speed,-1);
        }
        else if (spawnPositionType == 4 || spawnPositionType == 6)
        {
            enemys.transform.Rotate(Vector3.back*-45);
            rigid.velocity = new Vector2(enemyLogic.speed, -1);
        }
    }

    public void goRespawnPlayer()
    {
        Invoke(nameof(respawnPlayer), 1F);
    }

    public void respawnPlayer()
    {
        Player.transform.position = new Vector2(0, -3.5f);
    }

    public void GameOver()
    {
        RestartButton.SetActive(true);
        GameOverText.SetActive(true);
        Player.SetActive(false);
        LastScore.SetActive(true);
    }
    public void GameWin()
    {
        RestartButton.SetActive(true);
        Victory.SetActive(true);
        LastScore.SetActive(true);
        Player.SetActive(false);

    }
}
