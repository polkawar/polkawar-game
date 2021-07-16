using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Camera))]
public class CameraFollow : MonoBehaviour
{
    public Transform target;
    public Vector2 offset = new Vector2();
    public float smooth = 1f;
    private Camera cam;
    private Vector3 pos;

    private void Awake()
    {
        cam = GetComponent<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        if (target)
        {
            pos = target.position;
            pos += (Vector3)offset;
            pos.z = cam.transform.position.z;
            cam.transform.position = Vector3.Lerp(cam.transform.position, pos, Time.deltaTime * smooth);
        }
    }

    public void UpdateTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
