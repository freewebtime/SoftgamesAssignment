using Assets.Scripts.App.Bootstrap;
using Assets.Scripts.App.UI;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.AceOfShadows
{
    public class GameOverUIScreen : UIScreen<GameOverViewData>
    {
        public void RestartGame()
        {
            SceneLoader.RestartScene();
        }

        public void ReturnToMainMenu()
        {
            SceneLoader.Load(SceneId.MainMenu);
        }
    }

    [Serializable]
    public class GameOverViewData
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
        private List<UIButtonViewData> _buttons;

        [CreateProperty]
        public List<UIButtonViewData> Buttons { get => _buttons; set => _buttons = value; }
    }
}
