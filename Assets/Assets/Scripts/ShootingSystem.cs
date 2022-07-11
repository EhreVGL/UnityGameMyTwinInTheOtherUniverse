using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class ShootingSystem : MonoBehaviour
{
    [SerializeField] private GameObject ball;
    [SerializeField] private Rigidbody rb;
    [SerializeField] private Camera mainCamera;
    private GameObject shootingBoard;
    private bool moveBoard;
    private Vector3 BoardPos;
    private int BoardPosRotation;
    private Vector3 cameraPosition;
    private bool startShooting;
    private bool shoot;
    private bool takeBallValue;
    [SerializeField] private int ballValue;
    private Touch touch;

    private float touchBeganPosition;
    private float touchEndedPosition;
    private float forceValueX;
    private float forceValueZ;

    [SerializeField] private GameObject Ground;
    [SerializeField] private GameObject shootingBallPosition;

    //UI Elements
    [SerializeField] private TextMeshProUGUI ballValueText;
    [SerializeField] private RawImage[] backStars;
    [SerializeField] private RawImage[] frontStars;

    private float timer;

    private void Awake()
    {
        cameraPosition = new Vector3(0f, 8f, -18f);
        BoardPos = new Vector3(0.152f, 6.430001f, -0.2415566f);
        BoardPosRotation = 1;
        startShooting = false;
        shoot = false;
        takeBallValue = true;
        ballValue = 1;
        touchBeganPosition = 0;
        touchEndedPosition = 0;
        forceValueX = 0.01f;
        forceValueZ = -20f;
        timer = 4f;
    }
    private void Start()
    {
        ballValueText.enabled = false;
        for (int i = 0; i < 3; i++)
        {
            backStars[i].enabled = false;
            frontStars[i].enabled = false;

        }
        if (SaveLevel.singleton.GetLevel() >= 3)
        {
            shootingBoard = transform.GetChild(1).gameObject;
            BoardPos.x = 0.5f;
            shootingBoard.transform.localPosition = BoardPos;
            moveBoard = true;
        }
    }
    // Update is called once per frame
    void Update()
    {
        if(mainCamera.transform.position == cameraPosition)
        {
            startShooting = true;

            Ground.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        if (moveBoard)
        {
            moveBoard = false;
            StartCoroutine(MoveBoardPosition());
        }
        if (startShooting)
        {
            //UI Enable
            ballValueText.enabled = true;
            for (int i = 0; i < 3; i++)
            {
                backStars[i].enabled = true;
            }

            if(Input.touchCount > 0)
            {
                touch = Input.GetTouch(0);

                if (touch.phase == TouchPhase.Began)
                {
                    shoot = false;
                    touchBeganPosition = touch.position.x;
                }
                if (touch.phase == TouchPhase.Ended)
                {
                    touchEndedPosition = touch.position.x;

                    if (!shoot)
                    {
                        if (takeBallValue)
                        {
                            takeBallValue = false;
                            ballValue = shootingBallPosition.transform.childCount - 1;
                        }

                        shoot = true;


                        //GameObject ballclone = shootingBallPosition.transform.GetChild(ballValue).gameObject;
                        GameObject ballclone = shootingBallPosition.transform.GetChild(ballValue).gameObject;
                        ballValueText.text = "x" + ballValue;
                        ballValue--;
                        ballclone.SetActive(true);
                        ballclone.transform.position = shootingBallPosition.transform.position;
                        ballclone.gameObject.GetComponent<BallMechanic>().triggerTrowel = false;
                        rb = ballclone.GetComponent<Rigidbody>();
                        rb.constraints = RigidbodyConstraints.None;
                        rb.AddForce((touchBeganPosition - touchEndedPosition) * forceValueX, 0, forceValueZ, ForceMode.Impulse);
                        
                    }

                }

            }
            if (ballValue < 0)
            {
                timer -= Time.fixedDeltaTime;
                if(timer < 0)
                {
                    startShooting = false;
                    FinishBall();
                }
            }
        }


    }
    private void FinishBall()
    {
        this.gameObject.transform.GetChild(1).gameObject.GetComponent<Ending>().zeroBall = true;
    }

    IEnumerator MoveBoardPosition()
    {
        while (true)
        {
            if (shootingBoard.transform.localPosition.x > 0.5f)
            {
                BoardPosRotation = -1;
            }
            else if (shootingBoard.transform.localPosition.x < -0.3f)
            {
                BoardPosRotation = 1;
            }
            
            BoardPos.x += (0.005f * BoardPosRotation);
            shootingBoard.transform.localPosition = BoardPos;

            yield return new WaitForSeconds(0.01f);

        }
    }
}
