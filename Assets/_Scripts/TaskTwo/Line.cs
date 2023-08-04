using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Line : MonoBehaviour
{
    [SerializeField] private LineRenderer renderer;
    [SerializeField] private EdgeCollider2D collider;

    private readonly List<Vector2> points = new List<Vector2>();
    private readonly List<Collider2D> circles = new List<Collider2D>();
    void Start()
    {
        collider.transform.position -= transform.position;
    }

    public void SetPosition(Vector2 pos)
    {
        if (!CanAppend(pos)) return;

        points.Add(pos);

        renderer.positionCount++;
        renderer.SetPosition(renderer.positionCount - 1, pos);

        collider.points = points.ToArray();
    }

    private bool CanAppend(Vector2 pos)
    {
        if (renderer.positionCount == 0) return true;

        return Vector2.Distance(renderer.GetPosition(renderer.positionCount - 1), pos) > DrawManager.LINE_RESOLUTION;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (circles.Contains(collision)) return;

        circles.Add(collision);
    }
    public void DestroyCircles()
    {
        foreach(Collider2D circle in circles)
        {
            Destroy(circle.gameObject);
            UIManager.Instance.RestartButtonState(true);
        }
    }
    public void ResetLine()
    {
        if (gameObject != null)
            Destroy(gameObject);
    }
}