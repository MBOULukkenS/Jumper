using SmartData.Interfaces;
using SmartData.SmartEvent;
using UnityEngine;
using UnityEngine.Events;
using Utils;

namespace Data.Config
{
    [CreateAssetMenu(menuName = "Config/Game Config", fileName = nameof(GameConfig))]
    public class GameConfig : ScriptableObjectConfigSingleton<GameConfig>
    {
        [SerializeField]
        private GameObject _playerPrefab;

        public GameObject PlayerPrefab => _playerPrefab;
    }
}