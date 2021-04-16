using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;
using UnityEngine.UI;

public class NextLevelHandler : MonoBehaviour
{
    private DetectorCollision _detectorCollision;
    private Score _score;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private GameData _gameData;
    [SerializeField] private CinemachineVirtualCamera _cmPursuitSnake;
    [SerializeField] private CinemachineVirtualCamera _cmExitLvl;
    [SerializeField] private Button _buttonNextDay;
    [SerializeField] private Button _buttonResume;
    [SerializeField] private Text _endGameText;
    [SerializeField] private GameObject _panelPauseMenu;
    //[SerializeField] private Light _pointLight;
    [SerializeField] private float _timeWait;
    [SerializeField] private float _difficultUp;
    private void Awake()
    {
        _detectorCollision = FindObjectOfType<DetectorCollision>();
        _score = FindObjectOfType<Score>();
    }
    private void Start()
    {
        //    _detectorCollision = FindObjectOfType<DetectorCollision>();
        //    _score = FindObjectOfType<Score>();

        _detectorCollision.TryToExitTheLevel += CheckExitTheLevel;
        _buttonNextDay.onClick.AddListener(NextDay);
    }

    private void CheckExitTheLevel()
    {
        if (_score.ScoreValue >= _gameData.ScoreToCompleteTheDay[_gameData.LastCurrentDay - 1])
        {
            StartCoroutine(ExitTheLevel());
        }
        else
        {
            _detectorCollision.CollisionObstacleForest();
        }
    }


    //private void EnableExitLight()
    //{

    //}
    private IEnumerator ExitTheLevel()
    {
        _cmPursuitSnake.gameObject.SetActive(false);
        _cmExitLvl.gameObject.SetActive(true);

        DetectorCollision.CheckCollision = false;
        FoodSpawner.DoSpawnFood = false;

        if (_gameData.DurationOfDays.Length != _gameData.LastCurrentDay)
        {
            _buttonNextDay.gameObject.SetActive(true);
        }
        else
        {
            _buttonNextDay.gameObject.SetActive(false);
            _endGameText.gameObject.SetActive(true);
        }

        yield return new WaitForSeconds(_timeWait);

        _panelPauseMenu.SetActive(true);
        _buttonResume.gameObject.SetActive(false);

        MonsterMove.IsMove = false;
        //_buttonNextDay.gameObject.SetActive(true);

    }
    private void NextDay()
    {
         if (_gameData.LastCurrentDay < 10)
         {
             _gameData.LastCurrentDay++;
         }

         _gameData.DifficultyLevelOfTheDay += _difficultUp;
            
            
         if (_gameData.LastCurrentDay == 6 )
         {
            _gameData.MaxOpenLvlSceneIndex = 2;
         }
         if (_gameData.MaxOpenDay < _gameData.LastCurrentDay)
         {
             _gameData.MaxOpenDay = _gameData.LastCurrentDay;
         }
         //_buttonNextDay.gameObject.SetActive(true);

        
         _levelLoader.LoadGame(_gameData.MaxOpenLvlSceneIndex);
        
    }
    private void OnDisable()
    {
        _detectorCollision.TryToExitTheLevel -= CheckExitTheLevel;
        _buttonNextDay.onClick.RemoveListener(NextDay);
    }
}
