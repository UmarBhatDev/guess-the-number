using GTN.Utilities;
using Zenject;

namespace GTN.Features.SceneTransitions
{
    public class CurtainInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallService<CurtainViewFactory>();
        }
    }
}