using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(EaterSegments))]
public class DetectorCollision : MonoBehaviour
{
    [SerializeField] private float _maxDistance;
    [SerializeField] private float _radiusSphereCast;
    private bool _gameOver = false;
    private RaycastHit _hit;
    private SnakeController _snakeController;
    private Tail _tail;
    private Transform _transform;
    //private Score _score;

    //public UnityAction<bool> GameOver;
    public UnityAction<int> OnEat;
    public UnityAction<int> EatSegments;
    public UnityAction HittingAnObstacle;
    public UnityAction TryToExitTheLevel;

    [SerializeField] private LayerMask _layerMaskForest;
    [SerializeField] private LayerMask _layerMaskFood;
    [SerializeField] private LayerMask _layerMaskSegment;
    [SerializeField] private LayerMask _layerMaskExitLvl;
    [SerializeField] private LayerMask _layerMaskMonster;
    //[SerializeField] private LayerMask _layerMaskCast;

    [HideInInspector] static public bool CheckCollision = true;
    private void Awake()
    {
        _snakeController = GetComponent<SnakeController>();
        _tail = FindObjectOfType<Tail>();
        _transform = GetComponent<Transform>();
        CheckCollision = true;
        //_score = FindObjectOfType<Score>();
    }
    private void FixedUpdate()
    {
        if (CheckCollision) // чтоб только один раз сработало столкновение
        {
            //Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskCast);
            //Physics.SphereCast()

            //if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance/*, _layerMaskCast*/))
            //{
            //    //_hit.collider.gameObject.
            //    Debug.Log(_hit.collider.gameObject.layer);
            //    if (_hit.collider.gameObject.layer == _layerMaskForest.value)
            //    {
            //        CollisionObstacleForest();
            //    }
            //    else if (_hit.collider.gameObject.layer == _layerMaskFood.value)
            //    {
            //        CollisionObstacleFood();
            //    }
            //    else if(_hit.collider.gameObject.layer == _layerMaskSegment.value)
            //    {
            //        CollisionObstacleSegment();
            //    }
            //    else if(_hit.collider.gameObject.layer == _layerMaskExitLvl.value)
            //    {
            //        CollisionObstacleExitLvl();
            //    }
            //    else if(_hit.collider.gameObject.layer == _layerMaskMonster.value)
            //    {
            //        CollisionObstacleMonster();
            //    }
            //}

            if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskForest))
            {
                CollisionObstacleForest();
            }
            CollisionObstacleFood();
            CollisionObstacleSegment();
            CollisionObstacleExitLvl();
            //CollisionObstacleMonster();
        }
    }

    public void CollisionObstacleForest()
    {
        CheckCollision = false;
        SnakeController.MoveForward = false;
        HittingAnObstacle?.Invoke();
        _tail.FillingInTheListOfMovingBackwards();
        _tail.StartCoroutineDelayOfTheCollisionWithTheWall(_snakeController.DelayCollisionWithWallTime);  // передать время 5 например
    }
    private void CollisionObstacleFood()
    {
        if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskFood))
        {
            OnEat?.Invoke(_hit.collider.gameObject.GetComponent<Food>().SegmentCount);
            Destroy(_hit.collider.gameObject);
    }
}
    private void CollisionObstacleSegment()
    {
        if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskSegment))
        {
            EatSegments?.Invoke(_hit.collider.gameObject.GetComponent<Segment>().PersonalSegmentNumber);
            //Debug.Log(_hit.collider.gameObject.GetComponent<Segment>().PersonalSegmentNumber);

        }
    }
    private void CollisionObstacleExitLvl()
    {
        if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskExitLvl))
        {
            TryToExitTheLevel?.Invoke();
    }
}

    //private void CollisionObstacleMonster()
    //{
    //    if (Physics.SphereCast(_transform.position, _radiusSphereCast, _transform.forward, out _hit, _maxDistance, _layerMaskMonster))
    //    {
    //        GameOver?.Invoke(true);
    //    }
    //}
    //private void OnCollisionEnter(Collision collision)
    //{
    //    if (collision.gameObject.GetComponent<MonsterMove>())
    //    {
    //        //Debug.Log(1);
    //        GameOver?.Invoke(true);
    //    }
    //}


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Debug.DrawLine(transform.position, transform.position + transform.forward * _maxDistance);
        Gizmos.DrawWireSphere(transform.position + transform.forward * _maxDistance, _radiusSphereCast);
    }
}
