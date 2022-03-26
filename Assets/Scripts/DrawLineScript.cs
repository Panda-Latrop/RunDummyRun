using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
public class DrawLineScript : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler, IPointerExitHandler
{
    [SerializeField]
    protected new Camera camera;
    [SerializeField]
    protected RectTransform drawzone;
    [SerializeField]
    protected float distanceBetweenPoints = 1.0f;
    protected Vector2 lastPosition;
    [SerializeField]
    protected LineRenderer lineRenderer;
    protected bool canDrag = false;
    protected Quaternion cameraRotation;
    
    [SerializeField]
    protected GroupBuilderScript group;

    protected void Start()
    {
        cameraRotation = camera.transform.rotation;
    }

    public void OnDrag(PointerEventData eventData)
    {
        if (canDrag)
        {
            float distance = (eventData.position - lastPosition).sqrMagnitude;
            //Debug.Log(distance);
            if (distance >= distanceBetweenPoints * distanceBetweenPoints)
            {
                AddPoint(eventData.position);
                lastPosition = eventData.position;
            }
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        if (!canDrag)
        {
            group.ClearPoisitions();
            lineRenderer.positionCount = 0;
            lineRenderer.enabled = true;
            AddPoint(eventData.position);
            canDrag = true;
        }
    }
    public void OnPointerUp(PointerEventData eventData)
    {
        if (canDrag)
        {
            AddPoint(eventData.position);
            canDrag = false;
            lineRenderer.enabled = false;
            group.Regroup();
        }
    }

    protected void AddPoint(Vector3 position)
    {
        //Debug.Log(position);
        //if (RectTransformUtility.RectangleContainsScreenPoint(drawzone, uiposition))
        //if(RectTransformUtility.RectangleContainsScreenPoint(drawzone, position, camera))
        {
            position.z = 1.0f;
            position = camera.ScreenToWorldPoint(position);
            //RectTransformUtility.ScreenPointToLocalPointInRectangle(drawzone, position, camera, out local);

            lineRenderer.positionCount = group.Count + 1;
            Vector3 local = camera.transform.InverseTransformPoint(position);
            local.z = 0;
            lineRenderer.SetPosition(group.Count, local);

            position = RotatePointAroundPivot(position, camera.transform.position + camera.transform.forward, cameraRotation);
            position = position - camera.transform.position;
          
            group.AddPosition(position);
        }
    }
    public Vector3 RotatePointAroundPivot(Vector3 point, Vector3 pivot, Quaternion angles)
    {
        Vector3 dir = point - pivot; 
        dir = angles * dir;
        point = dir + pivot;
        return point;
    }
    public void OnPointerExit(PointerEventData eventData)
    {
        OnPointerUp(eventData);
        
    }
}
