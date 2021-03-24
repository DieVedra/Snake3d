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
    [SerializeField] private LevelLoader _levelLoader;
    [SerializeField] private GameData _gameData;
    private void Start()
    {
        CheckInteractiveButton(_buttonsDays, _gameData.MaxOpenDay);
        CheckInteractiveButton(_butStartLvls, _gameData.MaxOpenLvlSceneIndex);
    }

    private void CheckInteractiveButton(Button[] buttons, int value)
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
    public void LevelNumberToLoad(int numberLevel)
    {
        _gameData.LastSceneIndex = numberLevel/*SceneManager.GetActiveScene().buildIndex*/;

        if (numberLevel > _gameData.MaxOpenLvlSceneIndex)
        {
            _gameData.MaxOpenLvlSceneIndex = numberLevel;
        }

        _levelLoader.LoadGame(numberLevel);
        ButtonBackToMainMenu();
    }
    public void DayNumberToload(int numberDay) // numberDay это индекс
    {
        if (numberDay <= 4)
        {
            _gameData.LastCurrentDay = numberDay - 1;
            LevelNumberToLoad(1);
        }

        if (numberDay > 4 && numberDay <= 9)
        {
            _gameData.LastCurrentDay = numberDay - 1;
            LevelNumberToLoad(2);
        }

        if (numberDay > 9  && numberDay <= 14)
        {
            _gameData.LastCurrentDay = numberDay - 1;
            LevelNumberToLoad(3);
        }
        //LevelNumberToLoad(numberLevel);
    }
    public void ButtonBackToMainMenu()
    {
        gameObject.SetActive(false);
    }

    public void ButtonChooseLvl()
    {
        gameObject.SetActive(true);
    }
}
