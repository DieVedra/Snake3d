using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

//[ExecuteInEditMode]
public class DayCycle : MonoBehaviour
{
    [Range(1, 0)] public float TimeOfDay;
    public float TimeStartDay;
    public float TimeEndDay;

    [SerializeField] private Image _dayIndicatorUI;
    [SerializeField] private AnimationCurve _sunCurve;
    [SerializeField] private Light _sun;

    [SerializeField] private Gradient _gradientAmbient;
    [SerializeField] private Gradient _gradientFog;
    [SerializeField] private Gradient _gradientDirectionalLight;

    [SerializeField] private GameData _gameData;
    //[SerializeField] private Score _score;

    public UnityAction<bool> TheDayIsOver;

    private float _sunIntensity;
    private void Start()
    {
        _sun.transform.rotation = Quaternion.Euler(TimeStartDay, 0f, 0f);
        _sunIntensity = _sun.intensity;
    }
    private void Update()
    {
        if (TimeOfDay >= 0f)
        {
            //if (Application.isPlaying)
            //{
                TimeOfDay -= Time.deltaTime / _gameData.DurationOfDays[_gameData.LastCurrentDay];
            //}
            _dayIndicatorUI.fillAmount = TimeOfDay;

            _sun.color = _gradientDirectionalLight.Evaluate(_sunCurve.Evaluate(TimeOfDay));

            if (TimeOfDay < 0.25f && TimeOfDay > 0f)
            {
                _sun.intensity = Mathf.Lerp(0f, 1.2f , TimeOfDay);
            }
            //else
            //{
            //    _sun.intensity = _sunIntensity;
            //}

            RenderSettings.ambientLight = _gradientAmbient.Evaluate(_sunCurve.Evaluate(TimeOfDay));

            RenderSettings.fogColor = _gradientFog.Evaluate(TimeOfDay);

            _sun.transform.localRotation = Quaternion.Euler(Mathf.Lerp(TimeEndDay, TimeStartDay, TimeOfDay), 0, 0);  //60 - 210
        }
        else if(TimeOfDay <= 0f)
        {
            TheDayIsOver?.Invoke(true);
        }
    }
}
