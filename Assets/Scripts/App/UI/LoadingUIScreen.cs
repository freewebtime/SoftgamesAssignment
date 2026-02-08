using System;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.App.UI
{
    public class LoadingUIScreen : UIScreen<LoadingViewData>
    {
    }

    [Serializable]
    public class LoadingViewData : ViewData
    {
        [SerializeField]
        private string _title;

        [CreateProperty]
        public string Title { get => _title; set => _title = value; }

        [SerializeField]
        private string _description;

        [CreateProperty]
        public string Description { get => _description; set => _description = value; }

        [SerializeField]
        private float _progress;

        [CreateProperty]
        public float Progress { get => _progress; set => _progress = value; }

        [CreateProperty]
        public string ProgressText => Progress.ToString("P0");
    }
}