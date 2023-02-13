using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField]
    private float bulletSpeed;

    [SerializeField]
    private int bulletDamage;

    private void Start()
    {
        StartCoroutine(DestroyBullet());
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * bulletSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wolf"))
        {
            collision.gameObject.GetComponent<WolfHealth>().TakeDamage(bulletDamage);
            Destroy(gameObject);
        }

        if (collision.CompareTag("Wall"))
        {
            Destroy(gameObject);
        }
    }

    IEnumerator DestroyBullet()
    {
        yield return new WaitForSeconds(6f);
        Destroy(this);
    }
}
