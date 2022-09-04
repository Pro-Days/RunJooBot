using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public DataStore dataStore;

    [SerializeField]
    private TextMeshProUGUI textTitle;
    [SerializeField]
    private TextMeshProUGUI textTapToPlay;
    [SerializeField]
    private TextMeshProUGUI textTapToRetry;
    [SerializeField]
    private TextMeshProUGUI textScore;
    [SerializeField]
    private TextMeshProUGUI textHeart;
    [SerializeField]
    public TextMeshProUGUI textMaxScore;
    // [SerializeField]
    // public TextMeshProUGUI textTest;
    public int HeartCount = 3;

    public bool IsGameStart;
    public bool IsGameEnd;
    public bool CanGameStart;

    private void Awake()
    {
        IsGameStart = false;
        IsGameEnd = false;
        CanGameStart = false;

        textTitle.enabled = true;
        textTapToPlay.enabled = true;
        textTapToRetry.enabled = false;
        textScore.enabled = false;
        textHeart.enabled = false;
        textMaxScore.enabled = true;
    }

    private IEnumerator Start()
    {
        dataStore.Load();
        yield return new WaitForSeconds(0.5f);
        // if (Input.GetMouseButtonDown(0) == false)
        // {
        //     CanGameStart = true;
        // }

        while (true)
        {
            // textTest.text = Input.touchCount.ToString();
            if (Input.touchCount != 0)
            {
                // textTest.text = "100";
                IsGameStart = true;

                textTitle.enabled = false;
                textTapToPlay.enabled = false;
                textTapToRetry.enabled = false;
                textScore.enabled = true;
                textHeart.enabled = true;
                textMaxScore.enabled = true;

                CanGameStart = false;

                textHeart.text = new string('+', HeartCount).Replace("+", "♡");

                break;
            }

            yield return null;
        }
    }

    private void Update()
    {
        if (IsGameEnd == false) return;

        if (CanGameStart == true)
        {
            if (Input.touchCount != 0)
            {
                Reload();
            }
        }
    }

    public void IncreaseScore(float score)
    {
        int i = (int)score;
        textScore.text = "점수: " + i.ToString();
    }

    public void TextHeart()
    {
        HeartCount--;
        textHeart.text = new string('+', HeartCount).Replace("+", "♡");
    }

    public void GameOver(float score)
    {
        int i = (int)score;
    	// PlayerPrefs.SetInt("Current_Score", i);
        // if (!PlayerPrefs.HasKey("MaxScore"))
        // {
    	//     PlayerPrefs.SetInt("MaxScore", i);
        // }
        // else
        // {
        //     if (PlayerPrefs.GetInt("MaxScore") < i)
        //     {
        //         PlayerPrefs.SetInt("MaxScore", i);
        //     }
        // }

        // gameData.CurrentScore = i;
        // if (gameData.MaxScore < i)
        // {
        //     gameData.MaxScore = i;
        // }

        // print(gameData.MaxScore);
        // print(gameData.CurrentScore);

        // dataController.SaveGameData();
        dataStore.Save(false, i);

        textMaxScore.text = "최고점수: " + dataStore.MaxScore.ToString();

        IsGameStart = false;
        IsGameEnd = true;

        textHeart.enabled = false;

        StartCoroutine(SetGameStart());
    }

    public void Reload()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        dataStore.Load();
    }

    IEnumerator SetGameStart()
    {
        yield return new WaitForSeconds(1f);
        CanGameStart = true;
        textTapToRetry.enabled = true;
    }

    public void SetScore(int MaxScore)
    {
        textMaxScore.text = "최고점수: " + MaxScore.ToString();
    }
}
