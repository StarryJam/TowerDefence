using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LunchTurret : Turret {

    /*-----------------------------------防御塔信息-------------------------------------*/
    public float bulletSpeed = 30;//子弹速度
    public GameObject missile;//炮弹模型
    /*----------------------------------------------------------------------------------*/

        

    // Use this for initialization
    new void Start () {
        base.Start();
        attackTimer = attackRate;
    }
	
	// Update is called once per frame
	new void Update () {
        attackTimer += Time.deltaTime;
        base.Update();
    }

    override public void Attack()
    {
        if (attackTimer >= attackRate)
        {
            if (fireSound != null)
            {
                AudioSource fireSound = Instantiate(this.fireSound, gameObject.transform.position, Quaternion.identity);
                fireSound.Play();
                Destroy(fireSound.gameObject, 1f);
            }
            GameObject _missile = GameObject.Instantiate(missile, firePositon.transform.position, firePositon.transform.rotation);
            _missile.GetComponent<Bullet>().SetBullet(enemyList[0].transform, bulletSpeed, damage);
            attackTimer = 0;
            GameObject effect = Instantiate(fireEffect, firePositon.position, firePositon.rotation);
            Destroy(effect, 1f);
        }
    }

    override public void ShowInformation()
    {
        List<string> information = UIManager.information;
        information.Add(turretName);
        information.Add("Damage: " + damage);
        information.Add("Attack Rate: " + (1f / attackRate) + "/s");
        information.Add("Range: " + range + "Grids");
        information.Add("Cost: ￥" + cost);
        UIManager.ShowInformation();
    }
}
