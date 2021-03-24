using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DawnAndSunsetLvl : MonoBehaviour
{
    [SerializeField] private Animator _animator;

    private void Start()
    {
        _animator.Play("DawnLevel");
    }

    public void SunsetLevel()
    {
        _animator.Play("SunsetLevel");
    }
}
