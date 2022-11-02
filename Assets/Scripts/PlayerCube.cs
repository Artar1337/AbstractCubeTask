using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Контролирует OnCollisionEnter и следование по траектории для кубика
/// </summary>
[RequireComponent(typeof(LineRenderer))]
public class PlayerCube : MonoBehaviour
{
    private const float POSITIONERROR = 0.01f;

    [SerializeField] private string exitLayer;
    [SerializeField] private float speed = 1f;

    private LineRenderer lineRenderer;

    private void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // TODO: проигрыш
        Debug.Log("cube with exit layer " + exitLayer + " lost...");
        StopAllCoroutines();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == LayerMask.NameToLayer(exitLayer))
        {
            // TODO: выигрыш
            Debug.Log("cube with exit layer " + exitLayer + " won!");
            StopAllCoroutines();
        }
    }

    private IEnumerator TraectoryMovement()
    {
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
        StartCoroutine(TraectoryMovement());
    }
}
