using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleSpawner : MonoBehaviour
{
    [SerializeField] private GameObject[] upperLeftWallGroups;
    [SerializeField] private GameObject[] downLeftWallGroups;
    [SerializeField] private GameObject[] upperRightWallGroups;
    [SerializeField] private GameObject[] downRightWallGroups;

    private void Start()
    {
        ObstaclesInitialize();
    }

    /// <summary>
    /// Инициализировать препятствия на уровне
    /// </summary>
    private void ObstaclesInitialize()
    {
        ActivateOneRandom(upperLeftWallGroups);
        ActivateOneRandom(downLeftWallGroups);
        ActivateOneRandom(upperRightWallGroups);
        ActivateOneRandom(downRightWallGroups);
    }

    /// <summary>
    /// Активирует один объект из массива случайным образом
    /// </summary>
    /// <param name="group">Группа объектов для выбора активируемого</param>
    private void ActivateOneRandom(GameObject[] group)
    {
        group[Random.Range(0, group.Length)].SetActive(true);
    }
}
