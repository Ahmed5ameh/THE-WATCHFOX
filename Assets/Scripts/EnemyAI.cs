using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class EnemyAI : MonoBehaviour
{
    [SerializeField] List<Transform> points = new List<Transform>();
    [SerializeField] int nextID = 0;    
    [SerializeField] int idChangeValue = 1;
    [SerializeField] float movementSpeed = 2;
    [SerializeField] Transform playerTransform;

    private void Reset()
    {
        Initialize();
    }

    private void Update()
    {
        MoveToNextPoint();
    }

    void Initialize()
    {
        GetComponent<BoxCollider2D>().isTrigger = true;     //let collider of this object (enemy) to be a trigger
        GameObject root = new GameObject(name + "_Root");   //create root object for the enemy
        root.transform.position = transform.position;       //reset position of root to enemy position
        transform.SetParent(root.transform);                //set enemy object (this object) as a child of root
        GameObject waypoints = new GameObject("Waypoints"); //create waypoints object
        waypoints.transform.SetParent(root.transform);      //make waypoints child of root
        waypoints.transform.position = root.transform.position;

        //Create 2 points and reset their position to waypoints objects
        //Make the points childeren of waypoint object
        //Set points position to root
        GameObject p1 = new GameObject("Point1"); p1.transform.SetParent(waypoints.transform); p1.transform.position = root.transform.position;
        GameObject p2 = new GameObject("Point2"); p2.transform.SetParent(waypoints.transform); p2.transform.position = root.transform.position;

        //Add them to the list
        points.Add(p1.transform);
        points.Add(p2.transform);
    }

    void MoveToNextPoint()
    {
        Transform goalPoint = points[nextID];   //Get the next Point transform

        //if the X value of the target/goal point is > our (enemy) X position,
        //this means the goal point is to our right and we need to flip the enemy,
        //bcs by default he is facing left side
        if (goalPoint.transform.position.x > transform.position.x)
            transform.localScale = new Vector3(-10, 10, 10);
        else
            transform.localScale = new Vector3(10, 10, 10);

        //Move the enemy towards the goal/target point
        transform.position = Vector2.MoveTowards(
            transform.position,             /*current position*/
            goalPoint.position,             /*target position*/
            movementSpeed * Time.deltaTime  /*speed*/
            );

        #region CODE_BASSEM_HNSL7O_B3DEEN_XD
        //Vector3 dir = playerTransform.position - transform.position;
        //float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        //if (angle > 90)
        //{
        //    thisSprite.flipX = true;
        //}
        //transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward); 
        //transform.LookAt(playerTransform.position);
        #endregion


        //Check the distance between enemy position and goal position to trigger next point
        if (Vector2.Distance(transform.position, goalPoint.position) < 0.3)
        {
            /*Check if we are at the end of the line (make the change -1)*/
            //we have 2 points in the list with index (0,1),
            //so when the nextID has the index of 1,
            //it means we reached the end of the list so we have to start again from 0, so we need to set idChangeValue = -1
            if (nextID == points.Count - 1)
                idChangeValue = -1;

            /*Check if we are at the start of the line (make the change +1)*/
            if (nextID == 0)
                idChangeValue = 1;

            /*Apply the change on the nextID*/
            nextID += idChangeValue;
        }

    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag.Equals("Player"))
        {
            FindObjectOfType<LifeCount>().LoseLife();
            collision.GetComponent<Animator>().SetTrigger("isHurt");
        }
    }
}
