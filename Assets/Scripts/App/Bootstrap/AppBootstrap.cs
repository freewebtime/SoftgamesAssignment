using UnityEngine;

namespace Assets.Scripts.App.Bootstrap
{
    public class AppBootstrap : MonoBehaviour
    {
        [SerializeField] 
        private AppConfig Config;

        protected void Awake()
        {
            if (Config == null)
            {
                Debug.LogError("AppConfig is not assigned in the inspector.");
                return;
            }

            Application.targetFrameRate = Config.TargetFps;
        }

        protected void Start()
        {
            SceneLoader.Load(SceneId.Lobby);
        }
    }
}