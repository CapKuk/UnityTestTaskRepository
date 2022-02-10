using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 0.1f;
    private Vector3 vectorSpeed;
    private const int updateTime = 10;
    private int updateTimer;
    private Animator animator;

    public Game_Model model;
    private void Start()
    {
        vectorSpeed = new Vector3(0, -0.1f, 0);
        updateTimer = updateTime;
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (!model.isPlayerAlive)
        {
            return;
        }
        if(updateTimer == updateTime)
        {
            updateTimer = 0;
        }
        else
        {
            updateTimer++;
            return;
        }
        vectorSpeed.x = vectorSpeed.x + Random.Range(-speed, speed);
        vectorSpeed.y = vectorSpeed.y + Random.Range(-speed, speed);

        transform.position = new Vector3(transform.position.x + vectorSpeed.x, transform.position.y + vectorSpeed.y, -1);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!model.isPlayerAlive)
        {
            return;
        }
        Debug.Log(collision.gameObject.name);

        switch (collision.gameObject.name)
        {
            case "LeftWall":
                transform.position = new Vector3(transform.position.x + 1, transform.position.y, -1);
                vectorSpeed.x *= -1;
                break;
            case "RightWall":
                transform.position = new Vector3(transform.position.x - 1, transform.position.y, -1);
                vectorSpeed.x *= -1;
                break;
            case "UpWall":
                transform.position = new Vector3(transform.position.x, transform.position.y - 1, -1);
                vectorSpeed.y *= -1;
                break;
            case "DownWall":
                transform.position = new Vector3(transform.position.x, transform.position.y + 1, -1);
                vectorSpeed.y *= -1;
                break;
            case "Explosion(Clone)":
                animator.SetBool("isAlive", false);
                die();
                break;
        }
    }

    private void die()
    {
        model.playerDied(gameObject);
        Destroy(gameObject, 0.5f);
    }
}
