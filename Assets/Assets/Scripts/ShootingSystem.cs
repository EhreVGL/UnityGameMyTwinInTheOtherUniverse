using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    private Vector3 cameraPosition;
    private bool startShooting;
    [SerializeField] private int ballValue;
    private Touch touch;

    private Vector2 touchBeganPosition;
    private Vector2 touchEndedPosition;
    private Vector2 forceDirection;
    private float forceValueXAndY;
    private float forceValueZ;
    private float touchBeganTime;
    private float touchEndedTime;
    private void Awake()
    {
        cameraPosition = new Vector3(0f, 6f, -18f);
        startShooting = false;
        ballValue = 3;
        touchBeganPosition = Vector2.zero;
        touchEndedPosition = Vector2.zero;
        forceDirection = Vector2.zero;
        forceValueXAndY = 1f;
        forceValueZ = 50f;
        touchBeganTime = 0f;
        touchEndedTime = 0f;
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(mainCamera.transform.position == cameraPosition)
        {
            startShooting = true;
        }
    }

    private void FixedUpdate()
    {
        if(startShooting && ballValue > 0 && Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);

            if (touch.phase == TouchPhase.Began)
            {
                touchBeganPosition = touch.position;
                touchBeganTime = Time.time;
            }
            if(touch.phase == TouchPhase.Ended)
            {
                touchEndedPosition = touch.position;
                touchEndedTime = Time.time;

                forceDirection = touchBeganPosition - touchEndedPosition;

                GameObject ballclone =  Instantiate(ball, cameraPosition, Quaternion.identity);
                rb = ballclone.GetComponent<Rigidbody>();
                rb.AddForce(-forceDirection.x * forceValueXAndY, -forceDirection.y * forceValueXAndY, forceValueZ / (touchEndedTime - touchBeganTime));
            }


        }
    }
}
