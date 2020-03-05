using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LightningTurret : Turret {
    public int extraTarget = 2;
    public float bounceRange = 4;
    public LightningChain lightning;

    new private void Update()
    {
        base.Update();
        attackTimer += Time.deltaTime;
    }

    public override void Attack()
    {
        if (attackTimer >= attackRate)
        {
            attackTimer = 0;
            LightningChain lightning = Instantiate(this.lightning);
            lightning.SetChain(firePositon.transform, enemyList[0].transform);
            Destroy(lightning.gameObject, 0.5f);
            GameObject bouncer = new GameObject("Empty");
            bouncer.transform.position = enemyList[0].transform.position;
            bouncer.AddComponent<LightningBouncer>();
            bouncer.GetComponent<LightningBouncer>().SetBouncer(damage, extraTarget, bounceRange, this.lightning, enemyList[0]);
            enemyList[0].GetComponent<Enemy>().TakeDamage(damage);
        }
    }

    
    //private IEnumerator Bounce()
    //{
        
    //}

    class LightningBouncer : MonoBehaviour
    {
        public float damage = 0;
        public int extraTarget = 0;
        public float bounceRange = 4;
        public LightningChain lightning;
        public List<GameObject> targetList = new List<GameObject>();
        private GameObject lastTarget;
        private Vector3 lastTargetPosition = new Vector3();
        private bool destroy = false;

        private void Update()
        {
            for(int i = 0; i < targetList.Count; i++)
            {
                if (targetList[i] != null)
                {
                    //targetList[i].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            //gameObject.SetActive(false);
            //Destroy(gameObject);
        }


        public void SetBouncer(float _damage, int _extraTarget, float _bounceRange, LightningChain _lightning, GameObject _target)
        {
            damage = _damage;
            extraTarget = _extraTarget;
            bounceRange = _bounceRange * 3.35f;
            lightning = _lightning;
            targetList.Add(_target);
            lastTarget = _target;
            lastTargetPosition = _target.transform.position;
            StartCoroutine("Bounce");
        }

        private void OnTriggerEnter(Collider other)
        {
            StopCoroutine("Bounce");
            if (targetList.Count - 1 < extraTarget)
            {
                if (other.gameObject.tag == "Enemy" && !targetList.Exists(o => o == other.gameObject))
                {
                    Destroy(gameObject.GetComponent<SphereCollider>());
                    targetList.Add(other.gameObject);
                    LightningChain lightning = Instantiate(this.lightning);
                    if (lastTarget != null)
                        lightning.SetChain(lastTarget.transform, targetList[targetList.Count - 1].transform);
                    else
                    {
                        GameObject deadPoint = new GameObject("DeadPoint");
                        deadPoint.transform.position = lastTargetPosition;
                        lightning.SetChain(deadPoint.transform, targetList[targetList.Count - 1].transform);
                    }
                    lastTarget = targetList[targetList.Count - 1];
                    lastTargetPosition = lastTarget.transform.position;
                    transform.position = other.gameObject.transform.position;
                    
                    Destroy(lightning.gameObject, 0.5f);
                    targetList[targetList.Count - 1].GetComponent<Enemy>().TakeDamage(damage);
                }
            }
            StartCoroutine("Bounce");
        }

        IEnumerator Bounce()
        {
            yield return new WaitForSeconds(0.15f);

            if (targetList.Count - 1 >= extraTarget)
            {
                Destroy(gameObject);
            }
            gameObject.AddComponent<SphereCollider>();
            gameObject.GetComponent<SphereCollider>().isTrigger = true;
            gameObject.GetComponent<SphereCollider>().radius = bounceRange;
            
            yield return new WaitForSeconds(0.1f);
            Destroy(gameObject);
        }
    }
}
   