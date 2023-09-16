using Mono.Cecil;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class dragController : MonoBehaviour
{
    public LineRenderer lineRenderer;
    public Rigidbody2D PlayerRB;
    public GameObject Aim;

    public float DragLimit = 3f;
    public float ForceToAdd = 10f;

    private Camera Cam;
    private bool isDragging;

    Vector3 MousePosition
    {
        get 
        { 
            Vector3 pos = Cam.ScreenToWorldPoint(Input.mousePosition);
            pos.z = 0;
            return pos;
        }
    }

    private void Start()
    {
        Cam = Camera.main;
        lineRenderer.positionCount = 2;
        lineRenderer.SetPosition(0, Vector2.zero);
        lineRenderer.SetPosition(1, Vector2.zero);
        lineRenderer.enabled = false;
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0) && !isDragging)
        {
            DragStart();
        }
        if (isDragging)
        {
            Drag();
        }
        if(Input.GetMouseButtonUp(0) && isDragging)
        {
            DragEnd();
        }
        
    }

    private void DragStart()
    {
        lineRenderer.enabled = true;
        isDragging = true;
        lineRenderer.SetPosition(0, MousePosition);
    }

    private void Drag()
    {
        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 currentPos = MousePosition;

        Vector3 Distance = currentPos - startPos;

        if (Distance.magnitude <= DragLimit)
        {
            lineRenderer.SetPosition(1, currentPos);
        }
        else
        {
            Vector3 LimitVector = startPos+(Distance.normalized*DragLimit);
            lineRenderer.SetPosition(1 , LimitVector);
        }
        Aim.transform.rotation = Quaternion.FromToRotation(PlayerRB.transform.position , new Vector3 ( Distance.x , Distance.y , 0));
    }
     private void DragEnd()
    {
        isDragging = false;
        lineRenderer.enabled = false;

        Vector3 startPos = lineRenderer.GetPosition(0);
        Vector3 currentPos = lineRenderer.GetPosition(1);

        Vector3 Distance = currentPos - startPos;
        Vector3 finalForce = Distance * ForceToAdd;

        PlayerRB.AddForce(-finalForce , ForceMode2D.Impulse);
    }
}
