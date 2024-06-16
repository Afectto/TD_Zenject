using UnityEngine;
using Zenject;

public class GameInstaller : MonoInstaller
{
    [SerializeField]private Tower tower; 
    [SerializeField]private MoneyManager moneyManager; 
    [SerializeField]private Shop shop;
    [SerializeField] private StatusManager statusManager;

    public override void InstallBindings()
    {
        BindInstance();

        BindSingle();

        BindFactory();
    }

    private void BindInstance()
    {
        Container.BindInstance(tower);
        Container.BindInstance(moneyManager);
        Container.BindInstance(shop);
        Container.BindInstance(statusManager);
    }

    private void BindSingle()
    {
        Container.Bind<EventManager>().AsSingle().NonLazy();
        Container.Bind<BuffApplier>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponMultiplayer>().AsSingle().NonLazy();
        Container.Bind<ResourcesLoader>().AsSingle().NonLazy();
        Container.Bind<Armor>().AsSingle().NonLazy();
    }
    
    private void BindFactory()
    {
        Container.Bind<EnemyFactory>().AsSingle().NonLazy();
        Container.Bind<TowerWeaponFactory>().AsSingle().NonLazy();
    }
}