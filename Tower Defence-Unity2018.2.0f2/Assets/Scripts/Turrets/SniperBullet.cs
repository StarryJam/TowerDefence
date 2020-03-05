using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperBullet : MonoBehaviour {

    private Vector3 position;
    private float timer = 0;

	// Use this for initialization
	void Start () {
        position = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        timer += Time.deltaTime;
        transform.Translate(Vector3.forward * Time.deltaTime * 400);
        if (timer >= 0.2)
        {
            timer = 0;
            transform.position = position;
        }
	}
}
