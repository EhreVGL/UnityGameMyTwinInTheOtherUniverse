using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Blocks : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.layer == 7)
        {
            Debug.Log("Collision");
            collision.gameObject.GetComponent<CharacterMovement>().enabled = false;
            SaveLevel.singleton.ResetLevel();
        }
    }
    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.layer == 7)
        {
            collision.gameObject.GetComponent<CharacterMovement>().enabled = false;
            SaveLevel.singleton.ResetLevel();
        }
    }
}
