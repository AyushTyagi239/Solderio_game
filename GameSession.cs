using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class GameSession : MonoBehaviour
{ [SerializeField] int lives = 3;
int Score = 0;
int addvalue =10;

[SerializeField] TextMeshProUGUI livesText;
[SerializeField] TextMeshProUGUI scoretext;
void Awake(){
    int numGameSessions = FindObjectsOfType<GameSession>().Length;
    if (numGameSessions>1){
        Destroy(gameObject);
    }
    else{
        DontDestroyOnLoad(gameObject);
    }
}
 void Start(){
    livesText.text = lives.ToString();
    scoretext.text = Score.ToString();
    

 }
public void PlayerDeath(){
    if(lives > 1){
        takelive();
    }
    else{
        restartgameSession();
    }
    void restartgameSession(){
        SceneManager.LoadScene(0);
        Destroy(gameObject);
        FindObjectOfType<ScenePersist >().ResetScenePersist();
        
    }
    void takelive(){
        lives --;
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentSceneIndex);
        livesText.text = lives.ToString();

    }
}
      public void CoinScore(){
        Score = Score + addvalue;
        scoretext.text = Score.ToString();
     }



    
   
    void Update()
    {
        
    }
}
