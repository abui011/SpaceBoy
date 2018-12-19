using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour {

    public float health = 100;
    public float shotCounter;
    private float minTBS = 0.2f;
    private float maxTBS = 1.5f;
    private float laserspeed = 8f;
    public GameObject eLaser;
    public GameObject explosion;
    public AudioClip deathSFX;
    public AudioClip laserSFX;
	// Use this for initialization
	void Start () {
        shotCounter = Random.Range(minTBS, maxTBS);
	}
	
	// Update is called once per frame
	void Update () {
		if (health <= 0)
        {
            explosion.transform.position = transform.position;
            Destroy(gameObject);
            Instantiate(explosion);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, .3f);
        }
        countdown();
	}

    private void countdown()
    {
        shotCounter -= Time.deltaTime;
        if (shotCounter <= 0f)
        {
            Fire();
            shotCounter = Random.Range(minTBS, maxTBS);
        }
    }

    private void Fire()
    {
        AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, .3f);
        GameObject laser = Instantiate(eLaser,
                                               transform.position,
                                               Quaternion.identity) as GameObject;
        laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, -laserspeed);
    }

   
    private void OnTriggerEnter2D(Collider2D lazer)
    {
        Damage dmg = lazer.gameObject.GetComponent<Damage>();
        health -= dmg.GetDamage();
        Destroy(lazer.gameObject);
    }
}
