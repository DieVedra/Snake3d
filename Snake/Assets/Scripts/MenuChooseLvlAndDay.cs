using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuChooseLvlAndDay : MonoBehaviour
{
    [SerializeField] private Button[] _butStartLvls;
    [SerializeField] private Button _butChooseLvlInMenu;
    [SerializeField] private Button[] _buttonsDays;
    [SerializeField] private GameObject _panelChoiceLvl;
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private GameData _gameData;
    private void Start()
    {
        CheckInteractiveButton(_buttonsDays, _gameData.MaxOpenDay);
        CheckInteractiveButton(_butStartLvls, _gameData.MaxOpenLvlSceneIndex);
    }

    private void CheckInteractiveButton(Button[] buttons, int value) //проверяет какие кнопки сделать активными по данным хранилища
    {
        for (int i = 0; i < buttons.Length; i++)
        {
            if (value > i)
            {
                buttons[i].interactable = true;
            }
            else
            {
                buttons[i].interactable = false;
            }
        }
    }

    public void ResetData()
    {
        _gameData.MaxOpenDay = 1;
        _gameData.FullScore = 0;
        _gameData.DifficultyLevelOfTheDay = 0;
        _gameData.LastSceneIndex = 1;
        _gameData.LastCurrentDay = 1;
        _gameData.MaxOpenLvlSceneIndex = 1;

        CheckInteractiveButton(_buttonsDays, _gameData.MaxOpenDay);
        CheckInteractiveButton(_butStartLvls, _gameData.MaxOpenLvlSceneIndex);
    }
    public void LevelLoaderFromMenuChoiceLvl(int number)
    {
        if (number == 1)
        {
            _gameData.LastCurrentDay = 1;
            LevelNumberToLoad(number);
        }
        else if (number == 2)
        {
            _gameData.LastCurrentDay = 6;
            LevelNumberToLoad(number);
        }
    }
    public void LevelNumberToLoad(int numberLevel) // начинает загрузку сцены из панели выбора уровня после нажати кнопки
    {
        _gameData.LastSceneIndex = numberLevel/*SceneManager.GetActiveScene().buildIndex*/;

        if (numberLevel > _gameData.MaxOpenLvlSceneIndex)
        {
            _gameData.MaxOpenLvlSceneIndex = numberLevel;
        }

        _levelLoader.LoadGame(numberLevel);
        ButtonBackToMainMenu();
    }
    public void DayNumberToload(int numberDay) // загружает сцены от выбранного дня.   numberDay это индекс для массива дней, то есть с 0 начинается
    {
        if (numberDay <= 4)
        {
            _gameData.LastCurrentDay = numberDay + 1;
            LevelNumberToLoad(1);
        }

        if (numberDay > 4 && numberDay <= 9)
        {
            _gameData.LastCurrentDay = numberDay + 1;
            LevelNumberToLoad(2);
        }

        if (numberDay > 9  && numberDay <= 14)
        {
            _gameData.LastCurrentDay = numberDay + 1;
            LevelNumberToLoad(3);
        }
        //LevelNumberToLoad(numberLevel);
    }
    public void ButtonBackToMainMenu() //выключает панель выбора уровня
    {
        _panelChoiceLvl.SetActive(false);
    }

    public void ButtonChooseLvl() //включает панель выбора уровня
    {
        _panelChoiceLvl.SetActive(true);
    }
}
