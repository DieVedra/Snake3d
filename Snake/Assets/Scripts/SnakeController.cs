using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

[RequireComponent(typeof(DetectorCollision))]
public class SnakeController : MonoBehaviour
{
    [SerializeField] private List<GameObject> _segments;
    
    [SerializeField] private GameObject _segmentPrefab;

    [SerializeField] private float _minSpeed;
    [SerializeField] private float _maxSpeed;
    private float _speed;
    private float _angle;
    [SerializeField] private float _distanceBetweenSegments;

    public float DelayCollisionWithWallTime;
    private float _sqrDistance;

    private Joystick _joystick;
    private Slider _sliderSpeed;

    static public bool MoveForward;


    [SerializeField] private float _angleTurnJoystick;
    [SerializeField] private float _angleTurnButtons;

    private SwitchControll _switchControl;
    [SerializeField] private Tail _tail;
    [SerializeField] private DetectorCollision _detectorCollision;


    //public delegate void ScoreUpdate(int value);
    //public event ScoreUpdate ScoreUpdateAction;

    public UnityAction<Vector3, Quaternion, float> TailMoveActionForward;
    private void Awake()
    {
        _sliderSpeed = GameObject.Find("SliderSpeed").GetComponent<Slider>();
        _joystick = GameObject.Find("Control").GetComponentInChildren<Joystick>();
        _switchControl = GameObject.Find("Control").GetComponent<SwitchControll>();
        _tail = FindObjectOfType<Tail>();
        _detectorCollision = FindObjectOfType<DetectorCollision>();

        _sqrDistance = _distanceBetweenSegments * _distanceBetweenSegments;
        MoveForward = true;
    }
    private void Start()
    {
        _sliderSpeed.minValue = _minSpeed;
        _sliderSpeed.maxValue = _maxSpeed;
        _speed = _minSpeed;
    }

    private void Update()
    {
        if (MoveForward)
        {
            MoveHead(transform.position + transform.forward * _speed * Time.deltaTime);
        }
        else
        {
            _tail.MoveSnakeBackwards();
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            CreateSegment(1);
        }
    }

    private void MoveHead(Vector3 newPosition)
    {
        transform.position = newPosition;

        if (!_switchControl.Switched)
        {
            _angle = _joystick.Horizontal * _angleTurnJoystick * _sliderSpeed.value /** -1f*/;
        }

        transform.Rotate(0f, _angle, 0f);

        MoveSegments(_segments, transform.position - transform.forward * 0.2f, transform.rotation);

        if (_segments.Count == 0)
        {
            TailMoveActionForward?.Invoke(transform.position - transform.forward * 0.2f, transform.rotation, _sqrDistance);
        }
    }
    public void MoveSegments(List<GameObject> givenSegments, Vector3 position, Quaternion rotation)
    {
        Vector3 previosPosition = position;
        Quaternion previosRotation = rotation;

        for (int i = 0; i < givenSegments.Count; i++)
        {
            if ((givenSegments[i].transform.position - previosPosition).sqrMagnitude > _sqrDistance || !MoveForward)
            {
                var pos = givenSegments[i].transform.position;
                givenSegments[i].transform.position = previosPosition;
                previosPosition = pos;

                var rot = givenSegments[i].transform.rotation;
                givenSegments[i].transform.rotation = previosRotation;
                previosRotation = rot;

                if (i == givenSegments.Count - 1 && MoveForward)
                {
                    TailMoveActionForward?.Invoke(previosPosition, previosRotation, _sqrDistance);
                }
            }
            else
            {
                break;
            }
        }
    }
    public void CreateSegment(int segmentCreateCount)
    {
        for (int i = 0; i < segmentCreateCount; i++)
        {
            Vector3 pointCreatePosition = transform.position - transform.forward * 0.2f;
            Quaternion pointCreateRotation = transform.rotation;

            if (_segments.Count > 0)
            {
                pointCreatePosition = _segments[_segments.Count - 1].transform.position;
                pointCreateRotation = _segments[_segments.Count - 1].transform.rotation;
            }

            var segment = Instantiate(_segmentPrefab, pointCreatePosition, pointCreateRotation);

            _segments.Add(segment);

            segment.GetComponent<Segment>().SetPersonalSegmentNumber(_segments.Count);

        }

        //ScoreUpdateAction?.Invoke(segmentCreateCount); // нахуя
    }

    public List<GameObject> GetSegmentsCollection()
    {
        return _segments;
    }
    public void ButtonTurnControl(bool rightDirection)
    {
        if (rightDirection)
        {
            _angle = -1 * _angleTurnButtons * _sliderSpeed.value;
        }
        else if (!rightDirection)
        {
            _angle = _angleTurnButtons * _sliderSpeed.value;
        }
    }
    public void ButtonStopTurn()
    {
        _angle = 0;
    }
    public void SliderSpeed(float newSpeed)
    {
        _speed = newSpeed;
    }
    private void OnEnable()
    {
        _sliderSpeed.onValueChanged.AddListener(SliderSpeed);
        _detectorCollision.OnEat += CreateSegment;
    }
    private void OnDisable()
    {
        _sliderSpeed.onValueChanged.RemoveListener(SliderSpeed);
        _detectorCollision.OnEat -= CreateSegment;
    }
}
