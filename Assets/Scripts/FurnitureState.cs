using System.Collections.Generic;
using UnityEngine;

public class FurnitureState : MonoBehaviour
{
    public enum State
    {
        Clean,
        Dirty
    }

    public State currentState = State.Clean;
    public float fearIncreasementFactor = 1;
    public float cleanFearDecreasementFactor = 1;

    [Header("Visuals")]
    public ParticleSystem cleaningEffect;

    float cleaning;

    void OnEnable()
    {
        GameManager.instance.furnitures.Add(this);
    }

    void OnDisable()
    {
        GameManager.instance.furnitures.Remove(this);
    }

    void Start()
    {
        UpdateVisual();
    }

    void Update()
    {
        if (cleaningEffect)
        {
            var emission = cleaningEffect.emission;
            emission.enabled = cleaning > 0;
        }
        cleaning = Mathf.Max(0, cleaning - Time.deltaTime);
    }

    public void SetCleaning()
    {
        cleaning = .1f;
    }

    public void SetDirty()
    {
        if (currentState != State.Dirty)
        {
            currentState = State.Dirty;
            UpdateVisual();
        }
    }

    public void SetClean()
    {
        if (currentState != State.Clean)
        {
            GameManager.instance.fearLevel -= GameManager.instance.cleaningFurnitureFearImpact * cleanFearDecreasementFactor;
            currentState = State.Clean;
            UpdateVisual();
        }
    }

    void UpdateVisual()
    {
        foreach (var renderer in GetComponentsInChildren<Renderer>())
        {
            if (!renderer.TryGetComponent<ParticleSystem>(out _))
            {
                renderer.material.SetColor("_EmissionColor", new(currentState == State.Dirty ? 8 : 0, 0, 0, 1));
            }
        }
    }
}
