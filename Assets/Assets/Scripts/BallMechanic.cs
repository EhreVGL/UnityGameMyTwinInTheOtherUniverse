using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallMechanic : MonoBehaviour
{
    private bool triggerTrowel;
    private bool triggerTopGround;
    private bool triggerContainer;
    private Rigidbody rb;

    private void Awake()
    {
        triggerTrowel = false;
        triggerTopGround = false;
        triggerContainer = false;
        rb = GetComponent<Rigidbody>();
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        if (triggerTrowel)
        {
            rb.AddForce(-2 * Physics.gravity, ForceMode.Acceleration);
        }
        if (triggerTopGround)
        {
            triggerTopGround = false;
            rb.AddForce(10 * Vector3.down, ForceMode.Impulse);
        }
        if (triggerContainer)
        {
            rb.velocity = Vector3.zero;
        }
        if(transform.position.y < -100 || transform.position.y > 100)
        {
            Destroy(this.gameObject);
        }

    }

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            if (triggerTrowel == true)
            {
                triggerContainer = true;
                transform.SetParent(collision.transform.parent);
            }
            else
            {
                triggerTrowel = true;
            }
        }
        
        if (collision.gameObject.layer == 9)
        {
            rb.velocity = Vector3.zero;
            if (!triggerContainer)
            {
                triggerTopGround = true;
            }
        }

    }
}
