using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionExecutor : MonoBehaviour
{
    public Transform modelParent;

    private ControllerManager controllerManager;
    public OverlapFinder overlapFinder;

    private void Start()
    {
        controllerManager = GetComponent<ControllerManager>();
    }

    bool held = false;
    GameObject hitObject = null;
    Vector3 difference;
    Transform controller, manipulated;
    private void Update()
    {
        if (controllerManager.triggerPressed)
        {
            if (!held)
            {
                hitObject = overlapFinder.GetMostIntersectingObject();
                if (hitObject != null)
                {
                    // save transforms and offset(difference) for calculation of controller movement
                    Debug.LogWarning(hitObject.name);
                    difference = overlapFinder.transform.position - hitObject.transform.position;
                    controller = overlapFinder.transform;
                    manipulated = hitObject.transform;
                }
                held = true;
            }
            else
            {
                if (hitObject != null)
                {
                    // move the held object with the controller essentially parented by factoring in its offset from the controller
                    manipulated.position = controller.position - difference;
                }
            }
        }
        else
        {
            // no hold button pressed
            hitObject = null;
            held = false;
        }
    }

    public void EventReceiver(ref bool b)
    {
        // not yet coded out; reference check does not work thus far
        if(bool.ReferenceEquals(b, controllerManager.aPressed) || true /*TEMP*/)
        {
            Debug.LogWarning("RESET");
            foreach (Transform child in modelParent)
            {
                child.localPosition = Vector3.zero;
            }
        }
    }
}
