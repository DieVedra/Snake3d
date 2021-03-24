using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartMenu : MonoBehaviour
{
    [SerializeField] private Button _buttonRun;
    private void OnEnable()
    {
        _buttonRun.onClick.AddListener(LoadGame);
    }
    private void OnDisable()
    {
        _buttonRun.onClick.RemoveListener(LoadGame);
    }
    public void LoadGame()
    {
        SceneManager.LoadScene(1);
    }
}
