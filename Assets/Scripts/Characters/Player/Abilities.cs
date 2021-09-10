using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Abilities : MonoBehaviour
{
    public Camera myCamera;
    public LayerMask tileLayerMask;
    // Start is called before the first frame update
    void Start()
    {
    }
    private void Update()
    {
        //if (inputManager.GetKey(KeybindingActions.Secondary))
        //{
        //    Ray ray = myCamera.ScreenPointToRay(Input.mousePosition);

        //    if (Physics.Raycast(ray, out RaycastHit hit, 10000, tileLayerMask))
        //    {
        //        Teleport(hit.point);
        //    }
        //}
    }
    private void Teleport(Vector3 target)
    {
        Transform transform = GetComponent<Transform>();
        transform.position = new Vector3(target.x, transform.position.y, target.z);
    }
}
