using System.Collections.Generic;

namespace Assets.Scripts.MagicWords
{
    public static class ConversationViewDataFactory
    {
        public static ConversationViewData Create(ConversationDTO conversationDTO)
        {
            if (conversationDTO == null)
            {
                return default;
            }

            var avatars = new Dictionary<string, AvatarViewData>();
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

                    avatars[avatarViewData.Name] = avatarViewData;
                }
            }

            var dialogueLines = new List<DialogueLineViewData>();

            if (conversationDTO.dialogue != null)
            {
                for (var dlIndex = 0; dlIndex < conversationDTO.dialogue.Length; dlIndex++)
                {
                    var dialogueLineDTO = conversationDTO.dialogue[dlIndex];
                    if (dialogueLineDTO == null)
                    {
                        continue;
                    }

                    avatars.TryGetValue(dialogueLineDTO.name, out var avatarViewData);

                    var dialogueLineViewData = new DialogueLineViewData
                    {
                        Name = dialogueLineDTO.name,
                        Text = dialogueLineDTO.text,
                        Avatar = avatarViewData,
                        Position = avatarViewData?.Position ?? AvatarPosition.Left
                    };

                    dialogueLines.Add(dialogueLineViewData);
                }
            }

            var viewData = new ConversationViewData
            {
                DialogueLines = dialogueLines,
                Avatars = avatars
            };

            return viewData;
        }
    }
}
