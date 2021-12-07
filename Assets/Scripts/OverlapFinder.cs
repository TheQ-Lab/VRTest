using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapFinder : MonoBehaviour
{
    public GameObject intersectingObject;

    private List<Collider> allColliders = new List<Collider>();
    private List<float> distanceToColliders = new List<float>();

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("OverlapFinder on " + this.name + " starting");
    }

    /// <summary>
    /// Looks for the collision with the closest center of collider to this.collider.
    /// </summary>
    /// <param name="myBool">Parameter value to pass.</param>
    /// <returns>GameObject that is most likely to be selected by the user. 
    /// Can return null if there are no collisions.</returns>
    public GameObject GetMostIntersectingObject()
    {
        if (allColliders.Count == 0)
            return null;
        distanceToColliders.Clear();
        float shortestDist, newDist;
        shortestDist = float.MaxValue;
        GameObject returner = null;
        foreach (Collider c in allColliders)
        {
            Vector3 otherP = c.bounds.center;
            //distanceToColliders.Add(Vector3.Distance(otherP, transform.position));
            newDist = Vector3.Distance(otherP, transform.position);

            if(newDist < shortestDist)
            {
                returner = c.gameObject;
                shortestDist = newDist;
            }
        }
        return returner;
    }


    //private void OnTriggerStay(Collider other)
    //{
    //    Debug.Log("Colliding still with" + other.name);
    //}

    // Keep colliderList up to date
    private void OnTriggerEnter(Collider other)
    {
        allColliders.Add(other);
    }

    private void OnTriggerExit(Collider other)
    {
        allColliders.Remove(other);
    }
}
