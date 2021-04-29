using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private float raiseHeight = 1.5f;

    private Rigidbody myRigidbody;
    private Renderer myRenderer;

    private Vector3 startPosition;
    private Vector3 raisePosition;

    private bool isActivated = false;


    // Start is called before the first frame update
    void Start()
    {        
        myRigidbody = GetComponent<Rigidbody>();
        myRenderer = GetComponent<Renderer>();

        startPosition = myRigidbody.position;
        raisePosition = startPosition + new Vector3(0, raiseHeight, 0);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // TODO fix this.. idk what its doing really
        myRigidbody.velocity = Vector3.zero;
        myRigidbody.angularVelocity = Vector3.zero;        
    }
    private void SetColor(Color color)
    {
        myRenderer.material.color = color;
    }

    public void Raise()
    {
        if (isActivated)
        {
            return;
        }
        StartCoroutine(RaiseSequence());
    }
    public void Freeze()
    {
        if (isActivated)
        {
            return;
        }
        StartCoroutine(FreezeSequence());
    }
    private IEnumerator FreezeSequence()
    {
        isActivated = true;

        SetColor(Color.blue);
        yield return new WaitForSeconds(3f);
        SetColor(Color.white);

        isActivated = false;
    }
    private IEnumerator RaiseSequence()
    {
        isActivated = true;

        SetColor(Color.white);
        for(int i = 0; i < 10; i++)
        {
            yield return new WaitForSeconds(.2f);
            SetColor(Color.Lerp(Color.white, Color.red, (float)i/10));
        }
        SetColor(Color.red);  
        while (Vector3.SqrMagnitude(myRigidbody.position - raisePosition) > 0.1)
        {
            RaiseTile();
        }
        yield return new WaitForSeconds(3.0f);
        SetColor(Color.white);
        while(Vector3.SqrMagnitude(myRigidbody.position - startPosition) > 0.1)
        {
            LowerTile();
        }
        isActivated = false;
    }
    private void LowerTile()
    {
        myRigidbody.MovePosition(startPosition);        
    }
    private void RaiseTile()
    {
        // TODO raise tile is being done in update which means it probably does variable damage based on framerate
        myRigidbody.MovePosition(raisePosition);
    }
}
