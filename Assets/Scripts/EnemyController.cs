using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    private float speed = 0.1f;
    private Vector3 vectorSpeed;
    private const int updateTime = 10;
    private int updateTimer;

    private void Start()
    {
        vectorSpeed = new Vector3(0, -0.1f, 0);
        updateTimer = updateTime;
    }

    void Update()
    {
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

        transform.position = transform.position + new Vector3(vectorSpeed.x, vectorSpeed.y, 0);
    }

    private void OnCollisionEnter(Collision collision)
    {
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
        }
    }

}