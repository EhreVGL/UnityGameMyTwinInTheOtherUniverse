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
    [SerializeField] private RawImage endFrame;
    private Color color;
    private int endStatu;

    private void Awake()
    {
        cameraPosition = new Vector3(0f, 8f, -18f);
        startShooting = false;
        shoot = false;
        takeBallValue = true;
        ballValue = 1;
        touchBeganPosition = 0;
        touchEndedPosition = 0;
        forceValueX = 0.01f;
        forceValueZ = -20f;
        color = new Color(0, 0, 0, 0);
        endStatu = 0;

    }
    private void Start()
    {
        ballValueText.enabled = false;
        for (int i = 0; i < 3; i++)
        {
            backStars[i].enabled = false;
            frontStars[i].enabled = false;

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
        if(startShooting)
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
                        if (ballValue < 0)
                        {
                            startShooting = false;
                            endFrame.enabled = true;
                            FinishBall();
                        }
                        else
                        {
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

            }
        }
    }

    private void FinishBall()
    {
        for(int i = 0; i < 3; i++)
        {
            if (frontStars[i].enabled == true)
            {
                endStatu++;
            }
        }
        StartCoroutine(NextLevel());
    }

    IEnumerator NextLevel()
    {
        while (true)
        {
            color.a += 0.05f;
            endFrame.GetComponent<Image>().color = color;
            yield return new WaitForSeconds(0.1f);

            if (color.a == 1)
            {
                if(endStatu == 3)
                {
                    //NextLevel
                }
                else
                {
                    //CurrentLevel
                }
                yield break;
            }
        }
    }
}
