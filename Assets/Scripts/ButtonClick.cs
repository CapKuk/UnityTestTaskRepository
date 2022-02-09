using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ButtonClick : MonoBehaviour
{

    public Button yourButton;
    public Transform player;


    // Start is called before the first frame update
    void Start()
    {
        Button btn = yourButton.GetComponent<Button>();
        //btn.onClick.AddListener(TaskOnClick);
    }

    void TaskOnClick()
    {
        Vector2 position = Camera.main.ScreenToWorldPoint(player.position);

        //Debug.Log(name);

        switch (name)
        {
            case "Button_Left":
                player.position = new Vector2(player.position.x - 10, player.position.y);
                break;
            case "Button_Right":
                player.position = new Vector2(player.position.x + 10, player.position.y);
                break;
            case "Button_Down":
                player.position = new Vector2(player.position.x, player.position.y - 10);
                break;
            case "Button_Up":
                player.position = new Vector2(player.position.x, player.position.y + 10);
                break;
            default:
                return;
        }
    }

    void clickStart()
    {
        //Debug.Log("Start");
    }

    void clickStop()
    {
        //Debug.Log("Stop");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
