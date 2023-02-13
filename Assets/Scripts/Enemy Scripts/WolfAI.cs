using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WolfAI : MonoBehaviour
{
    [SerializeField]
    private bool isEater;

    [SerializeField]
    private float moveSpeed = 1f;

    [SerializeField]
    private int attackDamage = 1;

    [SerializeField]
    private float attackTimeTreshold = 2f;

    [SerializeField]
    private float eatTimeTreshold = 4;

    [SerializeField]
    private LayerMask playerMask;

    [HideInInspector]
    public bool isMoving, left;

    [SerializeField]
    private Artifact[] artifact;
    private Artifact currentArtifact;

    private PlayerHealth playerTarget;

    private float attackTimer;
    private float eatTimer;

    private bool killingPlayer;
    private bool isAttacking;

    // Start is called before the first frame update
    void Start()
    {
        if (isEater)
        {
            SearchForTarget();
            killingPlayer = false;
        }
        else
        {
            isAttacking = false;
        }

        artifact = FindObjectsOfType<Artifact>();

        currentArtifact = artifact[Random.Range(0, artifact.Length)];
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentArtifact)
            return;

        if (isEater)
        {
            if (Vector2.Distance(transform.position, playerTarget.transform.position) > 0.5f)
                killingPlayer = false;

            if (playerTarget && playerTarget.enabled && !killingPlayer)
            {
                // if not close to the buhs continue walking towards it, else stop and eat the bush
                if (Vector2.Distance(transform.position, playerTarget.transform.position) > 0.5f)
                {
                    float step = moveSpeed * Time.deltaTime;

                    transform.position = Vector2.MoveTowards(transform.position,
                        playerTarget.transform.position, step);

                    isMoving = true;
                }

                else
                {
                    isMoving = false;
                    eatTimer = Time.time + eatTimeTreshold;
                    killingPlayer = true;
                }
            }

            else if (killingPlayer)
            {
                Debug.Log(killingPlayer);
                if (Time.time > eatTimer)
                {
                    playerTarget.TakeDamage(attackDamage);
                    killingPlayer = false;
                }
            }

            else
            {
                SearchForTarget();
            }

            if (playerTarget)
            {
                if (playerTarget.transform.position.x < transform.position.x)
                    left = true;
                else
                    left = false;
            }

            if (!playerTarget)
                SearchForTarget();
        }

        else
        {
            // wolf that destroys artifact
            if (Vector2.Distance(transform.position, currentArtifact.transform.position) > 0.5f)
            {
                float step = moveSpeed * Time.deltaTime;

                transform.position = Vector2.MoveTowards(transform.position,
                    currentArtifact.transform.position, step);

                isMoving = true;
            }
            else if (!isAttacking)
            {
                isAttacking = true;
                attackTimer = Time.time + attackTimeTreshold;

                isMoving = false;
            }

            if (isAttacking)
            {
                if (Time.time > attackTimer)
                {
                    Attack();
                    attackTimer = Time.time + attackTimeTreshold;
                }
            }

            if (currentArtifact.transform.position.x < transform.position.x)
                left = true;
            else
                left = false;
        }
    } // update

    void SearchForTarget()
    {
        playerTarget = null;

        Collider2D[] hits;

        for (int i = 1; i < 50; i++) {

            hits = Physics2D.OverlapCircleAll(transform.position, Mathf.Exp(i), playerMask);

            foreach (Collider2D hit in hits)
            {
                if (hit && (hit.GetComponent<PlayerHealth>() &&
                    hit.GetComponent<PlayerHealth>().enabled))
                {
                    playerTarget = hit.GetComponent<PlayerHealth>();
                    break;
                }
            }
            if (playerTarget)
                break;
        }
    } // search for target

    void Attack()
    {
        currentArtifact.TakeDamage(attackDamage);
    }
}