using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Input;
using UnityEngine.XR;

public class ControllerManager : MonoBehaviour
{
    private Microsoft.MixedReality.Input.ControllerInput inputType;

    private const InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    private const InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

    private InputDevice rightController;
    private InputDevice leftController;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(RepeatGetDevice(rightControllerCharacteristics, (d) => { rightController = d; }));
        StartCoroutine(RepeatGetDevice(leftControllerCharacteristics, (d) => { leftController = d; }));

        //StartCoroutine(RepeatCheckDisconnectedDevices());
    }

    private void GetAllDevices()
    {
        List<InputDevice> devices = new List<InputDevice>();
        InputDevices.GetDevices(devices);
        //InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
        //InputDevices.

        foreach (var item in devices)
        {
            Debug.Log(item.name + item.characteristics);
        }
    }

    private IEnumerator RepeatGetDevice(InputDeviceCharacteristics characteristics, System.Action<InputDevice> callback)
    {
        var devices = new List<InputDevice>();

        do
        {
            yield return null;
            InputDevices.GetDevicesWithCharacteristics(characteristics, devices);
            if (devices.Count > 0)
            {
                callback(devices[0]);
                Debug.Log("Done assigning - " + devices[0].name);
            }
        } while (devices.Count == 0);
    }

    private IEnumerator RepeatCheckDisconnectedDevices()
    {
        yield return new WaitForSeconds(5f);
        List<InputDevice> foundDevices = new List<InputDevice>();
        bool found = false;
        Debug.Log("Start deactivated Device Checker");

        do
        {
            yield return new WaitForSeconds(1f);
            Debug.Log("check...");
            foundDevices.Clear();
            InputDevices.GetDevices(foundDevices);
            foreach(InputDevice d in foundDevices)
                Debug.Log(d.name);
            if (rightController != null)
            {
                foreach(InputDevice device in foundDevices)
                {
                    found = (device.serialNumber == rightController.serialNumber) || found;
                    if (!found)
                    {
                        //rightController = (InputDevice) null;
                        StartCoroutine(RepeatGetDevice(rightControllerCharacteristics, (d) => { rightController = d; }));
                        Debug.Log("found disconnected");
                    }
                }
            }
        } while (true);

    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(targetDevice.name);
    }
}
