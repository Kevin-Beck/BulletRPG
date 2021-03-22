using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private GameObject pointer;
    [SerializeField] LayerMask cursorlayer;
    private Camera myCamera;
    private void Start()
    {
        myCamera = GetComponent<Camera>();
    }

    void Update()
    {
        Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        if (Physics.Raycast(ray, out RaycastHit hit, 10000, cursorlayer))
        {
            pointer.transform.position = new Vector3(hit.point.x, 0, hit.point.z);
        }
    }
}
