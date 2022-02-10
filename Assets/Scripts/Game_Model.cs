using UnityEngine;
using UnityEngine.UI;

public class Game_Model : MonoBehaviour
{
    public Text lifeCounterTextField;
    public GameObject enemyPrefub;
    public GameObject restartButton;

    private int enemyLeft = 10;
    private EnemyController enemy = null;
    private bool _isPlayerAlive = true;
    public bool isPlayerAlive
    {
        get => _isPlayerAlive;
        private set {
            _isPlayerAlive = value;
        }
    }

    private bool _isGameFinished = false;
    public bool isGameFinished
    {
        get => _isGameFinished;
        private set
        {
            _isGameFinished = value;
        }
    }

    private bool _isEnemyAlive = true;
    public bool isEnemyAlive
    {
        get => _isEnemyAlive;
        private set
        {
            _isEnemyAlive = value;
            if(value == false && enemyLeft != 0)
            {
                _isEnemyAlive = true;
            }
        }
    }

    private void Start()
    {
        createEnemy();
    }

    private void createEnemy()
    {
        if (enemy != null) return;
        var x = Random.Range(30, 250);
        var y = Random.Range(30, 450);
        enemy = (Instantiate(enemyPrefub, new Vector3(x, y, 0), Quaternion.identity) as GameObject).GetComponent<EnemyController>();
        enemy.setModel(gameObject);
        Debug.Log(x + " " + y);
        enemyLeft--;
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
        Debug.Log("PlayerDied " + sender.name + " " + isPlayerAlive);
        if (sender.name == "Player" && isPlayerAlive)
        {
            isPlayerAlive = false;
            finishGame();
        }
    }

    public void enemyDied(GameObject sender)
    {
        Debug.Log("Enemy Died");
        if (sender.name == "Enemy(Clone)" && isEnemyAlive)
        {
            isEnemyAlive = false;
            if(enemyLeft == 0)
            {
                finishGame();
            }
            else
            {
                enemy = null;
                createEnemy();
            }
        }
    }
    public bool isEnemyStartYet()
    {
        return enemy.isStartYet;
    }

    private void finishGame()
    {
        isGameFinished = true;
        restartButton.SetActive(true);
    }

    private void resetGame()
    {
        Debug.Log("Game will be reseted " + enemy);
        enemyLeft = 10;
        if (enemy != null)
        {
            enemy.destroy(gameObject);
            enemy = null;
        }
        createEnemy();
        isEnemyAlive = true;
        isPlayerAlive = true;
        isGameFinished = false;
        restartButton.SetActive(false);
    }
}
