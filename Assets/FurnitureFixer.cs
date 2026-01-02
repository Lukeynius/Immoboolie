using UnityEngine;

public class FurnitureFixer : MonoBehaviour
{
    public float fixTime = 2f;
    private float timer;
    private SofaFurniturestate currentFurniture;

    void Update()
    {
        if (currentFurniture != null && currentFurniture.status == SofaFurniturestate.Furniturestatus.Dirty)
        {
            if (Input.GetKey(KeyCode.E))
            {
                timer += Time.deltaTime;

                if (timer >= fixTime)
                {
                    currentFurniture.SetClean();
                    timer = 0f;
                }
            }
            else
            {
                timer = 0f;
            }
        }
    }

    void OnTriggerEnter(Collider other)
    {
        currentFurniture = other.GetComponent<SofaFurniturestate>();
    }

    void OnTriggerExit(Collider other)
    {
        if (other.GetComponent<SofaFurniturestate>() == currentFurniture)
        {
            currentFurniture = null;
            timer = 0f;
        }
    }
}
