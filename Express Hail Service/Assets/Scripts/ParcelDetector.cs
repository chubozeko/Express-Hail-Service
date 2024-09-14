using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ParcelDetector : MonoBehaviour
{
    private SphereCollider sphereCollider;
    public GameObject detectedParcel;
    void Start()
    {
        // Make sure the collider is assigned
        sphereCollider = GetComponent<SphereCollider>();
    }

    private void OnTriggerEnter(Collider col)
    {
        // Check for nearby parcels within the sphereCollider area
        if (col.gameObject.CompareTag("Parcel"))
        {
            // Save the detected parcel
            detectedParcel = col.gameObject;
            Debug.Log("Parcel detected nearby!");
        }
        
    }

    private void OnTriggerExit(Collider col)
    {
        // Check if parcels are no longer within the sphereCollider area
        if (col.gameObject.CompareTag("Parcel"))
        {
            // Remove the detected parcel
            detectedParcel = null;
            Debug.Log("No Parcels detected");
        }
        
    }
}
