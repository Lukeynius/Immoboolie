using UnityEngine;

public class BathroomToiletFurniturestate : MonoBehaviour
{
    public enum Furniturestatus { Clean, Dirty }
    public Furniturestatus status = Furniturestatus.Clean;

    public GameObject cleanModel;
    public GameObject dirtyModel;

    void Start()
    {
        UpdateVisual();
    }

    public void SetDirty()
    {
        status = Furniturestatus.Dirty;
        UpdateVisual();
    }

    public void SetClean()
    {
        status = Furniturestatus.Clean;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        cleanModel.SetActive(status == Furniturestatus.Clean);
        dirtyModel.SetActive(status == Furniturestatus.Dirty);
    }
}
