using UnityEngine;

public class FurnitureState : MonoBehaviour
{
    public enum State
    {
        Clean,
        Dirty
    }

    public State currentState = State.Clean;

    [Header("Visuals")]
    public GameObject cleanVisual;
    public GameObject dirtyVisual;

    void Start()
    {
        UpdateVisual();
    }

    public void SetDirty()
    {
        if (currentState == State.Dirty) return;

        currentState = State.Dirty;
        UpdateVisual();
    }

    public void SetClean()
    {
        currentState = State.Clean;
        UpdateVisual();
    }

    void UpdateVisual()
    {
        if (cleanVisual != null)
            cleanVisual.SetActive(currentState == State.Clean);

        if (dirtyVisual != null)
            dirtyVisual.SetActive(currentState == State.Dirty);
    }
}
