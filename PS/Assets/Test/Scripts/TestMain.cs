using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMain : Singleton<TestMain>
{
    [SerializeField] private Test.GameSettings gameSettings;
    [SerializeField] private Test.LevelSettings levelSettings;

    private Test.Level currentLevel;

    private void Start()
    {
        currentLevel = new Test.Level(gameSettings, levelSettings);
    }

    void Update()
    {
        // Update the world
        currentLevel.Update(UnityEngine.Time.deltaTime);

        // Render the world using debug rendering.
        currentLevel.Render();
    }
}
