using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GTN.Features.SceneTransitions
{
    [AttributeCurtainType(CurtainType.NoFadeOut)]
    public class NoFadeOutCurtainView : CurtainViewBase
    {
        [SerializeField] private Image _graphic;
        [SerializeField] private float _fadeInDuration;

        private Tween _fadeTween;

        public override UniTask FadeInCompletionSource => _fadeTween.AwaitForComplete();
        
        public override void FadeIn()
        {
            _fadeTween = _graphic.DOFade(1, _fadeInDuration).From(0).SetEase(Ease.InCubic);
        }

        public override void FadeOut()
        {
            Destroy(gameObject);
        }
    }
}