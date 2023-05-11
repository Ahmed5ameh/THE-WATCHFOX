using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] Transform platform;
    [SerializeField] int goalPoint = 0;
    [SerializeField] float movementSpeed = 0.5f;

    void Update()
    {
        MoveToNextPoint();
    }

    void MoveToNextPoint()
    {
        //change the position of the platform (move towards goalPoint)
        platform.position = Vector2.MoveTowards(
            platform.position,              /*current position*/
            points[goalPoint].position,     /*goal position*/
            Time.deltaTime * movementSpeed  /*speed*/
            );

        //check if we are in a very close proximity of the next point
        if (Vector2.Distance(platform.position, points[goalPoint].position) < 0.1)
        {
            /*if so, change the goal point to the next one*/

            //check if we reached the last point (end of the list), reset to first point
            if (goalPoint == points.Count - 1)
                goalPoint = 0;
            else
                goalPoint++;
            
        }
    }
}
