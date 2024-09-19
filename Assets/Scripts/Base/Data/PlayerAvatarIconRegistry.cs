using System;
using System.Collections.Generic;
using GTN.Features.PlayerBehaviour;
using UnityEngine;
using Random = System.Random;

namespace GTN.Base.Data
{
    [CreateAssetMenu(fileName = "ViewRegistry", menuName = "Registries/PlayerAvatarIconRegistry")]

    public class PlayerAvatarIconRegistry : ScriptableObject
    {
        [field: SerializeField] public List<Sprite> RealPlayerIcons { get; private set; }
        [field: SerializeField] public List<Sprite> AIPlayerIcons { get; private set; }

        public Sprite GetRandomIcon(PlayerType playerType)
        {
            var random = new Random();

            return playerType switch
            {
                PlayerType.Real => RealPlayerIcons[random.Next(RealPlayerIcons.Count)],
                PlayerType.AI => AIPlayerIcons[random.Next(AIPlayerIcons.Count)],
                _ => throw new ArgumentOutOfRangeException(nameof(playerType), playerType, null)
            };
        }
    }
}