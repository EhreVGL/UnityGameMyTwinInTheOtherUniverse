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
    [SerializeField] private GameObject light;


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
            transformPlayerSeq.Join(light.transform.DORotate(new Vector3(-146.654f, 0, 0), 5));
        }
        if (transformFPS)
        {
            transformFPS = false;
            transformPlayerSeq = DOTween.Sequence();
            transformPlayerSeq.Append(transform.DORotate(new Vector3(0, 180, 0), 3));
            transformPlayerSeq.Join(transform.DOMove(new Vector3(0f, 8f, -18f), 5));

        }
    }
}
