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

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        //checkPlauerPosition();

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
                        transform.position = new Vector2(transform.position.x, transform.position.y + speed);
                        break;
                    case "LEFT":
                        transform.position = new Vector2(transform.position.x - speed, transform.position.y);
                        break;
                    case "DOWN":
                        transform.position = new Vector2(transform.position.x, transform.position.y - speed);
                        break;
                    case "RIGHT":
                        transform.position = new Vector2(transform.position.x + speed, transform.position.y);
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
        Instantiate(bomb, transform.position, Quaternion.identity);
    }

    private void checkPlauerPosition()
    {
        var pos = Camera.main.WorldToScreenPoint(transform.position);

        if (transform.transform.position.x > 100)
        {
            transform.transform.position = new Vector2(100, transform.transform.position.y);
            Debug.Log(transform.position + " " + transform.transform.position + " " + pos);
        }
        if (transform.transform.position.x < -100)
        {
            transform.transform.position = new Vector2(-100, transform.transform.position.y);
        }
        if (transform.transform.position.y > 213)
        {
            transform.transform.position = new Vector2(transform.transform.position.x, 213);
        }
        if (transform.transform.position.y < -213)
        {
            transform.transform.position = new Vector2(transform.transform.position.x, -213);
        }
    }

}
