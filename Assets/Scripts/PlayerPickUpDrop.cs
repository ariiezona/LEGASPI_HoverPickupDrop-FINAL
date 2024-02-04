using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class PlayerPickUpDrop : MonoBehaviour
{

    [SerializeField]
    private LayerMask pickableLayerMask;

    [SerializeField]
    private Transform playerCameraTransform;

    [SerializeField]
    private Transform objectGrabPointTransform;

    [SerializeField]
    private GameObject pickUpUI;

    [SerializeField] 
    private GameObject dropUI;

    [SerializeField]
    [Min(1)]
    private float hitRange = 2.5f;

    private RaycastHit hit;

    private ObjectGrabbable objectGrabbable;

    private void Update()
    {
        Debug.DrawRay(playerCameraTransform.position, playerCameraTransform.forward * hitRange, Color.red);
        if (hit.collider != null)
        {
            pickUpUI.SetActive(false);
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(false);
        }
        if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out hit, hitRange, pickableLayerMask))
        {;
            pickUpUI.SetActive(true);
            hit.collider.GetComponent<Highlight>()?.ToggleHighlight(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (objectGrabbable == null)
            {
                float pickupDistance = 2.5f;
                if (Physics.Raycast(playerCameraTransform.position, playerCameraTransform.forward, out RaycastHit raycastHit, pickupDistance, pickableLayerMask))
                {
                    if (raycastHit.transform.TryGetComponent(out objectGrabbable))
                    {
                        dropUI.SetActive(true);
                        objectGrabbable.Grab(objectGrabPointTransform);
                    }
                }
            }
            else
            {
                dropUI.SetActive(false);
                objectGrabbable.Drop();
                objectGrabbable = null;
            }
        }


    }
}
