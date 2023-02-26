using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class BackGround : MonoBehaviour
{
    public float speed;
    public int startIndex;
    public int endIndex;
    public Transform[] sprites;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 curpos = transform.position;
        Vector3 nextPos = Vector3.down *speed * Time.deltaTime;
        transform.position = curpos+nextPos;

        if (sprites[endIndex].position.y <-10)
        {
            Vector3 backSpritePos = sprites[startIndex].localPosition;
            Vector3 frontSpritePos = sprites[endIndex].localPosition;
            sprites[endIndex].transform.localPosition = backSpritePos + Vector3.up*10;

            int startIndexSave = startIndex;
            startIndex= endIndex;
            endIndex= (startIndexSave-1 == -1) ? sprites.Length-1 :startIndexSave-1;
        }
    }
}
