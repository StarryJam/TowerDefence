using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using cakeslice;

public class ClickObject : MonoBehaviour {

    public static GameObject selected;
    public List<string> selectableObj;
    public static bool isCasting = false;
    public static Skill castingSkill;
    public static RaycastHit hit;

    // Update is called once per frame
    void Update () {

        if (Input.GetMouseButtonDown(0))//鼠标左键点击
        {
            if (EventSystem.current.IsPointerOverGameObject() == false)//判断是否点击在UI上
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

                bool isCollider = Physics.Raycast(ray, out hit, 1000);
                if (isCasting)//判断是否正在释放技能
                {
                    castingSkill.CastSkill();
                }
                else
                {
                    if (isCollider)
                    {
                        if (hit.collider.gameObject != selected && (hit.collider.GetComponent<SubObject>() == null || hit.collider.GetComponent<SubObject>().FindFather() != selected)) //判断是否点击当前选择对象或其父对象
                        {
                            //取消当前选择
                            if (selected != null)
                            {
                                selected.GetComponent<Selected>().DeSelected();
                                selected = null;
                            }
                            //选中点击物体
                            if (hit.collider.GetComponent<Selected>() != null)
                            {
                                selected = hit.collider.gameObject;
                                selected.GetComponent<Selected>().BeSelected();
                            }
                        }
                    }
                }
            }
        }
        else if (Input.GetMouseButtonDown(1))//鼠标右键点击
        {
            if (selected != null)
            {
                selected.GetComponent<Selected>().DeSelected();
                selected = null;
            }
            if (isCasting)
            {
                castingSkill.StopCast();
            }
        }
    }
}
