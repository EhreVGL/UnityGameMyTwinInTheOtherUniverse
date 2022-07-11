using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class CharacterMovement : MonoBehaviour
{
    // UI OBJECTS
    [SerializeField] private TextMeshProUGUI startText;
    [SerializeField] private Image startLayer;
    [SerializeField] private TextMeshProUGUI ballValueText;
    // CAMERA OBJECTS
    [SerializeField] private Camera mainCamera;
    private Vector3 cameraPosition;

    private Rigidbody rb;
    private Transform transform;
    private Vector3 constantMovement;
    private float constantMovementMultiplier;
    private Vector3 verticalMovement;
    private float verticalMovementMultiplier;

    private Touch touch;
    private Vector2 touchBeganPosition;

    private bool isFinish;

    private bool whichPlatform;

    [SerializeField] private GameObject shootingBallPosition;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        transform = GetComponent<Transform>();
        constantMovement = Vector3.forward;
        constantMovementMultiplier = 16f;
        verticalMovement = Vector3.right;
        verticalMovementMultiplier = 4f;
        touchBeganPosition = Vector2.zero;

        isFinish = false;
        whichPlatform = transform.position.y < 6 ? true : false;
    }

    // Start is called before the first frame update
    void Start()
    {
        cameraPosition = new Vector3(mainCamera.transform.position.x - transform.position.x, mainCamera.transform.position.y - transform.position.y, mainCamera.transform.position.z - transform.position.z);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        rb.velocity = Vector3.zero;
        if (!isFinish)
        {
            mainCamera.transform.position = transform.position + cameraPosition;
            if (Input.touchCount > 0)
            {
                //Disable start UI
                startText.enabled = false;
                startLayer.enabled = false;

                touch = Input.GetTouch(0);

                //Move z-axis
                transform.position += whichPlatform ? (constantMovement * (constantMovementMultiplier * Time.fixedDeltaTime)) : (constantMovement * (-constantMovementMultiplier * Time.fixedDeltaTime));

                if (touch.phase == TouchPhase.Began)
                {
                    touchBeganPosition = touch.position;
                }

                //Move x-axis
                if (touchBeganPosition.x - touch.position.x > 30)
                {
                    if(transform.position.x > -5)
                    {
                        transform.position -= verticalMovement * (verticalMovementMultiplier * Time.fixedDeltaTime);
                    }
                }
                else if (touchBeganPosition.x - touch.position.x < -30)
                {
                    if (transform.position.x < 5)
                    {
                        transform.position += verticalMovement * (verticalMovementMultiplier * Time.fixedDeltaTime);
                    }
                }

            }
        }
    }

    private void OnTriggerEnter(Collider collision)
    {
        if(collision.gameObject.layer == 6)
        {
            isFinish = true;

            if (whichPlatform)
            {
                mainCamera.GetComponent<CameraPhaseController>().transformPlayer = true;
            }
            else
            {
                mainCamera.GetComponent<CameraPhaseController>().transformFPS = true;
                ballValueText.text = "x" + (transform.childCount-1);
                while (transform.childCount > 1)
                {
                    transform.GetChild(1).gameObject.SetActive(false);
                    transform.GetChild(1).transform.parent = shootingBallPosition.transform;
                }
            }
        }
    }
}
