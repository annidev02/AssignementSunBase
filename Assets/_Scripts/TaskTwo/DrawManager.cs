using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawManager : Singelton<DrawManager>
{
    [SerializeField] private Line line;
    public Line current;
    private Camera cam;

    public const float LINE_RESOLUTION = 0.3f;

    private void Start()
    {
        cam = Camera.main;
    }
    private void Update()
    {
        Vector2 mousePosition = cam.ScreenToWorldPoint(Input.mousePosition);
        if (Input.GetMouseButtonDown(0))
        {
            current = Instantiate(line, mousePosition, Quaternion.identity);
        }
        if (Input.GetMouseButton(0)) 
        {
            current.SetPosition(mousePosition);
        }
        if (Input.GetMouseButtonUp(0))
        {
            current.DestroyCircles();
            this.enabled = false;
            Destroy(current.gameObject);
        }
    }

}
