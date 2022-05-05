using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameStateManager : MonoBehaviour
{
    public static GameStateManager Instance;

    [HideInInspector]
    public int sheepSaved = 0;

    [HideInInspector]
    public int sheepDropped = 0;

    public int sheepDroppedBeforeGameOver;
    public SheepSpawner sheepSpawner;

    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("Title");
        }
    }

    public void SavedSheep(int points)
    {
        sheepSaved += points;
        UIManager.Instance.UpdateSheepSaved();
    }

    private void GameOver()
    {
        sheepSpawner.canSpawn = false; 
        sheepSpawner.DestroyAllSheep();
        UIManager.Instance.ShowGameOverWindow();
    }

    public void DroppedSheep(int damage)
    {
        sheepDropped+= damage;
        UIManager.Instance.UpdateSheepDropped();

        if (sheepDropped >= sheepDroppedBeforeGameOver) 
        {
            GameOver();
        }
    }
}
