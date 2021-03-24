using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    [SerializeField] private int _boneCount;
    public int SegmentCount { get; private set; }

    private void Awake()
    {
        SegmentCount = _boneCount;
    }
}
