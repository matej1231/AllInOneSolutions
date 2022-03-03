using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    [SerializeField] GameObject playerTarget; //ako se odlucim kasnije dodat respawn morat cu refaktorirat ili nesto iskombinirat ili ....
    private float moveSpeed = 3f;

    void Update()
    {
        if (playerTarget)
        {
            transform.position = Vector2.MoveTowards(transform.position, playerTarget.transform.position, moveSpeed * Time.deltaTime);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            Destroy(collision.gameObject);
        }
    }
}
