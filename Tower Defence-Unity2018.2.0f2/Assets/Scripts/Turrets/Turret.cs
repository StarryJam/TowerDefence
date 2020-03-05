using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using cakeslice;

public abstract class Turret : MonoBehaviour,Selected
{

    /*-----------------------------------防御塔信息-------------------------------------*/
    public string turretName;
    public int cost = 0;
    public float damage = 10;//伤害
    public float attackRate = 1;
    public int range;//射程
    public float slowRate = 0;//减速效果
    public GameObject fireEffect;//开火特效
    public Transform firePositon;//炮口位置
    public Turret upgradeTo;
    public AudioSource fireSound;
    /*----------------------------------------------------------------------------------*/

    private GameObject RangeUI;
    public  GameObject RangeUIPrefeb;
    public bool canBeUpgrade;
    private GameObject turretUIPrefeb;
    private GameObject turretUI;
    public GameObject head;
    public List<GameObject> enemyList;//射程内的敌人

    protected float attackTimer;

    public abstract void Attack();

    // Use this for initialization
    protected void Start()
    {
        gameObject.AddComponent<AudioSource>();
        attackTimer = attackRate;
        RangeUIPrefeb = (GameObject)Resources.Load("Prefebs/UI/RangeUI");
        turretUIPrefeb = (GameObject)Resources.Load("Prefebs/UI/TurretUI");
        GetComponent<SphereCollider>().radius = 3.35f * (0.5f+range);
        turretUI = Instantiate(turretUIPrefeb, transform.position + new Vector3(0, 6, 0), Camera.main.transform.rotation);
        turretUI.transform.parent = gameObject.transform;
        turretUI.SetActive(false);
    }

    // Update is called once per frame
    protected void Update()
    {
        if (enemyList.Count > 0)
        {
            while (enemyList[0] == null)
            {
                enemyList.RemoveAt(0);
                if (enemyList.Count == 0)
                    break;
            }
            if (enemyList.Count > 0)
            {
                head.transform.LookAt(enemyList[0].transform);
                Vector3 ang = head.transform.localEulerAngles;
                ang.x = 0;
                ang.z = 0;
                head.transform.localEulerAngles = ang;
                Attack();
            }
        }
    }

    protected void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Add(other.gameObject);
        }
    }

    protected void OnTriggerExit(Collider other)
    {
        if (other.tag == "Enemy")
        {
            enemyList.Remove(other.gameObject);
        }
    }
    
    public virtual void ShowInformation()
    {
        //List<string> information = UIManager.information;
        //information.Add(turretName);
        //if (isLaser)
        //{
        //    information.Add("Damage: " + damage + "/s");
        //}
        //information.Add("Range: " + range + "Grids");
        //if (slowRate > 0)
        //    information.Add("Slow Effect: " + slowRate + "%");
        //information.Add("Cost: ￥" + cost);
        //UIManager.ShowInformation();
    }

    public void BeSelected()
    {
        gameObject.GetComponent<SetChildren>().SetOutlineOnChildren();
        RangeUI = Instantiate(RangeUIPrefeb, new Vector3(transform.position.x, 1.01f, transform.position.z), Quaternion.identity);
        RangeUI.transform.localScale = new Vector3(range * 2 + 1, range * 2 + 1, range * 2 + 1);
        ShowInformation();
        ShowUI();
        ClickObject.selected = gameObject;
    }

    public void DeSelected()
    {
        gameObject.GetComponent<SetChildren>().DestroyOutlineOnChildren();
        HideUI();
        Destroy(RangeUI);
        UIManager.HideInformation();
    }

    public void Upgrade()
    {
        if (UIManager.playerState.money >= upgradeTo.cost)
        {
            UIManager.playerState.MoneyChange(-upgradeTo.cost);
            upgradeTo = transform.parent.GetComponent<BaseCube>().turret = Instantiate(upgradeTo, transform.position, transform.rotation).GetComponent<Turret>();
            Destroy(gameObject);
            transform.parent.GetComponent<BaseCube>().PlayBuildEffct();
        }
        else
        {
            UIManager.NotEnoughMoney();
        }
    }

    public void Sell()
    {
        Destroy(this);
    }

    public void ShowUI()
    {
        turretUI.SetActive(true);
    }

    public void HideUI()
    {
        turretUI.SetActive(false);
    }

    private void OnDestroy()
    {
        Debug.Log(this);
        DeSelected();
    }

}
