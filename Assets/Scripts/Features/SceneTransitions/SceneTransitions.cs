using Cysharp.Threading.Tasks;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceProviders;

namespace GTN.Features.SceneTransitions
{
    public delegate UniTask AdditionalTask();
    public delegate AsyncOperationHandle<SceneInstance> AsyncLoadSceneAction();
    public static class Transition
    {
        public static UniTask ToScene(string sceneName, CurtainViewFactory curtainViewFactory, 
            CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
            => ToScene(
                () => Addressables.LoadSceneAsync(sceneName),
                curtainViewFactory,
                curtain,
                additionalTask);
        
        private static async UniTask ToScene(AsyncLoadSceneAction action, CurtainViewFactory curtainViewFactory, 
            CurtainType curtain = CurtainType.BlackFade, AdditionalTask additionalTask = default)
        {
            var curtainView = curtainViewFactory.Create(curtain);
            
            using var transition = new CurtainScope(curtainView);
            
            await transition.FadeIn;
            
            var handle = action.Invoke();
            var task = new UniTaskCompletionSource();
            
            if (additionalTask != null) 
                await additionalTask.Invoke();

            handle.Completed += _ => task.TrySetResult();

            await task.Task;

            await handle.Result.ActivateAsync();
        }
    }
}