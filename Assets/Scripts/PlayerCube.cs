using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контролирует OnCollisionEnter и следование по траектории для кубика (+ ее начальную рандомизацию)
/// </summary>
[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(SpriteRenderer))]
public class PlayerCube : MonoBehaviour
{
    private const float POSITIONERROR = 0.01f;
    private const float LINEZVVALUE = -1f;
    private const float LINEPOINTZVVALUE = -5f;
    private const int POINTSCOUNT = 10;
    private const float XRANGE = 7f;
    private const float YRANGE = 2.7f;
    private const float MAXDISTANCEBETWEENPOINTS = 7f;

    [SerializeField] private string exitLayer;
    [SerializeField] private float speed = 1f;
    [SerializeField] private Transform endPosition;
    [SerializeField] private Transform linePoints;
    [SerializeField] private GameObject pointPrefab;

    private LineRenderer lineRenderer;
    private Vector3 defaultPosition;
    private bool isMoving = false;

    private void Start()
    {
        defaultPosition = transform.position;
        lineRenderer = GetComponent<LineRenderer>();
        Color rendererColor = GetComponent<SpriteRenderer>().color;
        lineRenderer.startColor = rendererColor;
        lineRenderer.endColor = rendererColor;
        RandomizeTrail();
    }

    /// <summary>
    /// Рандомизирует точки для пути
    /// </summary>
    private void RandomizeTrail()
    {
        lineRenderer.positionCount = POINTSCOUNT;
        lineRenderer.SetPosition(0, new Vector3(defaultPosition.x, defaultPosition.y, LINEZVVALUE));
        for (int i = 1; i < lineRenderer.positionCount - 1; i++)
        {
            Vector3 tryVector;
            while (true)
            {
                tryVector = new Vector3(Random.Range(-XRANGE, XRANGE),
                    Random.Range(-YRANGE, YRANGE), LINEZVVALUE);
                if (Vector3.Distance(tryVector, lineRenderer.GetPosition(i - 1)) < MAXDISTANCEBETWEENPOINTS)
                    break;
            }
            lineRenderer.SetPosition(i, tryVector);
            Instantiate(pointPrefab, new Vector3(tryVector.x, tryVector.y, LINEPOINTZVVALUE),
                Quaternion.identity, linePoints).GetComponent<LinePoint>().Initialize(lineRenderer, i);
        }
        lineRenderer.SetPosition(lineRenderer.positionCount - 1, new Vector3(endPosition.position.x, 
            endPosition.position.y, LINEZVVALUE));
    }

    /// <summary>
    /// Проигрыш (при абсолютно любой коллизии)
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnCollisionEnter2D(Collision2D collision)
    {
        StopAllCoroutines();
        transform.position = defaultPosition;
        isMoving = false;
        GameManager.instance.AddFail();
    }

    /// <summary>
    /// Выигрыш (при входе в нужный триггер)
    /// </summary>
    /// <param name="collision">Коллизия</param>
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(exitLayer))
        {
            StopAllCoroutines();
            isMoving = true;
            GameManager.instance.CubeIsReady();
        }
    }

    /// <summary>
    /// Контролирует движение по траектории
    /// </summary>
    private IEnumerator TraectoryMovement()
    {
        isMoving = true;
        // Нулевая позиция - всегда центр объекта, поэтому начнем с 1
        int i = 1;
        while (i < lineRenderer.positionCount)
        {
            yield return new WaitForFixedUpdate();
            transform.position = Vector3.MoveTowards(transform.position, lineRenderer.GetPosition(i), 
                speed * Time.fixedDeltaTime);
            if ((Mathf.Abs(transform.position.x - lineRenderer.GetPosition(i).x) +
                Mathf.Abs(transform.position.y - lineRenderer.GetPosition(i).y)) < POSITIONERROR)
            {
                i++;
            }
        }
    }

    /// <summary>
    /// Начинает движение объекта по траектории точек из lineRenderer
    /// </summary>
    public void StartMovingTowardTheTraectory()
    {
        if (!isMoving)
        {
            StartCoroutine(TraectoryMovement());
        }
    }
}
