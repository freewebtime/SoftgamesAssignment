using Assets.Scripts.App.UI;
using System;
using System.Collections.Generic;
using Unity.Properties;
using UnityEngine;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public class ConversationViewData : ViewData
    {
        [SerializeField]
        private string _title;

        [CreateProperty]
        public string Title { get => _title; set => _title = value; }

        [SerializeField]
        private List<DialogueLineViewData> _dialogueLines;

        [CreateProperty]
        public List<DialogueLineViewData> DialogueLines
        {
            get => _dialogueLines;
            set
            {
                if (Equals(_dialogueLines, value))
                {
                    return;
                }

                _dialogueLines = value;
                CommitChanges();
            }
        }

        public Dictionary<string, AvatarViewData> Avatars;

        public void FillData(ConversationDTO conversationDTO, IList<EmojiReplaceConfig> emojiReplaceConfigs)
        {
            if (conversationDTO == null)
            {
                return;
            }

            Avatars ??= new Dictionary<string, AvatarViewData>();
            Avatars.Clear();

            DialogueLines ??= new List<DialogueLineViewData>();
            DialogueLines.Clear();

            if (conversationDTO.avatars != null)
            {
                for (var aIndex = 0; aIndex < conversationDTO.avatars.Length; aIndex++)
                {
                    var avatarDTO = conversationDTO.avatars[aIndex];
                    if (avatarDTO == null)
                    {
                        continue;
                    }

                    var avatarViewData = new AvatarViewData
                    {
                        Name = avatarDTO.name,
                        Url = avatarDTO.url,
                        Position = avatarDTO.position == "left" ? AvatarPosition.Left : AvatarPosition.Right,
                        Texture = default
                    };

                    Avatars[avatarViewData.Name] = avatarViewData;
                }
            }

            if (conversationDTO.dialogue != null)
            {
                for (var dlIndex = 0; dlIndex < conversationDTO.dialogue.Length; dlIndex++)
                {
                    var dialogueLineDTO = conversationDTO.dialogue[dlIndex];
                    if (dialogueLineDTO == null)
                    {
                        continue;
                    }

                    Avatars.TryGetValue(dialogueLineDTO.name, out var avatarViewData);

                    var dialogueText = dialogueLineDTO.text;
                    if (emojiReplaceConfigs != null)
                    {
                        foreach (var kvp in emojiReplaceConfigs)
                        {
                            dialogueText = dialogueText.Replace(kvp.Pattern, kvp.Emoji);
                        }
                    }

                    var dialogueLineViewData = new DialogueLineViewData
                    {
                        Name = dialogueLineDTO.name,
                        Text = dialogueText,
                        Avatar = avatarViewData,
                        Position = avatarViewData?.Position ?? AvatarPosition.Left
                    };

                    DialogueLines.Add(dialogueLineViewData);
                }
            }
        }

        public void Clear()
        {
            Title = default;
            DialogueLines?.Clear();
            Avatars?.Clear();
        }
    }
}
