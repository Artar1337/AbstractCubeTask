using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Контроллер главного меню
/// </summary>
public class MenuController : MonoBehaviour
{
    private const string GAMESCENE = "Game";

    /// <summary>
    /// Загружает в игру
    /// </summary>
    public void StartGame()
    {
        SceneManager.LoadScene(GAMESCENE);
    }

    /// <summary>
    /// Выходит из игры
    /// </summary>
    public void Exit()
    {
        Application.Quit();
    }
}
