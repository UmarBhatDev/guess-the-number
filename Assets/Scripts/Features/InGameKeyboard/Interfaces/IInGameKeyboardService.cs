using System.Threading;
using Cysharp.Threading.Tasks;

namespace GTN.Features.InGameKeyboard
{
    public interface IInGameKeyboardService
    {
        UniTask<int> GetNumberFromKeyboard(CancellationToken cancellationToken);
    }
}