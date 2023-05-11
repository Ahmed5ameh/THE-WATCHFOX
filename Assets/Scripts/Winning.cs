using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Winning : MonoBehaviour
{
    [SerializeField] AudioSource winningAudioSource;
    [SerializeField] GameObject Fox;
    [SerializeField] Fox foxScript;
    private void Start()
    {
        Fox = GameObject.FindGameObjectWithTag("Player");
        foxScript = Fox.GetComponent<Fox>();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player") & !winningAudioSource.isPlaying)
        {
            Debug.Log("You Won!");
            GameObject.FindGameObjectWithTag("MainCamera").GetComponent<AudioSource>().Stop();
            winningAudioSource.Play();
            foxScript.ForceStopForWinning();
            foxScript.enabled = false;      //Disable fox script
        }
        
    }
}
