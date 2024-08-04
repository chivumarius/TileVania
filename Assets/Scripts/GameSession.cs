using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Object = UnityEngine.Object;
using TMPro;


public class GameSession : MonoBehaviour
{
    [SerializeField] int playerLives = 3;
    [SerializeField] int score = 0;

    [SerializeField] TextMeshProUGUI livesText;
    [SerializeField] TextMeshProUGUI scoreText;



    // ▬▬▬▬▬▬▬▬▬▬ "Awake()" Method 
    //  →  that Implements "Singleton Pattern" ▬▬▬▬▬▬▬▬▬▬
    void Awake()
    {
        // ▼ "Finding" the "Number" of "Game Session" Object ▼
        // int numGameSessions = FindObjectsOfType<GameSession>().Length;
        int numGameSessions = Object.FindObjectsByType<GameSession>(FindObjectsSortMode.None).Length;
        

        // ▼ "Checking" if there's more than One "Game Session" Object ▼
        if (numGameSessions > 1)
        {
            // ▼ "Destroying" the "Game Session" Object ▼
            Destroy(gameObject);
        }
        else
        {
            // ▼ "Do Not Destroy" the "Game Session" Object 
            //      → when "Loading New Scene" ▼
            DontDestroyOnLoad(gameObject);
        }
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Start()" Method ▬▬▬▬▬▬▬▬▬▬
    void Start() 
    {
        // ▼ "Setting" the "Text" for "Lives" & "Score" ▼
        livesText.text = playerLives.ToString();
        scoreText.text = score.ToString();    
    }





    // ▬▬▬▬▬▬▬▬▬▬ "Process Player Death()" Method ▬▬▬▬▬▬▬▬▬▬
    public void ProcessPlayerDeath()
    {
        // ▼ "Checking" if "Player" has more than "One Life" ▼
        if (playerLives > 1)
        {
            // ▼ "Calling" the "Method" 
            //      → to "Take" - "One Life" from the "Player" ▼
            TakeLife();
        }
        else
        {
            // ▼ "Calling" the "Method" 
            //      → to "Reset" the "Game Session" ▼
            ResetGameSession();
        }
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Add To Score()" Method ▬▬▬▬▬▬▬▬▬▬
    public void AddToScore(int pointsToAdd)
    {
        // ▼ "Adding" the "Points" to the "Score" ▼
        score += pointsToAdd;
        
        // ▼ "Setting" the "Text" for "Score" ▼
        scoreText.text = score.ToString(); 
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Take Life()" Method ▬▬▬▬▬▬▬▬▬▬
    void TakeLife()
    {
        // ▼ "Taking" - "One Life" from the "Player"
        playerLives--;
        
        // ▼ "Getting" the "Current Scene" by "Index" ▼
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // ▼ "Loading" the "Current Scene" by "Index" ▼
        SceneManager.LoadScene(currentSceneIndex);

        // ▼ "Setting" the "Text" for "Lives" ▼
        livesText.text = playerLives.ToString();
    }





    // ▬▬▬▬▬▬▬▬▬▬ "Reset Game Session()" Method ▬▬▬▬▬▬▬▬▬▬
    void ResetGameSession()
    {
        // ▼ "Reset" the "Scene Persist" Object ▼
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();

        // ▼ "Reset" the "Game Session" Object ▼
        SceneManager.LoadScene(0);
        
        // ▼ "Destroy" the "Game Session" Object ▼
        Destroy(gameObject);
    }
}
