using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretButton : MonoBehaviour,Buttons {

    private Turret turret;
    List<string> temp = new List<string>();

    private void Start()
    {
        for(int i = 0; i < GameObject.Find("InformationUI").transform.childCount; i++)
        {
            temp.Add(GameObject.Find("InformationUI").transform.GetChild(i).GetComponent<Text>().text);
        }
        turret = transform.parent.parent.parent.transform.GetComponent<Turret>();
    }

    private void OnMouseEnter()
    {
        List<string> information = UIManager.information;
        Turret upgradeTo = turret.upgradeTo;
        //information.Add("<color=#00FF01FF>"+upgradeTo.turretName+"</color>");
        //if (turret.isLaser)
        //{
        //    if (turret.damage != upgradeTo.damage) 
        //        information.Add("Damage: " + turret.damage + "/s" + "+<color=#00FF01FF>" + -(upgradeTo.damage - turret.damage) + "</color>");
        //    else
        //        information.Add("Damage: " + turret.damage + "/s");
        //}
        //else
        //{
        //    if (turret.damage != upgradeTo.damage)
        //        information.Add("Damage: " + turret.damage + "+<color=#00FF01FF>" + -(upgradeTo.damage - turret.damage) + "</color>");
        //    else
        //        information.Add("Damage: " + turret.damage);
        //    if (turret.attackRate != upgradeTo.attackRate)
        //        information.Add("Attack Rate: " + (1f / turret.attackRate) + "/s" + "+<color=#00FF01FF>" + 1f/-(upgradeTo.attackRate - turret.attackRate) + "</color>");
        //    else
        //        information.Add("Attack Rate: " + (1f / turret.attackRate) + "/s");
        //}
        //if (turret.range != upgradeTo.range)
        //    information.Add("Range: " + turret.range + "Grids" + "+<color=#00FF01FF>" + -(upgradeTo.range - turret.range) + "</color>");
        //else
        //    information.Add("Range: " + turret.range + "Grids");
        //if (turret.slowRate > 0)
        //{
        //    if (turret.slowRate != upgradeTo.slowRate)
        //        information.Add("Slow Effect: " + turret.slowRate + "%" + "+<color=#00FF01FF>" + -(upgradeTo.slowRate - turret.slowRate) + "</color>");
        //    else
        //        information.Add("Slow Effect: " + turret.slowRate + "%");
        //}
        //information.Add("Cost: ￥" + turret.cost);
        UIManager.ShowInformation();
    }

    private void OnMouseExit()
    {
        for (int i = 0; i < temp.Count; i++)
        {
            UIManager.information.Add(temp[i]);
        }
        UIManager.ShowInformation();
    }

    public void Upgrade()
    {
        transform.parent.parent.parent.GetComponent<Turret>().Upgrade();
    }

    public void Show()
    {
        throw new System.NotImplementedException();
    }

    public void Hide()
    {
        throw new System.NotImplementedException();
    }
}
