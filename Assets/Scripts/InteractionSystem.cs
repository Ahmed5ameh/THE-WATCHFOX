using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionSystem : MonoBehaviour
{
    [SerializeField] Transform detectionPoint;
    const float detectionRadius = 0.2f;
    [SerializeField] LayerMask detectionLayer;
    [SerializeField] GameObject detectedObject;
    [SerializeField] List<GameObject> pickedItems = new List<GameObject>();

    void Update()
    {
        if (DetectObject())
        {
            if (InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();     //access item script on this game object and call it's interact method
            }
        }
    }
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(detectionPoint.position, detectionRadius);
    }


    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D _object = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        if(_object != null )
        {
            detectedObject = _object.gameObject;
            return true;
        }
        detectedObject = null;
        return false;
    }
    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
    }
}
