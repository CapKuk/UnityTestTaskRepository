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
    private bool _isStartYet = true;
    public bool isStartYet
    {
        get => _isStartYet;
        private set
        {
            _isStartYet = value;
        }
    } 
        

    private Game_Model model;
    private void Start()
    {
        vectorSpeed = new Vector3(0, -0.1f, 0);
        updateTimer = updateTime;
        animator = GetComponent<Animator>();
        StartCoroutine(corutine());
    }

    private IEnumerator corutine()
    {
        yield return new WaitForSeconds(0.2f);
        isStartYet = false;
    }

    void Update()
    {
        if (!model.isPlayerAlive || model.isGameFinished)
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
                if (isStartYet) break;
                animator.SetBool("isAlive", false);
                die();
                break;
        }
    }

    public void setModel(GameObject sender)
    {
        if(sender.name == "GameModel")
        {
            model = sender.GetComponent<Game_Model>();
        }
    }

    private void die()
    {
        model.enemyDied(gameObject);
        Destroy(gameObject, 0.5f);
    }

    public void destroy(GameObject sender)
    {
        if(sender.name == "GameModel")
        {
            Destroy(gameObject);
        }
    }
}
