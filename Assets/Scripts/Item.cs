using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public enum InteractionType
    {
        None,
        PickUp,
        Examine,
    }
    public InteractionType type;
    void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;    //enable the obj collider to be a trigger
        gameObject.layer = 7;
    }

    public void Interact()
    {
        switch(type)
        {
            case InteractionType.PickUp:
                FindObjectOfType<InteractionSystem>().PickUpItem(gameObject);   //call "PickUpItem()" in "InteractionSystem" script, and pass this gameObject as a parameter to it
                gameObject.SetActive(false);
                break;
            case InteractionType.Examine:
                Debug.Log("Examine");
                break;
            default:
                Debug.Log("NULL");
                break;
        }
    }
}
