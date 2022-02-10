using System;
using System.Collections;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Game_Model model;
    public GameObject bomb;

    private bool[] durations = new bool[4] { false, false, false, false };
    private int _life = 3;
    private int life
        {
        get => _life;
        set
        {
            _life = value;
            model.refreshLife(gameObject, value);
            if (_life <= 0)
            {
                die();
            }
            else
            {
                getIndestructible();
            }
        }
        }
    private enum durationNumber
    {
        UP = 0,
        LEFT = 1,
        DOWN = 2,
        RIGHT = 3
    }

    private float speed = 1;
    private bool isIndestructible = false;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        model.refreshLife(gameObject, life);
    }

    void Update()
    {
        if (!model.isPlayerAlive || model.isGameFinished)
        {
            return;
        }
        bool isRunning = false;

        for (int i = 0; i < 4; i++)
        {
            if (durations[i])
            {
                isRunning = true;
                string name = Enum.GetName(typeof(durationNumber), i);
                switch (name)
                {
                    case "UP":
                        transform.position = new Vector3(transform.position.x, transform.position.y + speed, -1);
                        break;
                    case "LEFT":
                        transform.position = new Vector3(transform.position.x - speed, transform.position.y, -1);
                        break;
                    case "DOWN":
                        transform.position = new Vector3(transform.position.x, transform.position.y - speed, -1);
                        break;
                    case "RIGHT":
                        transform.position = new Vector3(transform.position.x + speed, transform.position.y, -1);
                        break;
                }
            }
        }

        if (isRunning)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!model.isPlayerAlive || model.isGameFinished)
        {
            return;
        }
        Debug.Log(collision.gameObject.name);
        switch (collision.gameObject.name)
        {
            case "LeftWall":
                transform.position = new Vector3(transform.position.x + 10, transform.position.y, -1);
                durations[(int)Enum.Parse(typeof(durationNumber), "LEFT")] = false;
                break;
            case "RightWall":
                transform.position = new Vector3(transform.position.x - 10, transform.position.y, -1);
                durations[(int)Enum.Parse(typeof(durationNumber), "RIGHT")] = false;
                break;
            case "UpWall":
                transform.position = new Vector3(transform.position.x, transform.position.y - 10, -1);
                durations[(int)Enum.Parse(typeof(durationNumber), "UP")] = false;
                break;
            case "DownWall":
                transform.position = new Vector3(transform.position.x, transform.position.y + 10, -1);
                durations[(int)Enum.Parse(typeof(durationNumber), "DOWN")] = false;
                break;
            case "Explosion(Clone)":
                if (!isIndestructible)
                {
                    life--;
                    getIndestructible();
                }
                break;
            case "Enemy(Clone)":
                if (!isIndestructible)
                {
                    life--;
                    getIndestructible();
                }
                break;
        }
    }

    private void die()
    {
        for(int i = 0; i < 4; i++)
        {
            durations[i] = false;
        }
        model.playerDied(gameObject);
    }
    private void getIndestructible()
    {
        StartCoroutine(corutine());
    }

    private IEnumerator corutine()
    {
        animator.SetBool("isHit", true);
        isIndestructible = true;
        yield return new WaitForSeconds(2f);
        isIndestructible = false;
        animator.SetBool("isHit", false);
    }

    private void startMoving(string duration)
    {
        switch (duration)
        {
            case "Left":
                durations[(int)Enum.Parse(typeof(durationNumber), "LEFT")] = true;
                break;
            case "Right":
                durations[(int)Enum.Parse(typeof(durationNumber), "RIGHT")] = true;
                break;
            case "Up":
                durations[(int)Enum.Parse(typeof(durationNumber), "UP")] = true;
                break;
            case "Down":
                durations[(int)Enum.Parse(typeof(durationNumber), "DOWN")] = true;
                break;
        }
    }
    private void stopMoving(string duration)
    {
        switch (duration)
        {
            case "Left":
                durations[(int)Enum.Parse(typeof(durationNumber), "LEFT")] = false;
                break;
            case "Right":
                durations[(int)Enum.Parse(typeof(durationNumber), "RIGHT")] = false;
                break;
            case "Up":
                durations[(int)Enum.Parse(typeof(durationNumber), "UP")] = false;
                break;
            case "Down":
                durations[(int)Enum.Parse(typeof(durationNumber), "DOWN")] = false;
                break;
        }
    }

    private void dropTheBomb()
    {
        if (!model.isPlayerAlive)
        {
            return;
        }
        Instantiate(bomb, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
    }

    private void resetGame()
    {
        life = 3;
        transform.position = new Vector3(140, 258, -1);
    }
}
