using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CameraPhaseController : MonoBehaviour
{
    [HideInInspector] public bool transformPlayer;
    [HideInInspector] public bool transformFPS;
    Sequence transformPlayerSeq;
    [SerializeField] private GameObject container;



    private void Awake()
    {
        transformPlayer = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        DOTween.Init();
    }

    // Update is called once per frame
    void Update()
    {
        if (transformPlayer)
        {
            transformPlayer = false;
            transformPlayerSeq = DOTween.Sequence();
            transformPlayerSeq.Append(transform.DORotate(new Vector3(-127, 0, 0), 5)).OnComplete(() => {container.SetActive(true);});
            transformPlayerSeq.Join(transform.DOMove(new Vector3(0.11f, 3.9f, 142.5f), 5));
        }
        if (transformFPS)
        {
            transformFPS = false;
            transformPlayerSeq = DOTween.Sequence();
            transformPlayerSeq.Append(transform.DORotate(new Vector3(-180, 0, 0), 3)).OnComplete(() => { Debug.Log("Þimdi Basket Zamaný"); });
            transformPlayerSeq.Join(transform.DOMove(new Vector3(0f, 6f, -18f), 5));

        }
    }
}
