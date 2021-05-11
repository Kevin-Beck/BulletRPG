using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineDropper : MonoBehaviour
{
    [Header("Mine Group")]
    [SerializeField] public Mines group;

    [Header("Drop Statistics")]
   
    [Header("Config")]
    [SerializeField] Vector3 dropPositionOffset = new Vector3(1, .25f, 1);

    [SerializeField] float mineLifeTime = 10f;
    private List<Vector3> minepositions;


    void Start()
    {
        minepositions = new List<Vector3>();
        StartCoroutine("InstantiateMines");
    }
    private void OnTriggerEnter(Collider collider)
    {
        minepositions.Add(SnapPosition(collider.ClosestPoint(transform.position), dropPositionOffset, 2f));
    }

    private IEnumerator InstantiateMines()
    {
        while (true)
        {
            yield return new WaitForSeconds(1f);
            foreach (Vector3 position in minepositions)
            {
                var mine = Instantiate(group.items[Random.Range(0, group.items.Count)], position, Quaternion.identity);
                Destroy(mine, mineLifeTime);
            }
            minepositions.Clear();
        }
    }
    
    Vector3 SnapPosition(Vector3 input, Vector3 offset, float factor = 1f)
    {
        if (factor <= 0f)
            throw new UnityException("factor argument must be above 0");

        float x = Mathf.Round(input.x / factor) * factor;
        float y = Mathf.Round(input.y / factor) * factor;
        float z = Mathf.Round(input.z / factor) * factor;

        return new Vector3(x, y, z) + offset;
    }

}
