using GTN.Utilities;
using UnityEngine.Scripting;
using Zenject;

namespace GTN.Features.PlayerBehaviour
{
    [Preserve]
    public class PlayerBehaviourInstaller : Installer
    {
        public override void InstallBindings()
        {
            Container.InstallFactory<PlayersFactory>();
            Container.InstallFactory<AIPlayerBehaviourProvider>();
            Container.InstallFactory<RealPlayerBehaviourProvider>();
        }
    }
}