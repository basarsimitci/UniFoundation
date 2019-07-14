using System.Collections;
using UnityEngine;

namespace UniFoundation.App
{
    public interface ICoroutineRunner
    {
        Coroutine StartCoroutine(IEnumerator routine);
        void StopCoroutine(Coroutine coroutine);
    }
}
