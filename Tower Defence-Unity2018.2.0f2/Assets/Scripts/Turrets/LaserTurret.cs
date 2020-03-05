using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserTurret : Turret {

    public LineRenderer laser;//激光模型

    // Use this for initialization
    new void Start () {
        base.Start();
        laser = Instantiate(laser);
    }
	
	// Update is called once per frame
	new void Update () {
        base.Update();
        if (!(enemyList.Count > 0))
        {
            laser.GetComponent<Laser>().Close();
        }
    }

    override public void Attack()
    {
        laser.GetComponent<Laser>().Hit(firePositon.transform, enemyList[0].transform);
        enemyList[0].GetComponent<Enemy>().TakeDamage(damage * Time.deltaTime);
        enemyList[0].GetComponent<Enemy>().SlowDown(slowRate);
    } 
}
