using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEditor.Callbacks;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public GameObject playerBody;
    public ParcelDetector parcelDetector;
    public Transform parcelHolder;
    private Rigidbody rigidbody;
    void Start()
    {
        // Make sure the rigidbody component is assigned
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        // Get the horizontal axis input for moving left/right ('A' and 'D' by default)
        float leftRightInput = Input.GetAxisRaw("Horizontal");
        // Get the vertical axis input for moving forward/backward ('W' and 'S' by default)
        float forwardBackwardInput = Input.GetAxisRaw("Vertical");
        // Move Player by setting the velocity of the rigidbody according to the inputs
        rigidbody.velocity = new Vector3(leftRightInput, 0, forwardBackwardInput) * moveSpeed;

        if (Input.GetKeyDown(KeyCode.Q)) 
        {
            // Check if a parcel was detected nearby AND no parcels are being held
            if (parcelDetector.detectedParcel != null && parcelHolder.childCount == 0)
            {
                // Set 'isKinematic' to true, so the parcel isn't affected by collisions
                parcelDetector.detectedParcel.GetComponent<Rigidbody>().isKinematic = true;
                // Set 'useGravity' to false, so the parcel is held on top
                parcelDetector.detectedParcel.GetComponent<Rigidbody>().useGravity = false;
                // Add parcel to the parcel Holder
                parcelDetector.detectedParcel.transform.SetParent(parcelHolder, false);
                // Reset its position relative to the parcel Holder
                parcelDetector.detectedParcel.transform.localPosition = Vector3.zero;
                // 
                parcelDetector.detectedParcel = null;
            }
        }
        
        if (Input.GetKeyDown(KeyCode.E))
        {
            // Check if the parcel Holder contains any parceles
            if (parcelHolder.childCount > 0)
            {
                // Get the first parcel in the Parcel Holder
                Transform heldParcel = parcelHolder.GetChild(0);
                // Set 'isKinematic' to false, to allow collisions on the parcel
                heldParcel.GetComponent<Rigidbody>().isKinematic = false;
                // Set 'useGravity' to true, to enable the gravity once it is dropped 
                heldParcel.GetComponent<Rigidbody>().useGravity = true;
                // Remove the parcel from the parcel Holder
                heldParcel.SetParent(null, false);
                // Set the parcel position in front of the Player (where the parcelDetector is)
                heldParcel.position = parcelDetector.transform.position;
            }
        }
        
    }

}
