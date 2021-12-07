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
            AddCollidersToChildren();
        }
    }

    /// <summary>
    /// Adds a standard collider(!convex) to every child in the models hierarchy
    /// </summary>
    /// <param name="myBool">Parameter value to pass.</param>
    /// <returns>void</returns>
    private void AddCollidersToChildren()
    {
        foreach (Transform t in transform)
        {
            if (t.gameObject.activeSelf)
            {
                t.gameObject.TryGetComponent<MeshCollider>(out MeshCollider m);
                if (m == null)
                    t.gameObject.AddComponent<MeshCollider>();
            }
        }
        this.enabled = false;
    }
}
