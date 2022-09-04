using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class DataStore : MonoBehaviour
{
    public GameController gameController;
    string filePath;
    public int MaxScore;

    public void Start()
    {
        filePath = Application.persistentDataPath + "/ScoreData.json";
        // print(filePath);
        Load();
    }

    public void Reset()
    {
        Save(true, 0);
        Load();
    }


    public void Save(bool IsReset, int Cur)
    {
        if (IsReset)
        {
            GameData myGameData = new GameData();
            myGameData.MaxScore = (int)0;
            myGameData.CurrentScore = (int)0;

            string json = JsonUtility.ToJson(myGameData);

            File.WriteAllText(filePath, json);
        }
        else
        {
            MaxLoad();
            // print(MaxScore);
            GameData myGameData = new GameData();
            if (MaxScore < Cur)
            {
                MaxScore = Cur;
            }
            myGameData.MaxScore = MaxScore;
            myGameData.CurrentScore = Cur;

            string json = JsonUtility.ToJson(myGameData);

            File.WriteAllText(filePath, json);
        }
        
    }

    public void Load()
    {
        if (!File.Exists(filePath)) { Reset(); return; }

        string ReadData = File.ReadAllText(filePath);
        GameData data = JsonUtility.FromJson<GameData>(ReadData);
        gameController.SetScore(data.MaxScore);
    }

    public void MaxLoad()
    {
        if (!File.Exists(filePath)) { Reset(); return; }

        string ReadData = File.ReadAllText(filePath);
        GameData data = JsonUtility.FromJson<GameData>(ReadData);
        MaxScore = data.MaxScore;
    }
}
