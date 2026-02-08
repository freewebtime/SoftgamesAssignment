using Assets.Scripts.App.Bootstrap;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.App.UI
{
    public class MainMenuUIScreen : UIScreen<MainMenuViewData>
    {
        public void LoadScene(string sceneName)
        {
            SceneLoader.Load(sceneName);
        }

        public void LoadScene(SceneId sceneId)
        {
            SceneLoader.Load(sceneId);
        }
    }

    [Serializable]
    public class MainMenuViewData : ViewData
    {
        [SerializeField]
        private string _title = "Main Menu";

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
        public List<UIButtonViewData> MenuButtons;
    }
}