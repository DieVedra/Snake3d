using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelLoader : MonoBehaviour
{
    //[SerializeField] private Button _buttonRun;
    [SerializeField] private GameData _gameData;
    [SerializeField] private DawnAndSunsetLvl _dawnAndSunsetLvl;
    [SerializeField] private Image _image;
    [SerializeField] private Slider _slider;
    [SerializeField] private Text _text;

    public void RunGame()
    {
        LoadGame(_gameData.LastSceneIndex);
    }
    public void LoadGame(int index)
    {
        StartCoroutine(LoadLevelAcyng(index));
    }
    public void LoadMainMenu()
    {
        SnakeController.MoveForward = false;
        StartCoroutine(LoadLevelAcyng(0));
        Time.timeScale = 1f;
    }

    private IEnumerator LoadLevelAcyng(int SceneIndex)
    {
        _dawnAndSunsetLvl.SunsetLevel();
        yield return new WaitForSeconds(1f);

        _image.gameObject.SetActive(true);
        //_startAnim

        AsyncOperation asyngLoad = SceneManager.LoadSceneAsync(SceneIndex);

        asyngLoad.allowSceneActivation = false;

        while (!asyngLoad.isDone)
        {
            //image.fillAmount = asyngLoad.progress;
            _slider.value = asyngLoad.progress;
            _text.text = asyngLoad.progress * 100f + "%";
            if (asyngLoad.progress >= .9f && !asyngLoad.allowSceneActivation /*&& nextScene*/)
            {
                asyngLoad.allowSceneActivation = true;
            }

            yield return 0;
        }
    }
    //private void OnEnable()
    //{
    //    _buttonRun.onClick.AddListener(RunGame);
    //}
    //private void OnDisable()
    //{
    //    _buttonRun.onClick.RemoveListener(RunGame);
    //}
}
