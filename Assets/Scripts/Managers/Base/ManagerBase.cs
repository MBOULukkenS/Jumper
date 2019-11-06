using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using Utils;

namespace Jumper.Managers.Base
{
    public abstract class ManagerBase : MonoBehaviour
    {
        protected bool _initialized;

        protected virtual bool DontDestroyOnLoad => false;

        public virtual void Start()
        {
            if (!_initialized)
                Initialize();
        }

        protected virtual void Initialize()
        {
            if (DontDestroyOnLoad)
                GameObject.DontDestroyOnLoad(gameObject);
            _initialized = true;
        }

        public static IEnumerable<ManagerBase> GetActiveSceneManagers()
        {
            return FindObjectsOfType<ManagerBase>();
        }
    }

    public abstract class ManagerBase<T> : ManagerBase
        where T : ScriptableObjectConfigSingleton<T>
    {
        private static readonly PropertyInfo _configInstanceField = typeof(ScriptableObjectConfigSingleton<T>)
            .GetProperty("Instance");

        [SerializeField]
        protected T _config;

        public T Config
        {
            get
            {
                if (_config == null)
                    _config = _configInstanceField.GetValue(null) as T;
                return _config;
            }
        }
    }
}