using System.Collections;
using UnityEngine;

namespace UniFoundation.App
{
    public class CoroutineRunner : ICoroutineRunner
    {
        private readonly MonoBehaviour host;
        
        public CoroutineRunner(MonoBehaviour coroutineHost)
        {
            host = coroutineHost;
        }

        public Coroutine StartCoroutine(IEnumerator routine)
        {
            if ((host != null) && (routine != null))
            {
                return host.StartCoroutine(routine);
            }

            return null;
        }

        public void StopCoroutine(Coroutine coroutine)
        {
            if ((host != null) && (coroutine != null))
            {
                host.StopCoroutine(coroutine);
            }
        }
    }
}