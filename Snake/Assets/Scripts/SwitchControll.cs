using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class SwitchControll : MonoBehaviour
{
    [SerializeField] private Button _buttonSwitch;
    [SerializeField] private Joystick _joystickControl;
    [SerializeField] private Text _text;
    [SerializeField] private GameObject _buttonsDriveObject;
    public bool Switched { get; private set; }

    private void OnEnable()
    {
        _buttonSwitch.onClick.AddListener(Switch);
    }
    private void OnDisable()
    {
        _buttonSwitch.onClick.RemoveListener(Switch);
    }

    private void Switch()
    {
        if (!Switched)
        {
            _buttonsDriveObject.SetActive(true);
            _joystickControl.gameObject.SetActive(false);
            _text.text = "Enable Joystick";
            Switched = true;
        }
        else
        {
            _buttonsDriveObject.SetActive(false);
            _joystickControl.gameObject.SetActive(true);
            _text.text = "Enable Buttons";
            Switched = false;
        }
    }

}
