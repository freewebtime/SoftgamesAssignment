using UnityEngine.SceneManagement;

namespace Assets.Scripts.App.Bootstrap
{
    public sealed class SceneLoader
    {
        public static void Load(SceneId scene)
        {
            Load(scene.ToString());
        }

        public static void Load(string sceneName)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}