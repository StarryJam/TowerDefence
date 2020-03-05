using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverLoad : MonoBehaviour {

    public static float rate = 50;
    private float dataChange;
    private Turret turret;
    public GameObject effect;

    private void Start()
    {
        effect = (GameObject)Resources.Load("Skills/OverLoad/OverLoadEffect");
        turret = GetComponent<Turret>();
        effect = Instantiate(effect, turret.transform.position, Quaternion.identity);
        if (turret.GetComponent<LaserTurret>() != null)
        {
            dataChange = turret.damage* (1 + rate / 100) - turret.damage;
            turret.damage += dataChange;
        }
        else if(turret.GetComponent<LunchTurret>() != null)
        {
            LunchTurret turret = this.turret.GetComponent<LunchTurret>();
            dataChange = turret.attackRate - turret.attackRate / (1 + rate / 100);
            turret.attackRate -= dataChange;
        }
    }
    
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnDestroy()
    {
        for (int i = 0; i < effect.transform.childCount; i++)
        {
            effect.transform.GetChild(i).GetComponent<ParticleSystem>().Stop();
        }
        Destroy(effect, 1);
        if (turret.GetComponent<LaserTurret>() != null)
        {
            LaserTurret turret = this.turret.GetComponent<LaserTurret>();
            turret.damage -= dataChange;
        }
        else if (turret.GetComponent<LunchTurret>() != null)
        {
            LunchTurret turret = this.turret.GetComponent<LunchTurret>();
            turret.attackRate += dataChange;
        }
    }
}
