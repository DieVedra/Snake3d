using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EaterSegments : MonoBehaviour
{
    [SerializeField] private DetectorCollision _detectorCollision;
    [SerializeField] private SnakeController _snakeController;
    //[SerializeField] private Score _score;

    public UnityAction<int> DecreaseScore;

    private void Awake()
    {
        _detectorCollision = FindObjectOfType<DetectorCollision>();
        _snakeController = FindObjectOfType<SnakeController>();
    }
    private void Start()
    {
        //_detectorCollision = FindObjectOfType<DetectorCollision>();
        //_snakeController = FindObjectOfType<SnakeController>();
        //_score = FindObjectOfType<Score>();

        _detectorCollision.EatSegments += DeleterSegments;
    }

    private void DeleterSegments(int numberSegment)
    {
        var segments = _snakeController.GetSegmentsCollection(); // ссылка на список сегментов

        for (int i = segments.Count - 1; i >= numberSegment - 1; i--)
        {
            Destroy(segments[i].gameObject);
            segments.RemoveAt(i);
            DecreaseScore?.Invoke(-1);
        }

    }
    private void OnDisable()
    {
        _detectorCollision.EatSegments -= DeleterSegments;
    }
}
