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
        private string _description;

        [CreateProperty]
        public string Description 
        { 
            get => _description; 
            set
            {
                if (Equals(_description, value))
                {
                    return;
                }

                _description = value;
                CommitChanges();
            } 
        }

        [SerializeField]
        private float _progress;

        [CreateProperty]
        public float Progress 
        { 
            get => _progress; 
            set
            {
                if (Equals(_progress, value))
                {
                    return;
                }

                _progress = value;
                CommitChanges();
            } 
        }

        [CreateProperty]
        public string ProgressText => Progress.ToString("P0");
    }
}