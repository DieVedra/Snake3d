using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineSubscription : MonoBehaviour
{
    private CinemachineVirtualCamera _virtualCamera1;
    private void Start()
    {
        _virtualCamera1 = GetComponent<CinemachineVirtualCamera>();
        _virtualCamera1.m_Follow = FindObjectOfType<SnakeController>().transform;
        _virtualCamera1.m_LookAt = GameObject.Find("Target").transform;
    }
}
