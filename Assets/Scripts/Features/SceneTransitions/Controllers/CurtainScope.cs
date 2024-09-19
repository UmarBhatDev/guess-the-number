using System;
using Cysharp.Threading.Tasks;

namespace GTN.Features.SceneTransitions
{
    public struct CurtainScope : IDisposable
    {
        public readonly UniTask FadeIn => _viewBase.FadeInCompletionSource;
        private readonly CurtainViewBase _viewBase;
            
        public CurtainScope(CurtainViewBase viewBase)
        {
            _viewBase = viewBase;
            
            _viewBase.FadeIn();
        }

        public void Dispose()
        {
            _viewBase.FadeOut();
        }
    }
}