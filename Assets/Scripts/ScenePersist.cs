using UnityEngine;


public class ScenePersist : MonoBehaviour
{
    // ▬▬▬▬▬▬▬▬▬▬ "Awake()" Method ▬▬▬▬▬▬▬▬▬▬
    void Awake()
    {       
       
        // ▼ "Find All Objects" of "Type" - "ScenePersist" ▼
        ScenePersist[] scenePersists = Object.FindObjectsByType<ScenePersist>(FindObjectsInactive.Exclude, FindObjectsSortMode.None);

        // ▼ "Get" the "Number" of "Objects Found" ▼
        int numScenePersists = scenePersists.Length;



        // ▼ "Checking" if there's more than One "Scene Persist" Object ▼
        if (numScenePersists > 1)
        {
            // ▼ "Destroying" the "Scene Persist" Object ▼
            Destroy(gameObject);
        }
        else
        {
            // ▼ "Do Not Destroy" the "Scene Persist" Object
            DontDestroyOnLoad(gameObject);
        }
    }



    // ▬▬▬▬▬▬▬▬▬▬ "Reset Scene Persist()" Method ▬▬▬▬▬▬▬▬▬▬
    public void ResetScenePersist()
    {
        // ▼ "Destroy" the "Scene Persist" Object ▼
        Destroy(gameObject);
    }
}
