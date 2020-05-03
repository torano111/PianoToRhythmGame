using UnityEngine;
using Zenject;

namespace PianoToRhythmGame.Input
{
    [RequireComponent(typeof(PianoInputProvider))]
    public class PianoInputProviderInstaller : MonoInstaller
    {
        PianoInputProvider _provider;

        public override void InstallBindings()
        {
            this._provider = GetComponent<PianoInputProvider>();

            Container.Bind<IMidiInputProvider>().To<PianoInputProvider>().FromInstance(_provider);
        }
    }
}