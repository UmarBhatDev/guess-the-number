using GTN.Utilities;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.InGameKeyboard
{
    [Preserve]
    public class InGameKeyboardInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<InGameKeyboardFactory>();
            Container.InstallService<InGameKeyboardService>();
        }
    }
}