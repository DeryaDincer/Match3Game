using Zenject;

public class GameSignalsInstaller : Installer<GameSignalsInstaller>
{
    public override void InstallBindings()
    {
        SignalBusInstaller.Install(Container);

        //DeclareSignals
        Container.DeclareSignal<MoveMadeSignal>().OptionalSubscriber();
        Container.DeclareSignal<GameEndSignal>().OptionalSubscriber();
        Container.DeclareSignal<ScreenStateChangedSignal>().OptionalSubscriber();
        Container.DeclareSignal<RequestBackToMainMenuSignal>().OptionalSubscriber();
    }
}
