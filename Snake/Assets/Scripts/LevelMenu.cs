using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Events;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    [SerializeField] private GameObject _pauseMenu;
    [SerializeField] private MonsterMove _monsterMove;
    //[SerializeField] private ScoreSaveData _scoreSaveData;
    [SerializeField] private Text _textMaxScoreInPauseMenu;
    //[SerializeField] private Score _score;
    [SerializeField] private Button _buttonResume;
    [SerializeField] private Text _dietext;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private DayCycle _dayCycle;
    [SerializeField] private Score _score;

    public void PauseGame(bool gameOver)
    {
        _pauseMenu.SetActive(true);

        if (gameOver)
        {
            _buttonResume.gameObject.SetActive(false);
            _dietext.gameObject.SetActive(true);
        }
        //_textMaxScoreInPauseMenu.text = "Max Score: " + _score.MaxScore;
        FoodSpawner.DoSpawnFood = false;
        Time.timeScale = 0.0f;

        //_scoreSaveData.SaveScoreJson();
    }

    public void ResumeGame()
    {
        _pauseMenu.SetActive(false);
        FoodSpawner.DoSpawnFood = true;

        Time.timeScale = 1f;
    }
    public void RestartGame()
    {
        _levelLoader.LoadGame(SceneManager.GetActiveScene().buildIndex);
        SnakeController.MoveForward = false;
        FoodSpawner.DoSpawnFood = false;

        //SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        Time.timeScale = 1f;
    }

    //public void ExitGame()
    //{
    //    Application.Quit();
    //}

    //private void CheckOverGame(bool gameOver)
    //{
    //    if (!_score.CheckOfComplianceScore())
    //    {
    //        PauseGame(gameOver);
    //    }
    //}
    private void OnEnable()
    {
        //_detectorCollision = GameObject.Find(SnakeSpawner.NameSpawnedSnake).GetComponent<DetectorCollision>();
        //_monsterMove = FindObjectOfType<DetectorCollision>();

        _monsterMove.GameOver += PauseGame;
        //_dayCycle.TheDayIsOver += CheckOverGame;
    }

    private void OnDisable()
    {
        _monsterMove.GameOver -= PauseGame;
        //_dayCycle.TheDayIsOver += CheckOverGame;
    }
}
