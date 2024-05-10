using Data;
using UnityEngine;

namespace Core.Services.GameDataService
{
    public class GameDataService : IGameDataService
    {
        private GameData _gameData;

        public GameData GameData => _gameData;

        public GameDataService()
        {
            LoadGameData();
        }

        public void LoadGameData()
        {
            string gameDataJson = PlayerPrefs.GetString("GameData", "{}");
            _gameData = JsonUtility.FromJson<GameData>(gameDataJson);
            if (_gameData == null)
            {
                _gameData = new GameData { timeScale = 1 };
            }
        }

        public void SaveGameData()
        {
            string gameDataJson = JsonUtility.ToJson(_gameData);
            PlayerPrefs.SetString("GameData", gameDataJson);
            PlayerPrefs.Save();
        }
    }
}