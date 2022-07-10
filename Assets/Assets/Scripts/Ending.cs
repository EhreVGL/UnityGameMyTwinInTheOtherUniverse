using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour
{
    [HideInInspector] public int goalCounter;
    [SerializeField] private RawImage[] frontStars;
    [SerializeField] private RawImage endFrame;
    private Color color;
    private void Awake()
    {
        goalCounter = 0;
        color = new Color(0, 0, 0, 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        endFrame.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(goalCounter == 1)
        {
            frontStars[0].enabled = true;
        }
        else if (goalCounter == 2)
        {
            frontStars[1].enabled = true;
        }
        else if (goalCounter == 3)
        {
            goalCounter++;
            frontStars[2].enabled = true;
            Debug.Log("Finished Level!");
            endFrame.enabled = true;
            StartCoroutine(NextLevel());

        }
    }

    IEnumerator NextLevel()
    {
        while (true)
        {
            color.a += 0.05f;
            endFrame.color = color;
            yield return new WaitForSeconds(0.1f);

            if (color.a == 1)
            {
                //NextLevel
                yield break;
            }
        }
    }
}
