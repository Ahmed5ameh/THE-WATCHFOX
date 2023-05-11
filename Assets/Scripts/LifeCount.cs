using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeCount : MonoBehaviour
{
    [SerializeField] Image[] lives;
    [SerializeField] int livesRemaining = 4;

    //void Update()
    //{
    //    //if (Input.GetKeyDown(KeyCode.Return))   //Enter
    //    //{
    //    //    LoseLife();
    //    //}
    //}

    //4 lives - 4 images (0, 1, 2, 3)
    //3 lives - 3 images ([0], 1, 2, 3)
    //2 lives - 2 images ([0], [1], 2, 3)
    //1 lives - 1 images ([0], [1], [2], 3)
    //0 lives - 0 images ([0], [1], [2], [3])   => LOSE
    public void LoseLife()
    {
        //if no lives remaining do nothing
        if (livesRemaining == 0)
            return;
        //decrease the value of lifesRemaining
        livesRemaining--;

        //hide one of the life images
        lives[livesRemaining].enabled = false;

        //if we run out of lifes, we lose game
        if(livesRemaining == 0)
        {
            FindObjectOfType<LevelManager>().Restart();
        }
    }
    public void ResetLives()
    {
        for(int i = 0; i < lives.Length; i++)
            lives[i].enabled = true;
        livesRemaining = 4;
    }
    
    public void LoseAllLives()
    {
        //if no lives remaining do nothing
        if (livesRemaining == 0)
            return;

        livesRemaining = 0;

        for(int i = 0; i<= lives.Length -1; i++)
            lives[i].enabled = false;

        FindObjectOfType<LevelManager>().Restart();
    }
}
