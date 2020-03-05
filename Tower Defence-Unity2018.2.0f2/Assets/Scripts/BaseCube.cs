using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class BaseCube : MonoBehaviour,Selected{
    [HideInInspector]
    public Turret turret;
    public GameObject buildEffect;
    public GameObject buildUI;
    public List<Turret> turretList;

    private void Start()
    {
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void Update()
    {
        if (buildUI.activeSelf == false)
        {
            buildUI.transform.rotation = Camera.main.transform.rotation;
        }
    }

    public void BuildTurret(int turretNum)
    { 
        if (UIManager.playerState.GetComponent<PlayerState>().money >= turretList[turretNum].cost)
        {
            turret = turretList[turretNum];
            UIManager.informationUI.GetComponent<Animator>().SetTrigger("Hide");
            UIManager.playerState.GetComponent<PlayerState>().MoneyChange(-turret.cost);
            buildUI.SetActive(false);
            turret = Instantiate(turret, transform.position, Quaternion.identity);
            turret.transform.parent = gameObject.transform;
            PlayBuildEffct();
        }
        else
            UIManager.NotEnoughMoney();
    }

    public void PlayBuildEffct()
    {
        GameObject effect = GameObject.Instantiate(buildEffect, transform.position, Quaternion.identity);
        GameObject.Destroy(effect, 1);
    }

    public void ShowUI()
    {
        buildUI.SetActive(true);
        StopCoroutine("Time");
    }

    public void HideUI()
    {
        if (buildUI.activeSelf)
        {
            buildUI.GetComponent<Animator>().SetTrigger("Close");
            StartCoroutine("Time");
        }
    }

    IEnumerator Time()
    {
        yield return new WaitForSeconds(0.5f);
        buildUI.SetActive(false);
    }

    private void OnMouseEnter()
    {
        if (turret == null && EventSystem.current.IsPointerOverGameObject() == false && gameObject != ClickObject.selected) 
        {
            GetComponent<Renderer>().material.color = Color.red;
        }
    }

    private void OnMouseExit()
    {
        if (gameObject != ClickObject.selected)
        GetComponent<Renderer>().material.color = Color.white ;
    }

    public void BeSelected()
    {
        GetComponent<Renderer>().material.color = Color.yellow;
        GameObject allCubes = GameObject.Find("BaseCubes");
        for (int i = 0; i < allCubes.transform.childCount; i++)
        {
            if (allCubes.transform.GetChild(i).gameObject.GetComponent<BaseCube>() != gameObject)
            {
                allCubes.transform.GetChild(i).gameObject.GetComponent<BaseCube>().HideUI();
            }
        }

        if (turret == null)
        {
            buildUI.SetActive(false);
            buildUI.SetActive(true);
            ShowUI();
        }
        else
        {
            //升级炮台
        }
    }

    public void DeSelected()
    {
        GetComponent<Renderer>().material.color = Color.white;
        HideUI();
        Destroy(gameObject.GetComponent<Outline>());
    }
}
