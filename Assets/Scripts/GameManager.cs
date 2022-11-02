using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

/// <summary>
/// Игровой менеджер, отвечает за контроль над процессом игры и над переключением UI окон в игре
/// </summary>
public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(gameObject);
            return;
        }
        instance = this;
        PhysicsIgnoreInitialize();
    }
    #endregion

    private const string LINEPOINTIGNORINGLAYER = "Line Point";
    private const string PLAYERIGNORINGLAYER = "Ignore Player";
    private const string PLAYERLAYER = "Player Square";
    private const string MENUSCREEN = "Menu";
    private const float TIMESPENTMULTIPLIER = 100f;
    private const float TIMESPENTEXPECTED = 120f;
    private const float COLLISIONSADDITION = -1000f;
    private const int CUBESCOUNT = 2;

    [SerializeField] private Text timerUi;
    [SerializeField] private Text gameOverScoreUi;
    [SerializeField] private GameObject pauseWindow;
    [SerializeField] private GameObject gameOverWindow;

    private float timeSpent = 0f;
    private int failes = 0;
    private int cubesReady = 0;
    private bool shouldCountTime = true;

    /// <summary>
    /// Инициализирует игнор слоев для правильного физического взаимодействия
    /// </summary>
    private void PhysicsIgnoreInitialize()
    {
        Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer(LINEPOINTIGNORINGLAYER), 
            LayerMask.GetMask(LINEPOINTIGNORINGLAYER));
        Physics2D.SetLayerCollisionMask(LayerMask.NameToLayer(PLAYERLAYER),
            ~LayerMask.GetMask(PLAYERIGNORINGLAYER, LINEPOINTIGNORINGLAYER));
    }

    /// <summary>
    /// Таймер для очков, для высокой точности измерения
    /// </summary>
    private void Update()
    {
        if (shouldCountTime)
        {
            timeSpent += Time.deltaTime;
        }       
    }

    /// <summary>
    /// Апдейтер UI
    /// </summary>
    private void FixedUpdate()
    {
        if (shouldCountTime)
        {
            timerUi.text = ((int)timeSpent).ToString();
        }
    }    
    
    /// <summary>
    /// Рестарт игры
    /// </summary>
    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    /// <summary>
    /// Вызывать при коллизии куба-игрока с чем-либо, кроме его базы
    /// </summary>
    public void AddFail()
    {
        failes++;
    }

    /// <summary>
    /// Переход в меню
    /// </summary>
    public void GoToMenu()
    {
        SceneManager.LoadScene(MENUSCREEN);
    }

    /// <summary>
    /// Вызывает окно завершения игры
    /// </summary>
    public void GameOver()
    {
        shouldCountTime = false;
        gameOverScoreUi.text += (Mathf.Max((TIMESPENTEXPECTED - timeSpent) * TIMESPENTMULTIPLIER, 0) +
            failes * COLLISIONSADDITION).ToString();
        gameOverWindow.SetActive(true);
    }

    /// <summary>
    /// Снимает/включает паузу
    /// </summary>
    /// <param name="pause">Включить паузу?</param>
    public void SetPause(bool pause)
    {
        shouldCountTime = !pause;
        pauseWindow.SetActive(pause);
    }

    /// <summary>
    /// Сообщает GameManager, что один из кубов дошел до точки В
    /// </summary>
    public void CubeIsReady()
    {
        cubesReady++;
        if (cubesReady >= CUBESCOUNT)
        {
            GameOver();
        }
    }
}
