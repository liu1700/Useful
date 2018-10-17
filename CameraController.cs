using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform PlayerTransform;
    private Transform camTransform, camParentTransform;
    private float cameraDistance = 10f;

    public bool needFollow, zooming;

    private Vector3 _cameraOffset;

    private Vector3 localRotation;

    [Range(0.01f, 1.0f)]
    public float SmoothFactor = 0.5f;

    [Range(0.01f, 10.0f)]
    public float MouseSensitive = 4f;

    [Range(0.01f, 10.0f)]
    public float ScrollSensitvity = 2f;

    [Range(0.01f, 20.0f)]
    public float OrbitDampening = 10f;

    [Range(0.01f, 10.0f)]
    public float ScrollDampening = 6f;

    // Use this for initialization
    void Start()
    {
        PlayerTransform = GameObject.FindGameObjectWithTag("Player").transform;

        camTransform = transform;
        camParentTransform = transform.parent;

        _cameraOffset = transform.position - PlayerTransform.position;
    }

    // LateUpdate is called after Update
    void LateUpdate()
    {
        if (needFollow)
        {
            Vector3 newPos = PlayerTransform.position + _cameraOffset;

            transform.position = Vector3.Slerp(transform.position, newPos, SmoothFactor);
        }


        if (Input.GetMouseButton(2))
        {
            Debug.Log(Input.GetAxis("Mouse X"));
            if (Input.GetAxis("Mouse X") != 0 || Input.GetAxis("Mouse Y") != 0)
            {
                localRotation.x += Input.GetAxis("Mouse X") * MouseSensitive;
                localRotation.y -= Input.GetAxis("Mouse Y") * MouseSensitive;

                localRotation.y = Mathf.Clamp(localRotation.y, 0f, 90f);
            }
        }

        if (Input.GetAxis("Mouse ScrollWheel") != 0f)
        {
            float ScrollAmount = Input.GetAxis("Mouse ScrollWheel") * ScrollSensitvity;

            ScrollAmount *= (cameraDistance * 0.3f);

            cameraDistance += ScrollAmount * -1f;

            cameraDistance = Mathf.Clamp(cameraDistance, 1.5f, 100f);
        }

        //Actual Camera Rig Transformations
        Quaternion QT = Quaternion.Euler(localRotation.y, localRotation.x, 0);
        camParentTransform.rotation = Quaternion.Lerp(this.camParentTransform.rotation, QT, Time.deltaTime * OrbitDampening);

        if (camTransform.localPosition.z != cameraDistance * -1f)
        {
            camTransform.localPosition = new Vector3(0f, 0f, Mathf.Lerp(camTransform.localPosition.z, cameraDistance * -1f, Time.deltaTime * ScrollDampening));
        }
    }
}
