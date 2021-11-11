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
                    manipulated.position = controller.position - difference;
                }
            }
        }
        else
        {
            hitObject = null;
            held = false;
        }
    }

    public void EventReceiver(ref bool b)
    {
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
