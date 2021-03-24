using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    [SerializeField] private Text _scoreText;
    //[SerializeField] private Text _scoreToDay;

    [SerializeField] private GameData _gameData;

    public int ScoreValue { get; private set; }
    //public int MaxScore { get; private set; }

    [SerializeField] private DetectorCollision _detectorCollision;
    [SerializeField] private EaterSegments _eaterSegments;
    [SerializeField] private Light _pointLight;


    private void Start()
    {
        _detectorCollision = FindObjectOfType<DetectorCollision>();
        _eaterSegments = FindObjectOfType<EaterSegments>();
        _eaterSegments.DecreaseScore += ScoreUpdate;
        _detectorCollision.OnEat += ScoreUpdate;
        ScoreUpdate(ScoreValue);
        ScoreValue = 0;
        _pointLight.gameObject.SetActive(false);
    }
    private void OnDisable()
    {
        _eaterSegments.DecreaseScore -= ScoreUpdate;
        _detectorCollision.OnEat -= ScoreUpdate;
    }

    private void ScoreUpdate(int value)
    {
        ScoreValue += value;
        _gameData.FullScore += value;
        _scoreText.text = "Score: " + ScoreValue + " /" + _gameData.ScoreToCompleteTheDay[_gameData.LastCurrentDay -1];

        TryEnableExitLight();
    }
    private void TryEnableExitLight()
    {
        if (CheckOfComplianceScore())
        {
            _pointLight.gameObject.SetActive(true);
        }
    }

    public bool CheckOfComplianceScore()
    {
        if (ScoreValue >= _gameData.ScoreToCompleteTheDay[_gameData.LastCurrentDay -1 ])
        {
            return true;
        }
        else
        {
            return false;
            Debug.Log(1);
        }
    }
}
