using System.Collections;
using System.Collections.Generic;
using UnityEngine; 
using UnityEngine.SceneManagement;

public class exitLevel : MonoBehaviour
{
    float LoadDelay = 1f;
    
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.tag == "Player")
        {  
            StartCoroutine(Rload());
        }
    }
     
    IEnumerator Rload()
    {
        yield return new WaitForSecondsRealtime(LoadDelay);
        int CurrentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        int nextScene = CurrentSceneIndex + 1;
        if(nextScene == SceneManager.sceneCountInBuildSettings)
        {
            nextScene = 0;
        }
        FindObjectOfType<ScenePersist >().ResetScenePersist();
        SceneManager.LoadScene(nextScene);
    }
}