using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCleanFurniture : MonoBehaviour
{
    public float cleanDistance = 2.5f;
    public float cleanTime = 2f;

    private float holdTimer;

    void Update()
    {
        Ray ray = new Ray(transform.position, transform.forward);

        if (Physics.Raycast(ray, out RaycastHit hit, cleanDistance))
        {
            FurnitureState furniture =
                hit.collider.GetComponentInParent<FurnitureState>();

            if (furniture != null &&
                furniture.currentState == FurnitureState.State.Dirty)
            {
                if (Keyboard.current != null &&
                    Keyboard.current.eKey.isPressed)
                {
                    holdTimer += Time.deltaTime;

                    if (holdTimer >= cleanTime)
                    {
                        furniture.SetClean();
                        holdTimer = 0f;
                    }
                }
                else
                {
                    holdTimer = 0f;
                }

                return;
            }
        }

        holdTimer = 0f;
    }
}
