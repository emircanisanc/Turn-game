using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [SerializeField]
    private Text textBestScore;

    [SerializeField]
    private CanvasGroup endScreenCanvasGroup;

    void Awake(){
        if(PlayerPrefs.GetInt("random", 0) == 1){
            textBestScore.text = PlayerPrefs.GetInt("bestScoreRandom", 0).ToString();
        }else{
            textBestScore.text = PlayerPrefs.GetInt("bestScoreNormal", 0).ToString();
        }
        
    }

    public void endGame(int score){
        if(PlayerPrefs.GetInt("random", 0) == 1){
            if(PlayerPrefs.GetInt("bestScoreRandom", 0) < score){
                PlayerPrefs.SetInt("bestScoreRandom", score);
            }
        }else{
            if(PlayerPrefs.GetInt("bestScoreNormal", 0) < score){
                PlayerPrefs.SetInt("bestScoreNormal", score);
            }
        }
        
        showEndScreen();
    }

    private void showEndScreen(){
        endScreenCanvasGroup.gameObject.SetActive(true);
    }

    public void restartRandomGame(){
        PlayerPrefs.SetInt("random", 1);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void restartDefaultGame(){
        PlayerPrefs.SetInt("random", 0);
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
