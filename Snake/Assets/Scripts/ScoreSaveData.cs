using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ScoreSaveData : MonoBehaviour
{
    [SerializeField] private Score _score;
    [SerializeField] private Save _save;

    private string _path;

    void Start()
    {
#if UNITY_ANDROID && !UNITY_EDITOR
        _path = Path.Combine(Application.persistentDataPath, "Save.json");
#else
        _path = Path.Combine(Application.dataPath, "Save.json");
#endif
    }

    private void OnApplicationPause(bool pause)
    {
        if (pause)
        {
            SaveScoreJson();
        }
    }
    public void SaveScoreJson()
    {
        //_save.MaxScore = _score.MaxScore;
        File.WriteAllText(_path, JsonUtility.ToJson(_save));
    }
}
