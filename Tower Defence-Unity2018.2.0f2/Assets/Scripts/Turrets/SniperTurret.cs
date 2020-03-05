using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperTurret : Turret {
    
    public GameObject hitEffect;
    

    new void Update()
    {
        attackTimer += Time.deltaTime;
        base.Update();
    }

    override public void Attack()
    {
        if (attackTimer >= attackRate)
        {
            attackTimer = 0;
            GameObject fireEffect = Instantiate(this.fireEffect, firePositon.position, firePositon.rotation);
            Destroy(fireEffect, 1f);
            GameObject hitEffect = Instantiate(this.hitEffect, enemyList[0].transform.position, Quaternion.identity);
            hitEffect.transform.LookAt(gameObject.transform);
            hitEffect.transform.GetChild(2).GetComponent<Renderer>().material = enemyList[0].GetComponent<MeshRenderer>().material;
            Destroy(hitEffect, 2f);
            enemyList[0].GetComponent<Enemy>().TakeDamage(damage);
        }
    }
}
