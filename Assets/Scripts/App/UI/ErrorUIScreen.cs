using System;
using Unity.Properties;
using UnityEngine;

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
        public string Title { get => _title; set => _title = value; }

        [SerializeField]
        private string _message;

        [CreateProperty]
        public string Message { get => _message; set => _message = value; }

        [SerializeField]
        private UIButtonViewData _closeButton;

        [CreateProperty]
        public UIButtonViewData CloseButton { get => _closeButton; set => _closeButton = value; }
    }
}