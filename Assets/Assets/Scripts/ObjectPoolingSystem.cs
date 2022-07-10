using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour
{

    private Queue<GameObject> ballsPool;
    [SerializeField] private GameObject ballPrefab;
    private int totalBall;


    private void Awake()
    {
        if (gameObject.name == "BottomGround")
        {
            totalBall = 18;
            ballsPool = new Queue<GameObject>();

            for (int i = 0; i < totalBall; i++)
            {
                GameObject ball = Instantiate(ballPrefab);
                ball.SetActive(false);
                ballsPool.Enqueue(ball);
            }
        }
    }

    public GameObject GetBallObject()
    {
        GameObject obj = ballsPool.Dequeue();

        obj.SetActive(true);

        return obj;
    }

}
