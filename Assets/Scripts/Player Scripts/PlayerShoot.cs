using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerShoot : MonoBehaviour
{
    [SerializeField]
    private GameObject shotPrefab;

    [SerializeField]
    private float attackCooldown = 0.3f;

    private float attackTimer;

    [SerializeField]
    private AudioSource audioSource;

    private Vector3 spawnPosition;
    
    [SerializeField]
    private Transform bulletSpawn;

    private GameObject artifact;

    [SerializeField]
    private GameObject aimObj;
    private SpriteRenderer sr;

    [SerializeField]
    PlayerInput playerInput;

    private void Awake()
    {
        artifact = GameObject.FindWithTag("Artifact");
        sr = GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        FinalAim();
    }

    private void Update()
    {
        if (Time.time > attackTimer)
        {
            Shoot();

            attackTimer = Time.time + attackCooldown;
        }
    }

    /*
    void Aim()
    {
        //Touch and aim position
        Vector3 touch = Touchscreen.current.position.ReadValue();
        var world = Camera.main.ScreenToWorldPoint(touch);

        if(touch.y > 450)
        {
            aimObj.transform.position = new Vector3(world.x, world.y, 0f);

            //Gun Rotation
            Vector2 offset = new Vector2(aimObj.transform.position.x - transform.position.x,
                aimObj.transform.position.y - transform.position.y);

            float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

            transform.rotation = Quaternion.Euler(0, 0, angle);
            sr.flipY = (aimObj.transform.position.x < transform.position.x);
        }
    }
    */

    void AutomaticAim()
    {
        Vector2 offset = new Vector2(aimObj.transform.position.x - transform.position.x,
                aimObj.transform.position.y - transform.position.y);

        float angle = Mathf.Atan2(offset.y, offset.x) * Mathf.Rad2Deg;

        transform.rotation = Quaternion.Euler(0, 0, angle);
        sr.flipY = (aimObj.transform.position.x < transform.position.x);
    }

    void FinalAim()
    {
        Vector2 input = playerInput.actions["Look"].ReadValue<Vector2>();
        if(input == Vector2.zero)
            return;

        float angle = Mathf.Atan2(input.y, input.x) * Mathf.Rad2Deg;
        var lookRotation = Quaternion.Euler(angle * Vector3.forward);
        transform.rotation = lookRotation;
        sr.flipY = input.x < transform.position.x;
        //transform.localScale = input.x > 0 ? transform.localScale : new Vector3(1 * transform.localScale.x, -1 * transform.localScale.y, -1 * transform.localScale.z);
    }

    void Shoot()
    {
        if (!artifact)
            return;

        audioSource.Play();

        spawnPosition = bulletSpawn.transform.position ;

        Instantiate(shotPrefab, spawnPosition, transform.rotation);
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Wolf"))
        {
            aimObj.transform.position = collision.transform.position;
            //AutomaticAim();
        }
    }
}