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
            SceneLoader.Load(SceneId.Lobby);
        }
    }

    [Serializable]
    public class GameOverViewData: ViewData
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
        private List<UIButtonViewData> _buttons;

        [CreateProperty]
        public List<UIButtonViewData> Buttons 
        { 
            get => _buttons; 
            set
            {
                if (Equals(_buttons, value))
                {
                    return;
                }

                _buttons = value;
                CommitChanges();
            } 
        }
    }
}
