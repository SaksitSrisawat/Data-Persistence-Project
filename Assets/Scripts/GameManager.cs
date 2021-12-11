using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.IO;

#if UNITY_EDITOR
using UnityEditor;
#endif

public class GameManager : MonoBehaviour
{
    #region Singleton
    public static GameManager Instance;

    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }
    #endregion

    public TMP_InputField inputName;
    public TextMeshProUGUI scoreText;

    public string playerName;
    public string newName;
    int bestScore;

    private void Start()
    {
        OnReadData();
        scoreText.text = "Best Score : "+ playerName + " : " + bestScore;
    }

    public void SetBestScore(int score)
    {
        bestScore = score;
    }

    public int GetBestScore()
    {
        return bestScore;
    }

    public void GetName()
    {
        newName = inputName.text;
        Debug.Log(playerName);
    }

    public void OnStart()
    {
        SceneManager.LoadScene(1);
    }

    public void onQuitGame()
    {

#if UNITY_EDITOR
        EditorApplication.ExitPlaymode();
#else
        Application.Quit();
#endif

    }

    [System.Serializable]
    class SaveData
    {
        public string name;
        public int b_Score;
    }

    public void OnSeveData()
    {
        SaveData data = new SaveData();
        data.name = newName;
        data.b_Score = bestScore;

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);

    }

    public void OnReadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerName = data.name;
            bestScore = data.b_Score;
        }

    }

    public void OnDeleteData()
    {
        string name = "Name";
        int score = 0;

        newName = name;
        bestScore = score;

        OnSeveData();

        scoreText.text = "Best Score : " + newName + " : " + bestScore;
    }
}
