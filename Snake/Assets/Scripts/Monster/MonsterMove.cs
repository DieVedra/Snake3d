using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.Events;

public class MonsterMove : MonoBehaviour
{
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private CinemachineDollyCart _cinemachineDolly;
    [SerializeField] private Transform _transform;
    [SerializeField] private AnimationCurve _alongPathCurve;
    [SerializeField] private ParticleSystem _shotEffect;
    [SerializeField] private float _speed;
    public UnityAction<bool> GameOver;

    static public bool IsMove = true;


    private float _positionMonster;
    //[SerializeField] private float _startMonster = 0.22f;
    private float _timer;


    private void Start()
    {
        _dayCycle = FindObjectOfType<DayCycle>();
        _cinemachineDolly = GetComponentInParent<CinemachineDollyCart>();
        _snakeController = FindObjectOfType<SnakeController>();
        _transform = GetComponent<Transform>();
    }

    private void MovingAlongPath()
    {
        if (_dayCycle.TimeOfDay < _dayCycle.StartMonster && _dayCycle.TimeOfDay >= 0f)
        {
            _cinemachineDolly.m_Position = Mathf.Lerp(0, 7, _alongPathCurve.Evaluate(_dayCycle.TimeOfDay));
        }
        else return;
        //_cinemachineDolly.m_Position = _positionMonster;
    }
    private void ChaseSnake()
    {
        if (_dayCycle.TimeOfDay <= 0f && IsMove)
        {
            _transform.position = Vector3.MoveTowards(_transform.position, Vector3.Lerp(_transform.position, _snakeController.transform.position, 0.1f), _speed);
            _transform.LookAt(_snakeController.transform.position);
        }
        else return;
    }

    private void FixedUpdate()
    {
        MovingAlongPath();

        

        ChaseSnake();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.GetComponent<SnakeController>())
        {
            StartCoroutine(CollisionGameOver());
        }
    }
    private IEnumerator CollisionGameOver()
    {
        IsMove = false;
        _shotEffect.Play();
        SnakeController.MoveForward = false;
        yield return new WaitForSeconds(0.5f);

        GameOver?.Invoke(true);
    }
}
