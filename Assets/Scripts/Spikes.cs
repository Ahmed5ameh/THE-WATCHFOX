using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Spikes : MonoBehaviour
{
    private void Reset()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            #region UNITY_EDITOR
            Debug.Log($"{name} Triggered");
            #endregion

            FindObjectOfType<LifeCount>().LoseAllLives();
            FindObjectOfType<Fox>().Die();
        }
    }
}
