using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Игровой менеджер, отвечает за контроль над процессом игры
/// </summary>
public class GameManager : MonoBehaviour
{
    private const string LINEPOINTIGNORINGLAYER = "Line Point";
    private const string PLAYERIGNORINGLAYER = "Ignore Player";
    private const string PLAYERLAYER = "Player Square";


    private void Awake()
    {
        PhysicsIgnoreInitialize();
    }

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
}
