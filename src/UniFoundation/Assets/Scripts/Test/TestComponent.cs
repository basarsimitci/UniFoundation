using JoyfulWorks.UniFoundationDev.App;
using UnityEngine;

namespace JoyfulWorks.UniFoundationDev.Test
{
    public class TestComponent : MonoBehaviour
    {
        private void Awake()
        {
            ISomeInput someInput = UniFoundationDevApp.Instance.GetInput<ISomeInput>();
            someInput.SomethingHappened += OnSomethingHappened;
        }

        private void OnSomethingHappened()
        {
        }
    }
}