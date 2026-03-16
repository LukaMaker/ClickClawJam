using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Draggable3D : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float liftHeight = 0.2f;
    [SerializeField] private float smoothSpeed = 15f;
    [SerializeField] private float dropSpeed = 10f;

    private Plane dragPlane;
    private Vector3 positionOffset;
    private float originalY;

    private Vector3 preReadPosition;
    private Quaternion preReadRotation;
    private Coroutine activeCoroutine;

    private bool isDragging = false;
    private bool isReading = false;
    private bool isDropping = false;
    private bool isReturning = false;

    private readonly Vector3 readPosition = new Vector3(0.0f, 1.493f, -1.199f);
    private readonly Quaternion readRotation = Quaternion.Euler(-30.4f, 0f, 0f);

    private void Update()
    {
        if (Camera.main == null) return;

        MainCameraControl cam = Camera.main.GetComponent<MainCameraControl>();
        bool isCameraAnimating = cam != null && cam.IsInAnimation();

        if (Input.GetMouseButtonDown(1) && !isCameraAnimating)
        {
            if (isReading)
            {
                if (activeCoroutine != null) StopCoroutine(activeCoroutine);
                isReading = false;
                isReturning = true;
                activeCoroutine = StartCoroutine(ReturnFromRead(preReadPosition, preReadRotation));
            }
            else if (!isDragging)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactLayer))
                {
                    if (hit.transform == this.transform && !isDropping && !isReturning)
                    {
                        isReading = true;
                        preReadPosition = transform.position;
                        preReadRotation = transform.rotation;

                        if (activeCoroutine != null) StopCoroutine(activeCoroutine);
                        activeCoroutine = StartCoroutine(ReadResume());
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !isCameraAnimating && !isReading)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactLayer))
            {
                if (hit.transform == this.transform && !isDropping && !isReturning)
                {
                    isDragging = true;
                    originalY = transform.position.y;

                    Vector3 targetLiftedPosition = transform.position;
                    targetLiftedPosition.y = originalY + liftHeight;
                    dragPlane = new Plane(Vector3.up, targetLiftedPosition);

                    if (dragPlane.Raycast(ray, out float enter))
                    {
                        Vector3 hitPoint = ray.GetPoint(enter);
                        positionOffset = targetLiftedPosition - hitPoint;
                    }
                    else
                    {
                        positionOffset = Vector3.zero;
                    }
                }
            }
        }

        if (isDragging && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 targetPosition = hitPoint + positionOffset;
                transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * smoothSpeed);
            }
        }

        if (Input.GetMouseButtonUp(0) && isDragging)
        {
            isDragging = false;
            isDropping = true;
            if (activeCoroutine != null) StopCoroutine(activeCoroutine);
            activeCoroutine = StartCoroutine(SmoothDrop(originalY));
        }
    }

    private IEnumerator SmoothDrop(float targetY)
    {
        Vector3 targetPos = transform.position;
        targetPos.y = targetY;
        while (Vector3.Distance(transform.position, targetPos) > 0.01f)
        {
            transform.position = Vector3.Lerp(transform.position, targetPos, Time.deltaTime * dropSpeed);
            yield return null;
        }

        transform.position = targetPos;
        isDropping = false;
    }

    private IEnumerator ReadResume()
    {
        bool reached = false;
        while (!reached)
        {
            transform.position = Vector3.Lerp(transform.position, readPosition, Time.deltaTime * smoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, readRotation, Time.deltaTime * smoothSpeed);

            if (Vector3.Distance(transform.position, readPosition) < 0.1f &&
                Quaternion.Angle(transform.rotation, readRotation) < 1f)
            {
                transform.position = readPosition;
                transform.rotation = readRotation;
                reached = true;
            }

            yield return null;
        }
    }

    private IEnumerator ReturnFromRead(Vector3 originalPos, Quaternion originalRot)
    {
        while (true)
        {
            transform.position = Vector3.Lerp(transform.position, originalPos, Time.deltaTime * smoothSpeed);
            transform.rotation = Quaternion.Lerp(transform.rotation, originalRot, Time.deltaTime * smoothSpeed);

            if (Vector3.Distance(transform.position, originalPos) < 0.1f &&
                Quaternion.Angle(transform.rotation, originalRot) < 1f)
            {
                transform.position = originalPos;
                transform.rotation = originalRot;
                break;
            }
            yield return null;
        }

        isReturning = false;
    }
}