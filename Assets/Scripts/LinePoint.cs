using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Компонент для точки в line renderer одного из игроков
/// </summary>
[RequireComponent(typeof(SpriteRenderer))]
public class LinePoint : MonoBehaviour
{
    private const float LINEZINDEX = -1f;

    private Camera mainCam;
    private LineRenderer attachedRenderer;
    private int attachedPointIndex;

    /// <summary>
    /// Реализация drag and drop для точек в линии
    /// </summary>
    private void OnMouseDrag()
    {
        Vector3 mousePos = mainCam.ScreenToWorldPoint(Input.mousePosition);
        transform.position = new Vector3(mousePos.x, mousePos.y, transform.position.z);
        attachedRenderer.SetPosition(attachedPointIndex, new Vector3(mousePos.x, mousePos.y, LINEZINDEX));
    }

    /// <summary>
    /// Инициализирует необходимые данные и цвет управляемой точки
    /// </summary>
    /// <param name="renderer">Рендерер линии, к кторой привязана точка</param>
    /// <param name="index">Индекс точки</param>
    public void Initialize(LineRenderer renderer, int index)
    {
        mainCam = Camera.main;
        attachedRenderer = renderer;
        attachedPointIndex = index;
        GetComponent<SpriteRenderer>().color = renderer.startColor;
    }
}
