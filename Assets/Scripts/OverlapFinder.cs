using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlapFinder : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("OverlapFinder on " + this.name +  " starting");
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerStay(Collider other)
    {
        Debug.Log("Colliding still with" + other.name);
    }
}
