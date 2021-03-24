using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class SignalSystemUI : MonoBehaviour
{
    [SerializeField] private Animator _animator;
    [SerializeField] private DetectorCollision _detectorCollision;
    [SerializeField] private FoodSpawner _foodSpawner;
    [SerializeField] private AudioClip _eatFoodClip;
    [SerializeField] private AudioClip _eatSegmentsClip; 
    [SerializeField] private AudioClip _hittingAnObstacleClip;

    private AudioSource _audioSource;

    private void Awake()
    {
        _audioSource = GetComponent<AudioSource>();
    }

    private void Start()
    {
        //    _detectorCollision = FindObjectOfType<DetectorCollision>();

        //_animator.Play("DawnLevel");
    }
    private void PlayEatSignals(int a)
    {
        _audioSource.clip = _eatFoodClip;
        _audioSource.Play();
        _animator.Play("Eat_Panel_Anim");


        //DoSpawnFood();
    }

    private void PlayEatSegments(int a) // просто должен что то принимать
    {
        _audioSource.clip = _eatSegmentsClip;
        _audioSource.Play();
        _animator.Play("Dead_Panel_Anim");
    }

    private void PlayHittingAnObstacle()
    {
        _audioSource.clip = _hittingAnObstacleClip;
        _audioSource.Play();
        _animator.Play("Dead_Panel_Anim");
    }
    //private void DoSpawnFood()
    //{
    //    _foodSpawner.DoSpawn();
    //}

    private void OnEnable()
    {
        _detectorCollision = FindObjectOfType<DetectorCollision>();

        _detectorCollision.OnEat += PlayEatSignals;
        _detectorCollision.EatSegments += PlayEatSegments;
        _detectorCollision.HittingAnObstacle += PlayHittingAnObstacle;

    }
    private void OnDisable()
    {
        _detectorCollision.OnEat -= PlayEatSignals;
        _detectorCollision.EatSegments -= PlayEatSegments;
        _detectorCollision.HittingAnObstacle -= PlayHittingAnObstacle;
    }
}
