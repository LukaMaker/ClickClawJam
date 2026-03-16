using System.Collections;
using UnityEngine;
using System.Collections.Generic;

public class Draggable3D : MonoBehaviour
{
    [SerializeField] private LayerMask interactLayer;
    [SerializeField] private float liftHeight = 0.2f;
    [SerializeField] private float smoothSpeed = 15f;
    [SerializeField] private float dropSpeed = 10f;

    private Transform selectedResume;
    private Plane dragPlane;
    private Vector3 positionOffset;
    private HashSet<Transform> droppingResume = new HashSet<Transform>();
    private HashSet<Transform> returningResume = new HashSet<Transform>();
    // OH MYGOD
    private float originalY;

    private Transform resumeToRead;
    private Vector3 preReadPosition;
    private Quaternion preReadRotation;
    private Coroutine ReadCoroutine;

    private readonly Vector3 readPosition = new Vector3(0.0f, 1.493f, -1.199f);
    private readonly Quaternion readRotation = Quaternion.Euler(-30.4f, 0f, 0f);

    private void Update()
    {
        if (Camera.main == null) return;

        MainCameraControl cam = Camera.main.GetComponent<MainCameraControl>();
        bool isCameraAnimating = cam != null && cam.IsInAnimation();

        if (Input.GetMouseButtonDown(1) && !isCameraAnimating)
        {
            if (resumeToRead != null)
            {
                if (ReadCoroutine != null) StopCoroutine(ReadCoroutine);
                ReadCoroutine = StartCoroutine(ReturnFromRead(resumeToRead, preReadPosition, preReadRotation));
                resumeToRead = null;
            }
            else if (selectedResume == null)
            {
                Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactLayer))
                {
                    Transform hitTransform = hit.transform;
                    if (!droppingResume.Contains(hitTransform) && !returningResume.Contains(hitTransform))
                    {
                        resumeToRead = hitTransform;
                        preReadPosition = hitTransform.position;
                        preReadRotation = hitTransform.rotation;

                        if (ReadCoroutine != null) StopCoroutine(ReadCoroutine);
                        ReadCoroutine = StartCoroutine(ReadResume(resumeToRead));
                    }
                }
            }
        }

        if (Input.GetMouseButtonDown(0) && !isCameraAnimating && resumeToRead == null)
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit, 1000f, interactLayer))
            {
                Transform hitTransform = hit.transform;

                if (droppingResume.Contains(hitTransform) || returningResume.Contains(hitTransform)) return;

                selectedResume = hitTransform;
                originalY = selectedResume.position.y;

                Vector3 targetLiftedPosition = selectedResume.position;
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

        if (selectedResume != null && Input.GetMouseButton(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (dragPlane.Raycast(ray, out float enter))
            {
                Vector3 hitPoint = ray.GetPoint(enter);
                Vector3 targetPosition = hitPoint + positionOffset;
                selectedResume.position = Vector3.Lerp(selectedResume.position, targetPosition, Time.deltaTime * smoothSpeed);
            }
        }

        if (Input.GetMouseButtonUp(0) && selectedResume != null)
        {
            StartCoroutine(SmoothDrop(selectedResume, originalY));
            selectedResume = null;
        }
    }

    private IEnumerator SmoothDrop(Transform droppingObject, float targetY)
    {
        droppingResume.Add(droppingObject);
        
        Vector3 targetPos = droppingObject.position;
        targetPos.y = targetY;
        while (droppingObject != null && Vector3.Distance(droppingObject.position, targetPos) > 0.01f)
        {
            droppingObject.position = Vector3.Lerp(droppingObject.position, targetPos, Time.deltaTime * dropSpeed);
            yield return null;
        }

        if (droppingObject != null)
        {
            droppingObject.position = targetPos;
            droppingResume.Remove(droppingObject);
        }
    }

    private IEnumerator ReadResume(Transform resumeToRead)
    {
        bool reached = false;
        while (resumeToRead != null && !reached)
        {
            resumeToRead.position = Vector3.Lerp(resumeToRead.position, readPosition, Time.deltaTime * smoothSpeed);
            resumeToRead.rotation = Quaternion.Lerp(resumeToRead.rotation, readRotation, Time.deltaTime * smoothSpeed);

            if (Vector3.Distance(resumeToRead.position, readPosition) < 0.1f &&
                Quaternion.Angle(resumeToRead.rotation, readRotation) < 1f)
            {
                resumeToRead.position = readPosition;
                resumeToRead.rotation = readRotation;
                reached = true;
            }

            yield return null;
        }
    }

    private IEnumerator ReturnFromRead(Transform resume, Vector3 originalPos, Quaternion originalRot)
    {
        returningResume.Add(resume);

        while (resume != null)
        {
            resume.position = Vector3.Lerp(resume.position, originalPos, Time.deltaTime * smoothSpeed);
            resume.rotation = Quaternion.Lerp(resume.rotation, originalRot, Time.deltaTime * smoothSpeed);

            if (Vector3.Distance(resume.position, originalPos) < 0.1f &&
                Quaternion.Angle(resume.rotation, originalRot) < 1f)
            {
                resume.position = originalPos;
                resume.rotation = originalRot;
                break;
            }
            yield return null;
        }

        if (resume != null)
        {
            returningResume.Remove(resume);
        }
    }
}