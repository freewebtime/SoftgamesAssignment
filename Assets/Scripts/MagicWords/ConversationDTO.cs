using System;

namespace Assets.Scripts.MagicWords
{
    [Serializable]
    public class ConversationDTO
    {
        public DialogueLineDTO[] dialogue;
        public AvatarDTO[] avatars;
    }
}
