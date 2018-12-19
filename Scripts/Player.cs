using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour {


    public float moveSpeed = 10;
    float xMin;
    float xMax;
    float yMin;
    float yMax;
    public GameObject laserPrefab;
    public float laserspeed = 10f;
    public float firerate = 0.1f;
    public int health = 100;
    public GameObject explosion;
    public AudioClip deathSFX;
    public AudioClip laserSFX;
    Coroutine firingco;


    // Use this for initialization
    void Start()
    {
        SetUpMoveBoundaries();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        Fire();
        if (health <= 0)
        {
            explosion.transform.position = transform.position;
            Instantiate(explosion);
            Destroy(gameObject,.2f);
            AudioSource.PlayClipAtPoint(deathSFX, Camera.main.transform.position, .3f);
        }
    }


    private void OnTriggerEnter2D(Collider2D lazer)
    {
        Damage dmg = lazer.gameObject.GetComponent<Damage>();
        health -= dmg.GetDamage();
        Destroy(lazer.gameObject);
    }

    private void Fire()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            firingco = StartCoroutine(Firepeat());
        }
        if (Input.GetButtonUp("Fire1"))
        {
            StopCoroutine(firingco);
        }
    }

    IEnumerator Firepeat()
    {
        while (true)
        {
            AudioSource.PlayClipAtPoint(laserSFX, Camera.main.transform.position, .1f);
            GameObject laser = Instantiate(laserPrefab,
                                               transform.position,
                                               Quaternion.identity) as GameObject;
            laser.GetComponent<Rigidbody2D>().velocity = new Vector2(0, laserspeed);
            yield return new WaitForSeconds(firerate);
        }
    }

    private void SetUpMoveBoundaries()
    {
        Camera gameCamera = Camera.main;
        xMin = gameCamera.ViewportToWorldPoint(new Vector3(0.03f, 0, 0)).x;
        xMax = gameCamera.ViewportToWorldPoint(new Vector3(.97f, 0, 0)).x;

        yMin = gameCamera.ViewportToWorldPoint(new Vector3(0, 0.02f, 0)).y;
        yMax = gameCamera.ViewportToWorldPoint(new Vector3(0, .98f, 0)).y;
    }

    private void Move()
    {
        var changex = Input.GetAxis("Horizontal") * Time.deltaTime * moveSpeed;
        var changey = Input.GetAxis("Vertical") * Time.deltaTime * moveSpeed;

        var newXPos = Mathf.Clamp(transform.position.x + changex, xMin, xMax);
        var newYPos = Mathf.Clamp(transform.position.y + changey, yMin, yMax);
        transform.position = new Vector2(newXPos, newYPos);
    }
}

