using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPartsSystem : MonoBehaviour
{
    private int whichPart;
    private bool isThereBottom;

    private bool playerTriggerExit;
    private bool becameInvisible;
    private bool spawnBallObject;
    private bool spawnBlockObject;
    private int changePositionCounter;

    private int spawnBallCount;
    private ObjectPoolingSystem poolingSystem;
    private Vector3 spawnPosition;
    private float movePartRotation;

    //LevelData
    private int level;
    private void Awake()
    {
        for(int i = 1; i < 5; i++)
        {
            if(this.gameObject.name == "Part" + i)
            {
                whichPart = i;
            }
        }

        // Bottom or 
        if (transform.parent.gameObject.name == "BottomGround")
        {
            isThereBottom = true;
        }
        else
        {
            isThereBottom = false;
        }

        playerTriggerExit = false;
        becameInvisible = false;
        spawnBallObject = true;
        spawnBlockObject = true;
        changePositionCounter = 2;

        spawnBallCount = 2;
        spawnPosition = Vector3.zero;
        movePartRotation = -0.2f;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolingSystem = transform.parent.transform.parent.GetComponent<ObjectPoolingSystem>();
        level = SaveLevel.singleton.GetLevel();
    }

    // Update is called once per frame
    void Update()
    {
        if(whichPart == 1)
        {
            ChangePosition();
        }
        else if(whichPart == 2)
        {
            ChangePosition();
            if (level == 1)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 2)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 3)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 4)
            {
                spawnBallCount = 1;
                SpawningBalls();
                SpawningBlock();
            }
            else if (level == 5)
            {
                spawnBallCount = 1;
                SpawningBalls();
            }

        }
        else if(whichPart == 3)
        {
            ChangePosition();
            if (level == 1)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 2)
            {
                SpawningBlock();
            }
            else if (level == 3)
            {
                SpawningBlock();
            }
            else if (level == 4)
            {
                SpawningBlock();
            }
            else if (level == 5)
            {
                ChangeScale();  
            }
        }
        else if(whichPart == 4)
        {
            ChangePosition();
            if (level == 1)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 2)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 3)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
            else if (level == 4)
            {
                spawnBallCount = 3;
                SpawningBalls();
            }
            else if (level == 5)
            {
                spawnBallCount = 2;
                SpawningBalls();
            }
        }
    }
    private void ChangePosition()
    {
        
        if (playerTriggerExit && becameInvisible && changePositionCounter > 0)
        {
            playerTriggerExit = false;
            becameInvisible = false;
            changePositionCounter--;

            if (isThereBottom)
            {
                transform.position += (Vector3.forward * 48);
            }
            else
            {
                transform.position -= (Vector3.forward * 48);
            }

            //SpawnObject
            spawnBallObject = true;
            spawnBlockObject = true;
        }
    }
    private void ChangeScale()
    {
        if (isThereBottom)
        {
            if (transform.localPosition.x < -15)
            {
                movePartRotation = -0.2f;
            }
            else if (transform.localPosition.x > 15)
            {
                movePartRotation = 0.2f;
            }
            transform.localPosition += (Vector3.left * movePartRotation);
        }
    }
    //Random.Range(transform.position.z - 12, transform.position.z)
    private void SpawningBalls()
    {
        if (spawnBallObject)
        {
            if (isThereBottom)
            {
                spawnPosition = new Vector3(Random.Range(-4, 4), transform.position.y + 1, Random.Range(transform.position.z - 6, transform.position.z + 6));
                for (int i = spawnBallCount; i > 0; i--)
                {
                    if (i == 3)
                    {
                        spawnPosition.x -= 1.25f;
                    }
                    else if (i == 2)
                    {
                        spawnPosition.x += 1.25f;
                    }
                    else
                    {
                        spawnPosition.x += 1.25f;
                    }
                    GameObject ball = poolingSystem.GetBallObject();
                    ball.transform.position = spawnPosition;
                }
                spawnBallObject = false;
            }
        } 
    }

    private void SpawningBlock()
    {
        if (spawnBlockObject)
        {
            if (isThereBottom)
            {
                spawnPosition = new Vector3(Random.Range(-4, 4), transform.position.y + 0.25f, Random.Range(transform.position.z - 6, transform.position.z + 6));
            }
            else
            {
                spawnPosition = new Vector3(Random.Range(-4, 4), transform.position.y - 0.25f, Random.Range(transform.position.z - 6, transform.position.z + 6));

            }

            GameObject block = poolingSystem.GetBlockObject();
            block.transform.position = spawnPosition;
            
            spawnBlockObject = false;
        }
    }

    private void OnTriggerExit(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            //Debug.Log("Player " + this.gameObject.name + "'dan çýktý.");
            playerTriggerExit=true;
        }
    }
    private void OnBecameInvisible()
    {
        becameInvisible=true;
    }

}
