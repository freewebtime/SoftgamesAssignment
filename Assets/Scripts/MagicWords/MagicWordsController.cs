using Assets.Scripts.App.UI;
using System;
using System.Collections;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    public class MagicWordsController : MonoBehaviour
    {
        public ConversationUIScreen ConversationScreen;
        public ErrorUIScreen ErrorScreen;
        public LoadingUIScreen LoadingScreen;

        [SerializeField]
        private MagicWordsConfig _config;

        private IEnumerator Start()
        {
            yield return LoadAndParseConversation();
        }

        private IEnumerator LoadAndParseConversation()
        {
            if (_config == null || string.IsNullOrEmpty(_config.EndpointUrl))
            {
                Debug.LogError("MagicWordsController: Config is not set or EndpointUrl is empty.");
                yield break;
            }

            var loadingViewData = ShowLoadingScreen("Loading...", "Please wait while the data is loading", 0);

            var url = _config.EndpointUrl;
            
            var webRequest = UnityEngine.Networking.UnityWebRequest.Get(url);
            webRequest.SendWebRequest();

            yield return null;

            if (GetIsSimulateSlowInternetConnection())
            {
                yield return new WaitForSeconds(GetSlowInternetConnectionDelay());
            }

            while (!webRequest.isDone)
            {
                if (loadingViewData != null)
                {
                    loadingViewData.Progress = webRequest.downloadProgress;
                }
                yield return null;
            }

            HideLoadingScreen();

            switch (webRequest.result)
            {
                case UnityEngine.Networking.UnityWebRequest.Result.Success:
                    yield return ProcessConversationJson(webRequest.downloadHandler.text);
                    break;

                case UnityEngine.Networking.UnityWebRequest.Result.ConnectionError:
                case UnityEngine.Networking.UnityWebRequest.Result.DataProcessingError:
                case UnityEngine.Networking.UnityWebRequest.Result.ProtocolError:
                    Debug.LogError($"MagicWordsController: Error downloading JSON from {url}. Error: {webRequest.error}");
                    ShowErrorScreen("Error", $"Failed to load data from url {url}:\r\n{webRequest.error}");
                    break;
            }
        }

        private IEnumerator ProcessConversationJson(string json)
        {
            if (ConversationScreen == null)
            {
                Debug.LogError("MagicWordsController: ConversationScreen reference is not set.");
                yield break;
            }

            ConversationViewData viewData = null;

            try
            {
                var conversationDTO = JsonUtility.FromJson<ConversationDTO>(json);
                viewData = ConversationScreen.ViewData ?? new ConversationViewData();

                // show dialog
                viewData.FillData(conversationDTO, _config.EmojiReplaceConfigs);
            }
            catch (Exception ex)
            {
                Debug.LogError($"MagicWordsController: Exception while processing conversation JSON. Exception: {ex}");
                ShowErrorScreen("Error", $"An error occurred while processing the data:\r\n{ex.Message}");
            }

            ConversationScreen.ViewData = viewData;
            ConversationScreen.Show();

            // now download avatar textures
            yield return DownloadAvatarTextures(viewData);
        }

        private IEnumerator DownloadAvatarTextures(ConversationViewData viewData)
        {
            if (viewData == null || viewData.Avatars == null)
            {
                yield break;
            }
            
            foreach (var avatarEntry in viewData.Avatars)
            {
                // uncomment this if you want to simulate slow internet connection
                if (GetIsSimulateSlowInternetConnection())
                {
                    yield return new WaitForSeconds(GetSlowInternetConnectionDelay());
                }

                var avatar = avatarEntry.Value;
                if (avatar == null || string.IsNullOrEmpty(avatar.Url) || avatar.Texture != null)
                {
                    continue;
                }
                var webRequest = UnityEngine.Networking.UnityWebRequestTexture.GetTexture(avatar.Url);
                webRequest.SendWebRequest();
                while (!webRequest.isDone)
                {
                    yield return null;
                }
            
                if (webRequest.result == UnityEngine.Networking.UnityWebRequest.Result.Success)
                {
                    var texture = ((UnityEngine.Networking.DownloadHandlerTexture)webRequest.downloadHandler).texture;
                    avatar.Texture = texture;
                }
                else
                {
                    Debug.LogError($"MagicWordsController: Error downloading avatar texture from {avatar.Url}. Error: {webRequest.error}");
                    //ShowErrorScreen("Error", $"Failed to load avatar texture from url {avatar.Url}:\r\n{webRequest.error}");
                }
            }
        }

        private bool GetIsSimulateSlowInternetConnection()
        {
            if (_config == null)
            {
                return false;
            }

            return _config.IsSimulateSlowInternetConnection;
        }

        private float GetSlowInternetConnectionDelay()
        {
            if (_config == null)
            {
                return 0;
            }

            return _config.SlowInternetConnectionDelay;
        }

        private LoadingViewData ShowLoadingScreen(string title, string description, float progress)
        {
            if (LoadingScreen == null)
            {
                Debug.LogError("MagicWordsController: LoadingScreen reference is not set.");
                return null;
            }

            var loadingViewData = LoadingScreen.ViewData ?? new LoadingViewData
            {
                Title = title,
                Description = description,
                Progress = progress
            };

            LoadingScreen.ViewData = loadingViewData;
            LoadingScreen.Show();

            return loadingViewData;
        }

        private void HideLoadingScreen()
        {
            if (LoadingScreen != null)
            {
                LoadingScreen.Hide();
            }
        }

        private ErrorViewData ShowErrorScreen(string title, string message)
        {
            if (ErrorScreen == null)
            {
                Debug.LogError("MagicWordsController: ErrorScreen reference is not set.");
                return null;
            }

            var errorViewData = ErrorScreen.ViewData ?? new ErrorViewData
            {
                Title = title,
                Message = message
            };

            ErrorScreen.ViewData = errorViewData;
            ErrorScreen.Show();

            return errorViewData;
        }

        private void HideErrorScreen()
        {
            if (ErrorScreen != null)
            {
                ErrorScreen.Hide();
            }
        }
    }
}
