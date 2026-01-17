using UnityEngine;
using System.Collections;
using System.Linq;

public class GhostFurnitureAI : MonoBehaviour
{
    [SerializeField] float moveSpeed = 2;
    [SerializeField] float interactDistance = 1.2f;
    [SerializeField] float searchRadius = 20;
    [SerializeField] float thinkDelay = 1.5f;
    [SerializeField] float cooldownAfterDirty = 5;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(2); // startdelay

        while (true)
        {
            yield return new WaitForSeconds(thinkDelay);

            if (!TryGetRandomTarget(out var furnitureState))
            {
                yield return new WaitForSeconds(2);
                continue;
            }

            while (Vector3.Distance(transform.position, furnitureState.transform.position) > interactDistance)
            {
                Vector3 dir = (furnitureState.transform.position - transform.position).normalized;
                transform.position += dir * (moveSpeed * Time.deltaTime);
                yield return new WaitForEndOfFrame();
            }

            furnitureState.SetDirty();

            yield return new WaitForSeconds(cooldownAfterDirty);
        }
    }

    bool TryGetRandomTarget(out FurnitureState furniture)
    {
        var layer = LayerMask.NameToLayer("Interactable");

        var cleanFurniture = FindObjectsByType<FurnitureState>(FindObjectsSortMode.None)
            .Where(x => x.gameObject.layer == layer)
            .Where(f => f.currentState == FurnitureState.State.Clean)
            .Where(f => (transform.position - f.transform.position).sqrMagnitude < searchRadius * searchRadius)
            .ToArray();

        if (cleanFurniture.Length > 0)
        {
            furniture = cleanFurniture[Random.Range(0, cleanFurniture.Length)];
            return true;
        }

        furniture = null;
        return false;
    }
}
