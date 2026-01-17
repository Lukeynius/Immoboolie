using UnityEngine;

public class FearLevelDisplay : MonoBehaviour
{
    void Update()
    {
        transform.localScale = new(Mathf.Clamp01(GameManager.instance.fearLevel), 1, 1);
    }
}
