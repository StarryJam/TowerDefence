using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour {

    public Transform target;
    public GameObject hitEffect;
    public AudioSource hitSound;
    //public bool hasTrail;
    //public GameObject trail;
    public float damage;
    public float speed;
    public float lifeTime;
    private float randx;
    private float randy;
    private float randz;

    public void SetBullet(Transform _target, float _speed , float _damage)
    {

        target = _target;
        speed = _speed;
        damage = _damage;

    }

    public void Attack(Transform _target)
    {
        if (target != null)
        {
            transform.LookAt(target.position + new Vector3(randx, randy, randz));
            transform.Translate(Vector3.forward * Time.deltaTime * speed);
            
        }
        else
            Destroy(gameObject);
        
    }

	// Use this for initialization
	void Start () {
        
        randx = Random.Range(-1f, 1f);
        randy = Random.Range(-1f, 1f);
        randz = Random.Range(-1f, 1f);
    }
	
	// Update is called once per frame
	void Update () {
        Attack(target);
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Enemy")
        {
            if (hitSound != null)
            {
                AudioSource hitSound = Instantiate(this.hitSound, gameObject.transform.position, Quaternion.identity);
                hitSound.Play();
                Destroy(hitSound.gameObject, 1f);
            }
            other.GetComponent<Enemy>().TakeDamage(damage);
            GameObject effect = GameObject.Instantiate(hitEffect, transform.position, Quaternion.identity);
            Destroy(effect, lifeTime * 1.2f);
            Destroy(gameObject);
        }
    }
}
