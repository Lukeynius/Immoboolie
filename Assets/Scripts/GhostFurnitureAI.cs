using UnityEngine;
using System.Collections;
using System.Linq;

public class GhostFurnitureAI : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float interactDistance = 1.2f;
    public float waitAfterDirty = 2f;
    public float searchRadius = 20f;
    public float thinkDelay = 1.5f;
    public float cooldownAfterDirty = 5f;


    private FurnitureState targetFurniture;

    void Start()
    {
        StartCoroutine(GhostLoop());
    }

    IEnumerator GhostLoop()
    {
        yield return new WaitForSeconds(2f); // startdelay

        while (true)
        {
            yield return new WaitForSeconds(thinkDelay);

            FindNewTarget();

            if (targetFurniture != null)
            {
                while (Vector3.Distance(transform.position,
                    targetFurniture.transform.position) > interactDistance)
                {
                    Vector3 dir =
                        (targetFurniture.transform.position - transform.position).normalized;
                    transform.position += dir * moveSpeed * Time.deltaTime;
                    yield return null;
                }

                targetFurniture.SetDirty();

                yield return new WaitForSeconds(cooldownAfterDirty);
            }
            else
            {
                yield return new WaitForSeconds(2f);
            }
        }
    }


    void FindNewTarget()
    {
        FurnitureState[] allFurniture =
            Object.FindObjectsByType<FurnitureState>(FindObjectsSortMode.None);


        var cleanFurniture = allFurniture
            .Where(f => f.currentState == FurnitureState.State.Clean)
            .Where(f => Vector3.Distance(transform.position, f.transform.position) < searchRadius)
            .ToArray();

        if (cleanFurniture.Length > 0)
        {
            targetFurniture =
                cleanFurniture[Random.Range(0, cleanFurniture.Length)];
        }
        else
        {
            targetFurniture = null;
        }
    }
}
