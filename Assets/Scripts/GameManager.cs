using System;
using System.IO;
using UnityEngine;

public class GameManager: MonoBehaviour
{
    public GameSceneManager gameSceneManager;
    public MenuManager menuManager;
    public Camera mainCamera;
    public static GameManager Instance;
    
    void Awake()
    {
        Instance = this;
    }
    
    void Start()
    {
        RequestPassword();
    }

    private void RequestPassword()
    {
        gameSceneManager.EndGame();
        menuManager.Start();
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);
    }

    public void StartGame(String userInput)
    {
        gameSceneManager.StartGame(userInput);
        menuManager.Hide();
        mainCamera.transform.rotation = Quaternion.Euler(12, 0, 0);
    } 
    
    public void EndGame()
    {
        gameSceneManager.EndGame();
        menuManager.Show();
        mainCamera.transform.rotation = Quaternion.Euler(0, 0, 0);

    }

}