using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] private GameObject platform;
    [SerializeField] private Transform[] targetPos;

    void Start()
    {
        platform.transform.position = targetPos[0].position;       
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
