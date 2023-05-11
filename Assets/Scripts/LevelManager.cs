using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [SerializeField] Vector2 playerInitPosition;
    void Awake()
    {
        //Save player initial position whenever the game starts
        playerInitPosition = FindObjectOfType<Fox>().transform.position;
    }
    public void Restart()
    {
        #region Method_One
        //Restart the scene
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        #endregion

        #region Method_Two
        ////Reset player position
        //FindObjectOfType<Fox>().transform.position = playerInitPosition;

        ////reset player movement speed
        //FindObjectOfType<Fox>().ResetPlayer();

        ////reset player lives
        //FindObjectOfType<LifeCount>().ResetLives(); 
        #endregion
    }
}
