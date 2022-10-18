using UnityEngine;

namespace PER.Common
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        private static T _instance;
        private static bool _shuttingDown;

        public static T Instance
        {
            get
            {
                if (!_shuttingDown)
                {
                    if (_instance == null)
                    {
                        T[] instances = FindObjectsOfType<T>();
                        if (instances.Length < 1)
                        {
#if UNITY_EDITOR
                            T component = CreateNew();
                            Debug.LogError($"<b>Singleton</b>: Wasn't able to find an instance of type {typeof(T).Name} <color=green>New created</color>", component);
#endif
                        }
                        foreach (var v in instances)
                        {
                            v.Init();
                        }
                    }
                }
                return _instance;
            }
        }

        protected virtual void Awake()
        {
            Init();
        }

        private void Init()
        {
            if (_instance == null)
            {
                _instance = GetComponent<T>();
            }
            else if (_instance != this)
            {
                Destroy(this.gameObject);
                return;
            }
        }

        protected virtual void OnApplicationQuit()
        {
            _shuttingDown = true;
        }

        protected virtual void OnDestroy()
        {
            _shuttingDown = true;
            if (_instance == GetComponent<T>())
            {
                _instance = null;
            }
        }

        private static T CreateNew() 
        {
            string name = typeof(T).Name;
            var go = new GameObject
            {
                name = name
            };
            T component = (T)go.AddComponent(typeof(T));
            return component;
        }
    }
}