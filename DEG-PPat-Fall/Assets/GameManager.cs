using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public static event System.Action<GameScene> OnGameSceneChanged;

    public GameScene Scene;

    void Awake(){
        if(Instance == null){
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }else{
            Destroy(gameObject);
        }
    }
    
    public void UpdateGameState(GameScene newScene){
        Scene = newScene;

        switch(Scene){
            case GameScene.Menu:
                break;
            case GameScene.NewGame:
                break;
            case GameScene.LoadGame:
                break;
            case GameScene.Setting:
                break;
            case GameScene.InGame:
                break;
            case GameScene.Victory:
                break;
            case GameScene.GameOver:
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(Scene), Scene, null);
        }
        
        OnGameSceneChanged?.Invoke(Scene);
    }
}

public enum GameScene{
    Menu,
    NewGame,
    LoadGame,
    Setting,
    InGame,
    Victory,
    GameOver
}