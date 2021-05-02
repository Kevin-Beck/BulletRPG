using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileController : MonoBehaviour
{
    private Renderer myRenderer;

    [SerializeField] public TileElement startingTileElement;
    private TileElement currentTileElement;

    // TODO the types of tileactions can probably be cleaned up and made into the scriptable object enum thing
    void Start()
    {
        currentTileElement = startingTileElement;
        myRenderer = GetComponent<Renderer>();
        SetColor(startingTileElement.color);
    }
    

    private void SetColor(Color color)
    {
        myRenderer.material.color = color;
    }

    public IEnumerator Revert(float time)
    {
        yield return new WaitForSeconds(time);
        SetColor(startingTileElement.color);
        currentTileElement = startingTileElement;
    }

    public void ProcessChange(TileChangerConfig tileChangerConfig)
    {
        if (tileChangerConfig.tileElement.overwriteElements.Contains(currentTileElement))
        {
            StopAllCoroutines();
            SetColor(tileChangerConfig.tileElement.color);
            currentTileElement = tileChangerConfig.tileElement;
            StartCoroutine(Revert(tileChangerConfig.timeEffectStays));
        }
    }
}
