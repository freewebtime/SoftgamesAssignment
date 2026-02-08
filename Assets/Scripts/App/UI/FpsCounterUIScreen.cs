using System;
using System.Diagnostics;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.App.UI
{
    public class FpsCounterUIScreen : UIScreen<FpsCounterViewData>
    {
        [SerializeField]
        private float _fps = 0;

        public float FPS 
        { 
            get => _fps; 
            set
            {
                if (Equals(_fps, value))
                {
                    return;
                }

                _fps = value;
                UpdateViewData();
            } 
        }

        public int FramesToMeasure = 60;

        private Stopwatch _stopwatch;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
        }

        protected void Update()
        {
            CalculateFPS();
        }

        private void CalculateFPS()
        {
            _stopwatch ??= new Stopwatch();
            if (!_stopwatch.IsRunning)
            {
                _stopwatch.Start();
            }

            if (Time.frameCount % FramesToMeasure == 0)
            {
                _stopwatch.Stop();
                float totalSeconds = (float)_stopwatch.Elapsed.TotalSeconds;

                FPS = totalSeconds > 0 ? FramesToMeasure / totalSeconds : FramesToMeasure;

                _stopwatch.Reset();
                _stopwatch.Start();
            }
        }

        private void UpdateViewData()
        {
            if (ViewData != null)
            {
                ViewData.Text = $"FPS: {FPS:0.00}";
            }
        }
    }

    [Serializable]
    public class FpsCounterViewData : ViewData
    {
        [SerializeField]
        private string _text;

        [CreateProperty]
        public string Text 
        { 
            get => _text; 
            set
            {
                if (Equals(_text, value))
                {
                    return;
                }

                _text = value;
                CommitChanges();
            } 
        }
    }
}