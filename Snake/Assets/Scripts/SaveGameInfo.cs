using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class SaveGameInfo : MonoBehaviour
{
    [SerializeField] private  GameObject _nameEnterPanel;
    [SerializeField] private Button _buttonReEnterName;
    [SerializeField] private Button _buttonRunGame;
    [SerializeField] private Text _textName;
    //[SerializeField] private Text _textMaxScore;
    [SerializeField] private Save _save;
    private Item _item = new Item();
    public string _path { get; private set; }

    private void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Save.json");
        //_pathToSaveMaxScore = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        _path = Path.Combine(Application.dataPath, "Save.json");
#endif
        if (File.Exists(_path))
        {
            _item = JsonUtility.FromJson<Item>(File.ReadAllText(_path));

            _textName.text = _item.Name;
            Debug.Log("Привет: " + _item.Name);
            //_textMaxScore.text = "Max Score: \n" + _item.MaxScore + " ";
            Debug.Log("\nТвой рекорд: " + _item.MaxScore);
        }
        else 
        {
            _nameEnterPanel.SetActive(true);  //панель для ввода имени и возраста если отсутствует файл сохранения
            
        }   
    }
    private void OnEnable()
    {
        _buttonReEnterName.onClick.AddListener(ReEnterName);
        _buttonRunGame.onClick.AddListener(SaveData);
    }
    private void OnDisable()
    {
        _buttonReEnterName.onClick.RemoveListener(ReEnterName);
        _buttonRunGame.onClick.RemoveListener(SaveData);
    }
    private void ReEnterName()
    {
        _textName.text = null;
        _nameEnterPanel.SetActive(true);
    }
    public void CheckName(string name)
    {
        if (!string.IsNullOrEmpty(name) && name.Length >= 3)
        {
            _item.Name = name;
            _textName.text = name;
            Debug.Log("Ваше имя: " + name);
            _nameEnterPanel.SetActive(false);
        }
        else
        {
            Debug.Log("Введите нормальное имя!");
        }
    }
    //public void CheckAge(string age)
    //{
    //    if (!string.IsNullOrEmpty(age) && age.Length > 0)
    //    {
    //        _save.age = int.Parse(age);
    //        Debug.Log("Ваш возраст: " + age);
    //    }
    //    else Debug.Log("Введите нормальный возраст!");
    //}
//#if UNITY_ANDROID && !UNITY_EDITOR
    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveData();
        }
    }
//#endif
    private void OnApplicationQuit()
    {
        SaveData();
    }
    private void SaveData()
    {
        _save.MaxScore = _item.MaxScore;
        _save.Name = _item.Name;
        File.WriteAllText(_path, JsonUtility.ToJson(_save));
    }
}

[Serialize]
public class Item 
{
    public string Name;
    public int MaxScore;
}
//[Serialize]
//public class ScoreMax
//{
//    public int MaxScore;
//}
