using UnityEngine;
using UnityEngine.UI;

public class Game_Model : MonoBehaviour
{
    public Text lifeCounterTextField;
    public GameObject enemyPrefub;
    public GameObject restartButton;
    public GameObject canvas;
    public GameObject stonePrefab;

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

    private Vector2 size = new Vector2();
    private void setSize()
    {
        var h = canvas.GetComponent<RectTransform>().rect.height;
        var w = canvas.GetComponent<RectTransform>().rect.width;

        size.x = w;
        size.y = h;
    }

    private void Start()
    {
        setSize();

        GameObject stone = Instantiate(stonePrefab, new Vector3(0, 0, 0), Quaternion.identity);
        stone.transform.SetParent(this.transform.parent);
        stone.transform.localPosition = new Vector3(0, -size.y / 4, -1);

        stone = Instantiate(stonePrefab, new Vector3(size.x / 2, size.y / 2, -1), Quaternion.identity);
        stone.transform.SetParent(this.transform.parent);
        stone.transform.localPosition = new Vector3(0, 0, -1);

        stone = Instantiate(stonePrefab, new Vector3(size.x / 2, (3 * size.y) / 4, -1), Quaternion.identity);
        stone.transform.SetParent(this.transform.parent);
        stone.transform.localPosition = new Vector3(0, size.y / 4, -1);

        createEnemy();
    }

    private void createEnemy()
    {
        if (enemy != null) return;
        Debug.Log(size);
        var x = Random.Range((-size.x / 2) + 30, (size.x / 2) - 30);
        var y = Random.Range((-size.y / 2) + 30, (size.y / 2) - 30);

        var enemyObject = Instantiate(enemyPrefub, new Vector3(0, 0, 0), Quaternion.identity);
        enemyObject.transform.SetParent(this.transform.parent);
        enemyObject.transform.localPosition = new Vector3(x, y, -1);

        enemy = enemyObject.GetComponent<EnemyController>();
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
