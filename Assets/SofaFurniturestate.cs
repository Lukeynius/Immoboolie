using UnityEngine;

public class SofaFurniturestate : MonoBehaviour
{
    public enum Furniturestatus { Clean, Dirty }
    public Furniturestatus status = Furniturestatus.Clean;

    public GameObject SofaClean;
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
        SofaClean.SetActive(status == Furniturestatus.Clean);
        dirtyModel.SetActive(status == Furniturestatus.Dirty);
    }
}
