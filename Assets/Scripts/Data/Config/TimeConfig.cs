using DG.Tweening;
using UnityEngine;
using Utils;

namespace Data.Config
{
    [CreateAssetMenu(menuName = "Config/Time Config", fileName = nameof(TimeConfig))]
    public class TimeConfig : ScriptableObjectConfigSingleton<TimeConfig>
    {
        [SerializeField]
        private Ease _timeScaleEase = Ease.Linear;

        public Ease TimeScaleEase => _timeScaleEase;

        [SerializeField]
        private float _slowMotionSpeed = 0.1f;

        public float SlowMotionSpeed => _slowMotionSpeed;

        [SerializeField]
        private float _defaultSpeed = 1f;

        public float DefaultSpeed => _defaultSpeed;

        [SerializeField]
        private float _speedDecrease = 0.005f;

        public float SpeedDecrease => _speedDecrease;
    }
}