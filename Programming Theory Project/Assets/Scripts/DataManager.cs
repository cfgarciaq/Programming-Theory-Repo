using UnityEngine;

public class DataManager : MonoBehaviour
{
    public static DataManager Instance;
    public string FarmName { get; set; }

    private AnimalData AnimalData { get; set; }

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    // Save system to implement if needed

}



/* 
     * SAVE SYSTEM CODE EXEMPLE 
     * 
        [System.Serializable]
        private class BestPlayer
        {
            public string Name;
            public int Score;
        }

        private void SaveScore()
        {
            try
            {
                BestPlayer best = new BestPlayer();
                best.Name = BestPlayerName;
                best.Score = HigestScore;

                string json_bestScore = JsonUtility.ToJson(best);

                File.WriteAllText($"{Application.persistentDataPath}/savedBestScore.json", json_bestScore);
            }
            catch (Exception e)
            {
                Debug.Log($"Error: {e} \nCouldn't save json file");
            }
        }

        private void LoadScore()
        {
            string path = $"{Application.persistentDataPath}/savedBestScore.json";

            if (File.Exists(path))
            {
                string json_savedBestScore = File.ReadAllText(path);
                BestPlayer bestPlayer = JsonUtility.FromJson<BestPlayer>(json_savedBestScore);

                HigestScore = bestPlayer.Score;
                BestPlayerName = bestPlayer.Name;
            }
            else
            {
                HigestScore = 0;
                BestPlayerName = "NoHighScore";
            }
        }
     *
     */
