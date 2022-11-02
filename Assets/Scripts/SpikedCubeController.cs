using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Направления движения куба с шипами
/// </summary>
public enum SpikedCubeDirection
{
    Left,
    Right,
    Up,
    Down
}

/// <summary>
/// Контроллер движения врага-шипованного кубика
/// </summary>
public class SpikedCubeController : MonoBehaviour
{
    private const float BIGFLOAT = 1000000f;

    [SerializeField] private SpikedCubeDirection currentDirection;
    [SerializeField] private float speed = 2f;
    private SpikedCubeDirection oppositeDirection;
    private Vector3 target;

    private void Start()
    {
        SetDirectionBasedParameters();
    }

    /// <summary>
    /// Установка параметров направления и цели для куба
    /// </summary>
    private void SetDirectionBasedParameters()
    {
        switch (currentDirection)
        {
            case SpikedCubeDirection.Left:
                oppositeDirection = SpikedCubeDirection.Right;
                target = new Vector3(-BIGFLOAT, transform.position.y, transform.position.z);
                return;
            case SpikedCubeDirection.Right:
                oppositeDirection = SpikedCubeDirection.Left;
                target = new Vector3(BIGFLOAT, transform.position.y, transform.position.z);
                return;
            case SpikedCubeDirection.Up:
                oppositeDirection = SpikedCubeDirection.Down;
                target = new Vector3(transform.position.x, BIGFLOAT, transform.position.z);
                return;
            case SpikedCubeDirection.Down:
                oppositeDirection = SpikedCubeDirection.Up;
                target = new Vector3(transform.position.x, -BIGFLOAT, transform.position.z);
                return;
        }
    }

    /// <summary>
    /// Вход в коллизию с любым объектом - смена направления на противоположное
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        ChangeCurrentDirection(oppositeDirection);
    }

    /// <summary>
    /// Постоянно двигает куб через MoveTowards
    /// </summary>
    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, target,
            speed * Time.fixedDeltaTime);
    }
    
    /// <summary>
    /// Смена текущего направления движения для куба с шипами
    /// </summary>
    /// <param name="direction">Направление, которое будет установлено</param>
    public void ChangeCurrentDirection(SpikedCubeDirection direction)
    {
        currentDirection = direction;
        SetDirectionBasedParameters();
    }
}
