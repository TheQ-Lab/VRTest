using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Microsoft.MixedReality.Input;
using UnityEngine.XR;

public class ControllerManager : MonoBehaviour
{
    //private Microsoft.MixedReality.Input.ControllerInput inputType;
    
    public const float threshold = 0.1f;

    public GameObject destroyer;

    private InteractionExecutor interactionExecutor;

    private const InputDeviceCharacteristics rightControllerCharacteristics = InputDeviceCharacteristics.Right | InputDeviceCharacteristics.Controller;
    private const InputDeviceCharacteristics leftControllerCharacteristics = InputDeviceCharacteristics.Left | InputDeviceCharacteristics.Controller;

    private InputDevice rightController;
    private InputDevice leftController;
    // Start is called before the first frame update
    void Start()
    {
        interactionExecutor = GetComponent<InteractionExecutor>();
        ConnectAllDevices();
        //InputDevices.deviceDisconnected += ReConnectDevice;

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
                Debug.Log("Done assigning - " + devices[0].name + " - " + devices[0].characteristics);
            }
        } while (devices.Count == 0);
    }


    void ConnectAllDevices()
    {
        StartCoroutine(RepeatGetDevice(rightControllerCharacteristics, (d) => { rightController = d; }));
        StartCoroutine(RepeatGetDevice(leftControllerCharacteristics, (d) => { leftController = d; }));
    }


    [HideInInspector]
    public bool aPressed = false, triggerPressed = false;
    void Update()
    {
        if (rightController.isValid)
        {
            rightController.TryGetFeatureValue(CommonUsages.primaryButton, out bool primaryButtonVal);
            if (primaryButtonVal)
            {
                if (!aPressed) 
                {
                    Debug.Log("Pressing Primary Button");
                    if (destroyer != null)
                        destroyer.SetActive(!destroyer.activeSelf);
                    aPressed = true;
                    interactionExecutor.EventReceiver(ref aPressed);
                }
            }
            else
                aPressed = false;
            rightController.TryGetFeatureValue(CommonUsages.trigger, out float triggerValue);
            if(triggerValue > threshold)
            {
                if (!triggerPressed)
                {
                    Debug.Log("Pressing Trigger");
                    triggerPressed = true;
                }
            }
            else
                triggerPressed = false;
        }
    }




    // oldies

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
    void ReConnectDevice(InputDevice device)
    {
        ConnectAllDevices();
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
}
