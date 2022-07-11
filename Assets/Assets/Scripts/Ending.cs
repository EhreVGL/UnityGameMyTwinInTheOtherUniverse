using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Ending : MonoBehaviour
{
    [HideInInspector] public int goalCounter;
    [HideInInspector] public bool zeroBall;
    [SerializeField] private RawImage[] frontStars;
    [SerializeField] private RawImage endFrame;
    [SerializeField] private TextMeshProUGUI ballValueText;
    private Color color;
    private SceneChanger changer;
    private bool returningLevel;
    private void Awake()
    {
        changer = GetComponent<SceneChanger>();
        returningLevel = false;
        zeroBall = false;
        goalCounter = 0;
        color = new Color(0, 0, 0, 0);

    }
    // Start is called before the first frame update
    void Start()
    {
        endFrame.enabled = false;
        SaveLevel.singleton.ResetResetLevel();
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
            returningLevel = false;
            goalCounter++;
            frontStars[2].enabled = true;
            endFrame.enabled = true;
            StartCoroutine(NextLevel());

        }

        if (zeroBall == true && returningLevel == false)
        {
            returningLevel = true;
            endFrame.enabled = true;
            StartCoroutine(ReturnLevel());
        }
        if (SaveLevel.singleton.GetResetLevel() >= 1)
        {
            SaveLevel.singleton.SetResetLevel();
            endFrame.enabled = true;
            StartCoroutine(ReturnLevel());
        }
    }

    IEnumerator NextLevel()
    {
        while (true)
        {
            color.a += 0.05f;
            endFrame.color = color;
            yield return new WaitForSeconds(0.1f);

            if (color.a >= 1)
            {
                //NextLevel
                changer.LoadNewLevel();
                SaveLevel.singleton.LevelUp();
                yield break;
            }
        }
    }

    IEnumerator ReturnLevel()
    {
        while (true)
        {
            color.a += 0.05f;
            endFrame.color = color;
            yield return new WaitForSeconds(0.1f);

            if (color.a >= 1)
            {
                //CurrentLevel
                changer.ReturnThisLevel();
                yield break;
            }
        }
    }
}
