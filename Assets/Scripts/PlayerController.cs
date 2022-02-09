using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{

    public Game_Model model;
    public GameObject bomb;

    private bool[] durations = new bool[4] { false, false, false, false };
    private enum durationNumber
    {
        UP = 0,
        LEFT = 1,
        DOWN = 2,
        RIGHT = 3
    }

    private float speed = 1;
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
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
        }
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
        Instantiate(bomb, new Vector3(transform.position.x, transform.position.y, transform.position.z - 10), Quaternion.identity);
    }
}
