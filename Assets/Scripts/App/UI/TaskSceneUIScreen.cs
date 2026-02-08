using Assets.Scripts.App.Bootstrap;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.App.UI
{
    public class TaskSceneUIScreen : UIScreen<TaskSceneViewData>
    {
        public void ToggleMenu()
        {
            ViewData?.ToggleMenu();
        }

        public void Resume()
        {
            ViewData?.HideMenu();
        }

        public void ReturnToMainMenu()
        {
            SceneLoader.Load(SceneId.MainMenu);
        }

        public void RestartScene()
        {
            SceneLoader.RestartScene();
        }
    }

    [Serializable]
    public class TaskSceneViewData : ViewData
    {
        [SerializeField]
        private string _menuTitle = "Task Menu";

        [CreateProperty]
        public string MenuTitle
        {
            get => _menuTitle;
            set
            {
                if (Equals(_menuTitle, value))
                {
                    return;
                }

                _menuTitle = value;
                CommitChanges();
            }
        }

        [SerializeField]
        private UIButtonViewData _toggleMenuButton;

        [CreateProperty]
        public UIButtonViewData ToggleMenuButton { get => _toggleMenuButton; set => _toggleMenuButton = value; }

        [SerializeField]
        private List<UIButtonViewData> _menuButtons;

        [CreateProperty]
        public List<UIButtonViewData> MenuButtons { get => _menuButtons; set => _menuButtons = value; }


        [SerializeField]
        private DisplayStyle _menuDisplayStyle = DisplayStyle.None;
        
        [CreateProperty]
        public DisplayStyle MenuDisplayStyle { 
            get => _menuDisplayStyle; 
            set
            {
                if (Equals(_menuDisplayStyle, value))
                {
                    return;
                }

                _menuDisplayStyle = value;
                CommitChanges();
            } 
        }

        public void ToggleMenu()
        {
            switch (_menuDisplayStyle)
            {
                case DisplayStyle.Flex:
                    HideMenu();
                    break;

                case DisplayStyle.None:
                    ShowMenu();
                    break;
            }
        }

        public void HideMenu()
        {
            MenuDisplayStyle = DisplayStyle.None;
        }

        public void ShowMenu()
        {
            MenuDisplayStyle = DisplayStyle.Flex;
        }
    }
}