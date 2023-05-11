using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooting : MonoBehaviour
{
    [SerializeField] GameObject shootingItem;
    [SerializeField] Transform shootingPoint;
    [SerializeField] bool canShoot = true;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Q))
        {
            Shoot();
        }
    }

    void Shoot()
    {
        if(!canShoot)
            return;

        //Instantiate
        GameObject ShootingItem = Instantiate(shootingItem, shootingPoint);
        //Remove it from parenting to the player, so we dont get a rotation problem
        ShootingItem.transform.parent = null;
        //Destroy it after 1 second
        Destroy(ShootingItem.gameObject, 1.0f);
    }
}
