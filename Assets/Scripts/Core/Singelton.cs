using UnityEngine;

namespace ComicHero
{
    /// <summary>
    ///     Base class used by all singelton components.
    /// </summary>
    /// <typeparam name="T">Type of singelton component.</typeparam>
    public class Singelton<T> : MonoBehaviour
    where T : Singelton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Destroy(gameObject);
                return;
            }

            Instance = GetComponent<T>();
        }
    }
}