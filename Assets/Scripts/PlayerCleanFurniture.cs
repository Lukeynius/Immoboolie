using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCleanFurniture : MonoBehaviour
{
    public float cleanTime = 2;

    [SerializeField] float holdTimer;

    [SerializeField] Transform playerTransform;
    [SerializeField] float playerInteractionRadius = 1;
    [SerializeField] InputActionReference interactInputRef;
    [SerializeField] LayerMask interactLayerMask;

    void Update()
    {
        if (interactInputRef.action.ReadValue<float>() == 0)
        {
            holdTimer = 0;
            return;
        }

        var ray = new Ray(transform.position, transform.forward);

        var playerOverlapColliders = new Collider[10];
        Physics.OverlapSphereNonAlloc(
            playerTransform.position,
            playerInteractionRadius,
            playerOverlapColliders,
            interactLayerMask
        );

        var furniture = playerOverlapColliders
            .Where(x => x)
            .Select(x => x.GetComponentInParent<FurnitureState>())
            .Where(x => x)
            .Where(x => x.currentState == FurnitureState.State.Dirty)
            .OrderBy(x => (x.transform.position - playerTransform.position).sqrMagnitude)
            .FirstOrDefault();

        if (!furniture)
        {
            holdTimer = 0;
            return;
        }

        holdTimer += Time.deltaTime;

        if (holdTimer >= cleanTime)
        {
            holdTimer = 0;
            furniture.SetClean();
        }
        else
        {
            furniture.SetCleaning();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(playerTransform.position, playerInteractionRadius);
    }
}
