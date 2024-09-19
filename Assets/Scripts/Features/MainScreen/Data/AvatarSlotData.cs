using GTN.Features.PlayerBehaviour;
using UnityEngine.UI;

namespace Features.MainScreen.Data
{
    public class AvatarSlotData
    {
        public Image Image;
        public PlayerType PlayerType;
        public AvatarSlotBusyness AvatarSlotBusyness;

        public AvatarSlotData(AvatarSlotBusyness avatarSlotBusyness, PlayerType playerType, Image image)
        {
            AvatarSlotBusyness = avatarSlotBusyness;
            PlayerType = playerType;
            Image = image;
        }
    }
}