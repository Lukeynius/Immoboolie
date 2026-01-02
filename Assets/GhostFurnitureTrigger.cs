using UnityEngine;

public class GhostFurnitureTrigger : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        SofaFurniturestate furniture = other.GetComponent<SofaFurniturestate>();

        if (furniture != null && furniture.status == SofaFurniturestate.Furniturestatus.Clean)
        {
            furniture.SetDirty();
        }
    }
}
