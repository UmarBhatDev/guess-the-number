using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace GTN.Features.SceneTransitions
{
    [AttributeCurtainType(CurtainType.BlackFade)]
    public class BlackFadeCurtainViewBase : CurtainViewBase
    {
        [SerializeField] private Image _graphic;
        [SerializeField] private float _fadeInDuration;
        [SerializeField] private float _fadeOutDuration;

        private Tween _fadeTween;

        public override UniTask FadeInCompletionSource => _fadeTween.AwaitForComplete();
        
        public override void FadeIn()
        {
            _fadeTween = _graphic.DOFade(1, _fadeInDuration).From(0).SetEase(Ease.InCubic);
        }

        public override void FadeOut()
        {
            FadeOutAsync().Forget();

            async UniTask FadeOutAsync()
            {
                await _graphic.DOFade(0, _fadeOutDuration).AwaitForComplete();
                Destroy(gameObject);
            }
        }
    }
}