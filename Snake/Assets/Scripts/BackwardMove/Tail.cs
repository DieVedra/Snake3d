using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(WaySnake))]
public class Tail : MonoBehaviour
{
    [SerializeField] public List<GameObject> _listOfMovingBackwards;

    [SerializeField] private SnakeController _snakeController;
    [SerializeField] private WaySnake _waySnake;


    [SerializeField] private float _pointToPointDelayTime;
    private int _movementCount;
    [SerializeField] private int _movementCountMaxValue;

    public bool Key = true;
    private void Awake()
    {
        _waySnake = GetComponent<WaySnake>();
    }
    private void Start()
    {
        _snakeController = FindObjectOfType<SnakeController>();
        _snakeController.TailMoveActionForward += MoveTail;
    }
    private void MoveTail(Vector3 pos, Quaternion rot, float sqrDistance) // передвижение обьекта "Хвост" вперед или назад
    {
        Vector3 positionOfTheLastPoint = pos;
        Quaternion rotationOfTheLastPoint = rot;

        if ((transform.position - positionOfTheLastPoint).sqrMagnitude > sqrDistance && SnakeController.MoveForward)
        {
            _waySnake.SetPositionAndRotationOfWay(transform.position, transform.rotation);
            
            transform.position = positionOfTheLastPoint;

            transform.rotation = rotationOfTheLastPoint;
        }
        else if (!SnakeController.MoveForward)
        {
            var a = transform.position;

            var b = transform.rotation;

            transform.position = positionOfTheLastPoint;

            transform.rotation = rotationOfTheLastPoint;

            _snakeController.MoveSegments(_listOfMovingBackwards, a, b);
        }
    }

    public void FillingInTheListOfMovingBackwards()
    {
        _listOfMovingBackwards.Clear();
        var segments = _snakeController.GetSegmentsCollection();

        if (segments.Count > 0)
        {
            for (int i = segments.Count - 1; i >= 0; i--)
            {
                _listOfMovingBackwards.Add(segments[i]);
            }
        }

        _listOfMovingBackwards.Add(_snakeController.gameObject);
    }
    public void MoveSnakeBackwards()
    {
        if (Key)
        {
            StartCoroutine(TailMoveBackward(_pointToPointDelayTime));
        }
    }

    private IEnumerator TailMoveBackward(float timeDelay)
    {
        Key = false;

        yield return new WaitForSeconds(timeDelay);

        MoveTail(_waySnake.GetPositionOfWay(), _waySnake.GetRotationOfWay(), 0f);  // параметр 0f просто что бы вызвать метод

        _movementCount++;

        yield return new WaitForSeconds(0.1f);

        if (_movementCount == _movementCountMaxValue)
        {
            SnakeController.MoveForward = true;
            DetectorCollision.CheckCollision = true;
            _movementCount = 0;
        }

        Key = true;
    }

    public void StartCoroutineDelayOfTheCollisionWithTheWall(float delayOfTheCollisionWithTheWallTime)
    {
        StartCoroutine(TailMoveBackward(delayOfTheCollisionWithTheWallTime));
    }
    private void OnDisable()
    {
        _snakeController.TailMoveActionForward -= MoveTail;
    }
}
