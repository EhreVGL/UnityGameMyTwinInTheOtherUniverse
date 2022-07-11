using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour
{
    //Balls
    private Queue<GameObject> ballsPool;
    [SerializeField] private GameObject ballPrefab;
    private int totalBall;

    //Blocks
    private Queue<GameObject> blocksPool;
    [SerializeField] private GameObject[] blocksPrefab;
    private int totalBlock;

    private void Awake()
    {
        //Ball
        totalBall = 18;
        ballsPool = new Queue<GameObject>();

        for (int i = 0; i < totalBall; i++)
        {
            GameObject ball = Instantiate(ballPrefab);
            ball.SetActive(false);
            ballsPool.Enqueue(ball);
        }

        //Block
        totalBlock = 12;
        blocksPool = new Queue<GameObject>();

        for (int i = 0; i < totalBlock; i++)
        {
            GameObject block = Instantiate(blocksPrefab[Random.Range(0,2)]);
            block.SetActive(false);
            blocksPool.Enqueue(block);
        }
    }

    public GameObject GetBallObject()
    {
        GameObject obj = ballsPool.Dequeue();

        obj.SetActive(true);

        return obj;
    }

    public GameObject GetBlockObject()
    {
        GameObject obj = blocksPool.Dequeue();

        obj.SetActive(true);

        return obj;
    }

}
