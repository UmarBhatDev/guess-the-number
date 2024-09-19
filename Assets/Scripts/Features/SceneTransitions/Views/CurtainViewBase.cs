using Cysharp.Threading.Tasks;
using UnityEngine;

namespace GTN.Features.SceneTransitions
{
    public abstract class CurtainViewBase : MonoBehaviour
    {
        public abstract UniTask FadeInCompletionSource { get; }
        public abstract void FadeIn();
        public abstract void FadeOut();
    }
}