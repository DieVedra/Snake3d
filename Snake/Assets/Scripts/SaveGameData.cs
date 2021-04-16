using UnityEngine;
using System.IO;

public class SaveGameData : MonoBehaviour
{
    static private SaveGameData _instance = null;

    [SerializeField] private GameData _gameData;
    private SaveToJson _saveToJson = new SaveToJson();
    private string _path;

    private void Awake()
    {
        DontDestroyOnLoad(gameObject);

        if (_instance == null)
        {
            _instance = this;
        }
        else if (gameObject != _instance)
        {
            Destroy(gameObject);
        }

        LoadDataInScriptableObject();
    }
    private void OnApplicationPause(bool pause)
    {
        SaveDataFromScriptableObject();
    }
    private void OnApplicationQuit()
    {
        SaveDataFromScriptableObject();
    }
    private void SaveDataFromScriptableObject()
    {
        _saveToJson.LastCurrentDay = _gameData.LastCurrentDay;
        _saveToJson.MaxOpenLvlSceneIndex = _gameData.MaxOpenLvlSceneIndex;
        _saveToJson.FullScore = _gameData.FullScore;
        _saveToJson.DifficultyLevelOfTheDay = _gameData.DifficultyLevelOfTheDay;
        _saveToJson.LastCurrentDay = _gameData.LastCurrentDay;
        _saveToJson.MaxOpenDay = _gameData.MaxOpenDay;

        File.WriteAllText(_path, JsonUtility.ToJson(_saveToJson));
    }
    private void LoadDataInScriptableObject()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "SaveGameData.json");
#else
        _path = Path.Combine(Application.dataPath, "SaveGameData.json");
#endif
        if (File.Exists(_path))
        {
            _saveToJson = JsonUtility.FromJson<SaveToJson>(File.ReadAllText(_path));

            _gameData.LastCurrentDay = _saveToJson.LastCurrentDay;
            _gameData.MaxOpenLvlSceneIndex = _saveToJson.MaxOpenLvlSceneIndex;
            _gameData.FullScore = _saveToJson.FullScore;
            _gameData.DifficultyLevelOfTheDay = _saveToJson.DifficultyLevelOfTheDay;
            _gameData.LastCurrentDay = _saveToJson.LastCurrentDay;
            _gameData.MaxOpenDay = _saveToJson.MaxOpenDay;
        }
    }
}

[System.Serializable]
public class SaveToJson
{
    public int LastSceneIndex;
    public int MaxOpenLvlSceneIndex;
    public int FullScore;
    public float DifficultyLevelOfTheDay;
    public int LastCurrentDay;
    public int MaxOpenDay;
}
