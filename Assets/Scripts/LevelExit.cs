using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class LevelExit : MonoBehaviour
{
    [SerializeField] float levelLoadDelay = 1f;
    


    // ▬▬▬▬▬▬▬▬▬▬ "On Trigger Enter 2D()" Method ▬▬▬▬▬▬▬▬▬▬
    void OnTriggerEnter2D(Collider2D other) 
    {        
       
        // ▼ Calling "Coroutine" Method 
        //      → to "Call" the "Method" (Similar to Asynchronous Methods)
        //      → that has a "Delay" in it ▼
        StartCoroutine(LoadNextLevel());
    }




    // ▬▬▬▬▬▬▬▬▬▬ "Load Next Level()" Method ▬▬▬▬▬▬▬▬▬▬
    IEnumerator LoadNextLevel()
    {
        // ▼ "Waiting" before "Load"ing the "Next Level/Scene" ▼
        yield return new WaitForSecondsRealtime(levelLoadDelay);
        
        // ▼ Getting the "Current Scene" by "Index" ▼
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        
        // ▼ "Loading" the "Next Scene" by "Index" ▼
        int nextSceneIndex = currentSceneIndex + 1;


        // ▼ "Checking" if the "Next Scene" is "Last Scene" ▼
        if (nextSceneIndex == SceneManager.sceneCountInBuildSettings)
        {
            // ▼ "Loading" the "First Scene" by "Index" ▼
            nextSceneIndex = 0;
        }


        // ▼ "Reset" the "Scene Persist" Object ▼
        FindFirstObjectByType<ScenePersist>().ResetScenePersist();

        // ▼ "Loading" the "Next Scene" by "Index" ▼    
        SceneManager.LoadScene(nextSceneIndex);
    }
}
