using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class SkillOnTarget : Skill {
    
    public bool toEnemy = true;//true为以敌方为目标false为以友方为目标
    private string targetTag;
    protected GameObject target;


    override public void StartCast()
    {
        if (ClickObject.isCasting)
            return;

        base.StartCast();
        if (timer <= 0)
        {
            Cursor.SetCursor(UIManager.onPointPointer, Vector2.zero, CursorMode.Auto);
        }
    }

    override public void CastSkill()
    {
        GameObject hitObject = ClickObject.hit.collider.gameObject;
        if ((toEnemy && ClickObject.hit.collider.gameObject.tag == "Enemy") || (!toEnemy && ClickObject.hit.collider.gameObject.tag == "Turret")) 
        {
            if (hitObject.GetComponent<SubObject>() != null)
            {
                target = hitObject.GetComponent<SubObject>().FindFather();
            }
            else
                target = ClickObject.hit.collider.gameObject;
        }
    }
}