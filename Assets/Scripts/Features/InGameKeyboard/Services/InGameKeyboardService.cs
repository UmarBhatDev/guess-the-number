using System.Threading;
using Cysharp.Threading.Tasks;

namespace GTN.Features.InGameKeyboard
{
    public class InGameKeyboardService : IInGameKeyboardService
    {
        private readonly InGameKeyboardFactory _inGameKeyboardFactory;

        private InGameKeyboardView _inGameKeyboardView;

        public InGameKeyboardService(InGameKeyboardFactory inGameKeyboardFactory)
        {
            _inGameKeyboardFactory = inGameKeyboardFactory;
        }

        public async UniTask<int> GetNumberFromKeyboard(CancellationToken cancellationToken)
        {
            var inputCompletionSource = new UniTaskCompletionSource<int>();
            
            _inGameKeyboardView ??= _inGameKeyboardFactory.Create();

            _inGameKeyboardView.OnSubmitPressed += result => inputCompletionSource.TrySetResult(result);

            await _inGameKeyboardView.Show(cancellationToken);
            
            var result = await inputCompletionSource.Task;

            await _inGameKeyboardView.Hide(cancellationToken);

            return result;
        }
    }
}