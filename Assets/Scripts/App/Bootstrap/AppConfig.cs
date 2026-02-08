using UnityEngine;

namespace Assets.Scripts.App.Bootstrap
{
    [CreateAssetMenu(fileName = "AppConfig", menuName = "App/AppConfig")]
    public class AppConfig : ScriptableObject
    {
        public int TargetFps = 60;
    }
}