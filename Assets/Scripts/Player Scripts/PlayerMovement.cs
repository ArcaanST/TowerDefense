using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovement : MonoBehaviour
{
    PlayerInput playerInput;

    public float movementSpeed = 3f;

    private Rigidbody2D myBody;

    private Vector2 moveVector;

    private SpriteRenderer sr;

    private Transform tf;

    private float harvestTimer;
    private bool isHarvesting;

    private GameObject artifact;
    [SerializeField]
    private GameObject aim;

    private void Awake()
    {
        tf = GetComponent<Transform>();
        myBody = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        playerInput = GetComponent<PlayerInput>();
    }

    private void Update()
    {
        if (Time.time > harvestTimer)
            isHarvesting = false;

        FlipSprite();
    }

    private void FixedUpdate()
    {
        if (isHarvesting)
            myBody.velocity = Vector2.zero;

        else
        {
            Vector2 input = playerInput.actions["Move"].ReadValue<Vector2>();
            moveVector = new Vector2(input.x, input.y);

            if (moveVector.sqrMagnitude > 1)
                moveVector = moveVector.normalized;

            myBody.velocity = new Vector2(moveVector.x * movementSpeed, moveVector.y * movementSpeed);
        }
    }

    void FlipSprite()
    {
        if (moveVector.x > 0)
        {
            sr.flipX = false;
            //tf.localScale = new Vector3(0.5f, 0.5f, 1f);
        }
        else if (moveVector.x < 0)
        {
            sr.flipX = true;
            //tf.localScale = new Vector3(-0.5f, 0.5f, 1f);
        }
    }

    public void HarvestStopMovement(float time) {
        isHarvesting = true;
        harvestTimer = Time.time + time;
    }

    public bool IsHarvesting() 
    {
        return isHarvesting;
    }
} 