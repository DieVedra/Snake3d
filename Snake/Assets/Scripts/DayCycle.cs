using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class DayCycle : MonoBehaviour
{
    [Range(1, 0)] public float TimeOfDay;
    private float _timeStartDay = 60f;
    private float _timeEndDay = 210f;
    [SerializeField] private float _startMonster = 0.22f;
    public float StartMonster => _startMonster;

    [SerializeField] private Image _dayIndicatorUI;
    [SerializeField] private AnimationCurve _sunCurve;
    [SerializeField] private Light _sun;
    [SerializeField] private GameObject _monster;

    [SerializeField] private Gradient _gradientAmbient;
    [SerializeField] private Gradient _gradientFog;
    [SerializeField] private Gradient _gradientDirectionalLight;

    [SerializeField] private GameData _gameData;
    [SerializeField] private Transform _transformSun;
    //[SerializeField] private Score _score;

    public UnityAction<bool> TheDayIsOver;
    //public UnityAction<bool> StartMonster;

    private float _sunIntensity;
    private void Start()
    {
        _sun.transform.rotation = Quaternion.Euler(_timeStartDay, 0f, 0f);
        _sunIntensity = _sun.intensity;
    }
    private void Update()
    {
        if (TimeOfDay >= 0f)
        {
            TimeOfDay -= Time.deltaTime / _gameData.DurationOfDays[_gameData.LastCurrentDay - 1];
            _dayIndicatorUI.fillAmount = TimeOfDay;

            _sun.color = _gradientDirectionalLight.Evaluate(_sunCurve.Evaluate(TimeOfDay));  

            if (TimeOfDay < 0.4f && TimeOfDay > 0.2f) // плавное затемнение перед уходом солнца за горизонт
            {
                _sun.intensity = Mathf.Lerp(0f, _sunIntensity, _sunCurve.Evaluate(TimeOfDay) - 0.2f );  // множитель 0,2f для корректировки
            }

            RenderSettings.ambientLight = _gradientAmbient.Evaluate(_sunCurve.Evaluate(TimeOfDay));

            RenderSettings.fogColor = _gradientFog.Evaluate(TimeOfDay);

            _transformSun.localRotation = Quaternion.Euler(Mathf.Lerp(_timeEndDay, _timeStartDay, TimeOfDay), 0, 0);  //60 - 210

            if (TimeOfDay < _startMonster)
            {
                _monster.gameObject.SetActive(true);
            }
            //if (StartMonster < )
            //{

                //}
                //if (TimeOfDay <= 0f)
                //{
                //    TheDayIsOver?.Invoke(true);
                //}
        }
    }
}
