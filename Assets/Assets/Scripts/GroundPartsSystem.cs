using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundPartsSystem : MonoBehaviour
{
    private int whichPart;
    private bool isThereBottom;

    private bool playerTriggerExit;
    private bool becameInvisible;
    private bool spawnObject;
    private int changePositionCounter;

    private int spawnBallCount;
    private ObjectPoolingSystem poolingSystem;
    private Vector3 spawnPosition;
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
        spawnObject = true;
        changePositionCounter = 2;

        spawnBallCount = 2;
        spawnPosition = Vector3.zero;
    }
    // Start is called before the first frame update
    void Start()
    {
        poolingSystem = transform.parent.GetComponent<ObjectPoolingSystem>();
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
            SpawningObject();

        }
        else if(whichPart == 3)
        {
            ChangePosition();
            SpawningObject();
        }
        else if(whichPart == 4)
        {
            ChangePosition();
            SpawningObject();
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
            spawnObject = true;
        }
    }
    //Random.Range(transform.position.z - 12, transform.position.z)
    private void SpawningObject()
    {
        if (spawnObject)
        {
            if (isThereBottom)
            {
                for (int i = spawnBallCount; i > 0; i--)
                {
                    if (i == 2)
                    {
                        spawnPosition = new Vector3(Random.Range(-4, 4), transform.position.y + 1, Random.Range(transform.position.z - 6, transform.position.z + 6));
                    }
                    else
                    {
                        spawnPosition.x += 2;
                    }
                    GameObject ball = poolingSystem.GetBallObject();
                    ball.transform.position = spawnPosition;
                }
                spawnObject = false;
            }
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
        Debug.Log("ASDASD");
        if (!isThereBottom) { Debug.Log("TopGround ASDASD"); }
        becameInvisible=true;
    }

}
