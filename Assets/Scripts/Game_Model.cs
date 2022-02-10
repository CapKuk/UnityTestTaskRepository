using UnityEngine;
using UnityEngine.UI;

public class Game_Model : MonoBehaviour
{
    public Text lifeCounterTextField;

    private bool _isPlayerAlive = true;
    public bool isPlayerAlive
    {
        get => _isPlayerAlive;
        private set {
            _isPlayerAlive = value;
        }
    }

    private bool _isEnemyAlive = true;
    public bool isEnemyAlive
    {
        get => _isEnemyAlive;
        private set
        {
            _isEnemyAlive = value;
        }
    }

    public void refreshLife(GameObject sender, int life)
    {
        if(sender.name == "Player" && isPlayerAlive)
        {
            lifeCounterTextField.text = life.ToString();
        }
    }

    public void playerDied(GameObject sender)
    {
        if (sender.name == "Player" && isPlayerAlive)
        {
            isPlayerAlive = false;
        }
    }

    public void enemyDied(GameObject sender)
    {
        if (sender.name == "Enemy" && isEnemyAlive)
        {
            isEnemyAlive = false;
        }
    }
}
