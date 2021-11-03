using System.Collections;
using UnityEngine;

namespace JoyfulWorks.UniFoundation.App
{
    public class CoroutineProvider : MonoBehaviour
    {
        public Coroutine Start(IEnumerator coroutineEnumerator)
        {
            return StartCoroutine(coroutineEnumerator);
        }

        public void Stop(Coroutine coroutine)
        {
            StopCoroutine(coroutine);
        }
    }
}