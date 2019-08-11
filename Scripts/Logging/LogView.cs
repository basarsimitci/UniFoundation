using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UniFoundation.Logging
{
    public class LogView : MonoBehaviour, ILogOutput
    {
        [SerializeField] protected TextMeshProUGUI textMesh;
        [SerializeField] protected Button closeButton;
        [SerializeField] protected string[] logCategories;
        [SerializeField] protected LogLevel outputLogLevel;
        [SerializeField] protected bool startHidden;

        public virtual void OutputLog(string logCategory, string log, LogLevel logLevel)
        {
            if (logLevel >= outputLogLevel)
            {
                if (textMesh != null)
                {
                    textMesh.text = log;
                    gameObject.SetActive(true);
                }
            }
        }

        protected virtual void Awake()
        {
            if (logCategories != null)
            {
                foreach (string category in logCategories)
                {
                    Log.RegisterLogOutput(this, category);
                }
            }

            if (closeButton != null)
            {
                closeButton.onClick.AddListener(OnCloseClicked);
            }

            if (startHidden)
            {
                gameObject.SetActive(false);
            }
        }

        protected virtual void OnDestroy()
        {
            if (logCategories != null)
            {
                foreach (string category in logCategories)
                {
                    Log.UnregisterLogOutput(this, category);
                }
            }
        }

        protected virtual void OnCloseClicked()
        {
            gameObject.SetActive(false);
        }
    }
}