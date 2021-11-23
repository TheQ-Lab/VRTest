using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteAlways]
public class EAAddCollidersToModelHirarchy : MonoBehaviour
{
    void Start()
    {
        
    }
    private void Update()
    {
        if (Application.IsPlaying(gameObject))
        {
            // Play logic
        }
        else
        {
            // Editor logic
            foreach (Transform t in transform)
            {
                if (t.gameObject.activeSelf)
                {
                    t.gameObject.TryGetComponent<MeshCollider>(out MeshCollider m);
                    if (m == null)
                        t.gameObject.AddComponent<MeshCollider>();
                    //t.gameObject.GetComponent<MeshCollider>();
                }
            }
            this.enabled = false;
        }
    }
}
