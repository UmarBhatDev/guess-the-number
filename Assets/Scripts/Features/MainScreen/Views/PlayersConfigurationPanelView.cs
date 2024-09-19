using System;
using System.Collections.Generic;
using System.Linq;
using Features.MainScreen.Data;
using GTN.Base.Data;
using GTN.Features.PlayerBehaviour;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace GTN.Features.MainScreen
{
    public class PlayersConfigurationPanelView : MonoBehaviour
    {
        public event Action<bool> OnAvailabilityChanged;
        
        [SerializeField] private Button _addAIPlayer;
        [SerializeField] private Button _addRealPlayer;
        [SerializeField] private Button _removeAIPlayer;
        [SerializeField] private Button _removeRealPlayer;
        [SerializeField] private Sprite _notSelectedSprite;
        [SerializeField] private List<Image> _playerIcons;

        private int _realPlayersCount;
        private int _aiPlayersCount;

        private List<AvatarSlotData> _avatarSlots;

        [Inject] private PlayerAvatarIconRegistry _avatarIconRegistry;

        private void Awake()
        {
            _avatarSlots = new List<AvatarSlotData>();
        }

        private void Start()
        {
            foreach (var playerIcon in _playerIcons)
                _avatarSlots.Add(new AvatarSlotData(AvatarSlotBusyness.Free, PlayerType.None, playerIcon));
            
            _addAIPlayer.onClick.AddListener(() => AddPlayer(PlayerType.AI));
            _addRealPlayer.onClick.AddListener(() => AddPlayer(PlayerType.Real));
            _removeAIPlayer.onClick.AddListener(() => RemovePlayer(PlayerType.AI));
            _removeRealPlayer.onClick.AddListener(() => RemovePlayer(PlayerType.Real));
            
            UpdateButtons();
        }

        public (int aiPlayers, int realPlayers) GetInputData() 
            => (_aiPlayersCount, _realPlayersCount);

        private void AddPlayer(PlayerType playerType)
        {
            if (_realPlayersCount + _aiPlayersCount >= 4)
                return;
            
            if (!_avatarSlots.Any(x => x.AvatarSlotBusyness is AvatarSlotBusyness.Free))
                return;
            
            var avatarSlotData = _avatarSlots.FirstOrDefault(x =>
                x.PlayerType == PlayerType.None && x.AvatarSlotBusyness == AvatarSlotBusyness.Free);

            if (avatarSlotData is null) return;
            
            if (playerType == PlayerType.Real)
                _realPlayersCount++;
            else if (playerType == PlayerType.AI)
                _aiPlayersCount++;

            avatarSlotData.PlayerType = playerType;
            avatarSlotData.AvatarSlotBusyness = AvatarSlotBusyness.Selected;
            avatarSlotData.Image.sprite = _avatarIconRegistry.GetRandomIcon(playerType);

            UpdateButtons();
            OnAvailabilityChanged?.Invoke(_aiPlayersCount + _realPlayersCount > 1);
        }
        
        private void RemovePlayer(PlayerType playerType)
        {
            if (_realPlayersCount + _aiPlayersCount <= 0)
                return;
            if (_avatarSlots.All(x => x.AvatarSlotBusyness is AvatarSlotBusyness.Free))
                return;
            
            var avatarSlotData = _avatarSlots.LastOrDefault(x => x.PlayerType == playerType);

            if (avatarSlotData is null) return;
            
            if (playerType == PlayerType.Real)
                _realPlayersCount--;
            else if (playerType == PlayerType.AI)
                _aiPlayersCount--;

            avatarSlotData.PlayerType = PlayerType.None;
            avatarSlotData.Image.sprite = _notSelectedSprite;
            avatarSlotData.AvatarSlotBusyness = AvatarSlotBusyness.Free;
            
            UpdateButtons();
            OnAvailabilityChanged?.Invoke(_aiPlayersCount + _realPlayersCount > 1);
        }

        private void UpdateButtons()
        {
            _removeAIPlayer.interactable = _aiPlayersCount != 0;
            _removeRealPlayer.interactable = _realPlayersCount != 0;

            _addRealPlayer.interactable = _addAIPlayer.interactable = _realPlayersCount + _aiPlayersCount != 4;
        }
    }
}