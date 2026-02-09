using System;
using Unity.Properties;
using UnityEngine;
using UnityEngine.Rendering.RenderGraphModule;

namespace Assets.Scripts.App.UI
{
    public class ErrorUIScreen : UIScreen<ErrorViewData>
    {
    }

    [Serializable]
    public class ErrorViewData : ViewData
    {
        [SerializeField]
        private string _title;

        [CreateProperty]
        public string Title 
        { 
            get => _title; 
            set
            {
                if (Equals(_title, value))
                {
                    return;
                }

                _title = value;
                CommitChanges();
            } 
        }

        [SerializeField]
        private string _message;

        [CreateProperty]
        public string Message 
        { 
            get => _message; 
            set
            {
                if (Equals(_message, value))
                {
                    return;
                }

                _message = value;
                CommitChanges();
            } 
        }

        [SerializeField]
        private UIButtonViewData _closeButton;

        [CreateProperty]
        public UIButtonViewData CloseButton 
        { 
            get => _closeButton; 
            set
            {
                if (Equals(_closeButton, value))
                {
                    return;
                }

                _closeButton = value;
                CommitChanges();
            } 
        }
    }
}