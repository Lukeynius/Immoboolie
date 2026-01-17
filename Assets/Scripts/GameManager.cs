using System.Collections.Generic;
using System.Linq;
using StarterAssets;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public float timerSeconds = 300;
    public float dirtyFurnitureFearImpact = .01f;
    public float cleaningFurnitureFearImpact = .3f;
    [SerializeField] float _fearLevel;

    public GameObject victoryScreen;
    public GameObject gameoverScreen;

    public List<FurnitureState> furnitures;

    public float fearLevel
    {
        get => _fearLevel;
        set => _fearLevel = Mathf.Clamp01(value);
    }

    float initTimerSeconds;

    void Awake()
    {
        instance = this;
        initTimerSeconds = timerSeconds;
        furnitures = new List<FurnitureState>();
    }


    void Update()
    {
        timerSeconds = Mathf.Max(0, timerSeconds - Time.deltaTime);

        if (timerSeconds > 0)
        {
            fearLevel += Time.deltaTime * CalcFearIncreasement();
            if (fearLevel >= 1)
            {
                ShowGameover();
            }
        }
        else
        {
            ShowVictory();
        }
    }

    float CalcFearIncreasement()
    {
        var result = 0f;
        foreach (var furniture in furnitures)
        {
            if (furniture.currentState == FurnitureState.State.Dirty)
            {
                result += dirtyFurnitureFearImpact * furniture.fearIncreasementFactor;
            }
        }
        return result;
    }

    void BeforeShowScreen()
    {
        Time.timeScale = 0;

        Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;

        FindFirstObjectByType<StarterAssetsInputs>().cursorInputForLook = false;
    }

    public void ShowVictory()
    {
        BeforeShowScreen();
        victoryScreen.SetActive(true);
    }

    public void ShowGameover()
    {
        BeforeShowScreen();
        gameoverScreen.SetActive(true);
    }

    public void ResetGame()
    {
        victoryScreen.SetActive(false);
        gameoverScreen.SetActive(false);

        foreach (var furniture in furnitures)
        {
            furniture.SetClean();
        }

        timerSeconds = initTimerSeconds;
        fearLevel = 0;
        FindFirstObjectByType<GhostFurnitureAI>().ResetGhost();

        Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;

        Time.timeScale = 1;

        FindFirstObjectByType<StarterAssetsInputs>().cursorInputForLook = true;

        FindFirstObjectByType<RespawnPlayer>().Respawn();
    }
}
