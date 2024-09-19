using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.UI;

namespace GTN.Features.GameScreen
{
    public class AvatarIconView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private GameObject _playerOutline;
        
        public async UniTask Raise(CancellationToken cancellationToken)
        {
            _playerOutline.SetActive(true);
        }

        public async UniTask Hide(CancellationToken cancellationToken)
        {
            _playerOutline.SetActive(false);
        }

        public void SetSprite(Sprite sprite)
        {
            _image.sprite = sprite;
        }
    }
}