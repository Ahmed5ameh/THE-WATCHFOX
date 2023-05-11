using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShootingItem : MonoBehaviour
{
    [SerializeField] float speed;

    private void Update()
    {
        //As this script will be attached to a game object which will be a child of player aka Fox
        //so we use the direction.x that fox is looking at to determine where will we shoot the item
        //we do so using "transform.localScale.x"
        transform.Translate(transform.right * transform.localScale.x * speed * Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Avoid colliding with the player
        if (collision.tag.Equals("Player"))
            return;

        //Trigger a custom action on the other object (if it exists)
        if (collision.GetComponent<ShootingAction>())    //if the object we hit, has a "ShootingAction script"
            collision.GetComponent<ShootingAction>().Action();

        //Destroy
        Destroy(gameObject);
    }
}
