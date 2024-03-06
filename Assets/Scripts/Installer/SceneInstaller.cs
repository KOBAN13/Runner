using Character;
using Character.Collisions;
using Character.Physics;
using Character.PlayerJumpController;
using InputSystem;
using UnityEngine;
using UnityEngine.Serialization;
using Zenject;

public class SceneInstaller : MonoInstaller
{
    [field: SerializeField] public Player Player { get; private set; }
    [field: SerializeField] public InputSystemPC inputSystemPC;
    [field: SerializeField] public CharacterInputController characterController;
    [field: SerializeField] public PlayerMovementController movementController;
    [field: SerializeField] public PlayerJumpController jumpController;
    [field: SerializeField] public CoroutineRunner coroutineRunner;
    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindInputInterface();
        BindAllInterface();
        BindPhysics();
        BindCollisionHandler();
        BindCourutine();
        BindStopMove();
    }
    
    //IConfigable, IAnimator, IUseConfigable, IStopeMove
    
    private void BindAllInterface()
    {
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromInstance(movementController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerJumpController>().FromInstance(jumpController).AsCached().NonLazy();
    }

    private void BindCollisionHandler()
    {
        Container.BindInterfacesAndSelfTo<CollisionHandler>().AsSingle().NonLazy();
    }

    private void BindPhysics()
    {
        Container.BindInterfacesAndSelfTo<Gravity>().AsSingle().NonLazy();
    }

    private void BindCourutine()
    {
        Container.BindInterfacesAndSelfTo<CoroutineRunner>().FromInstance(coroutineRunner).AsSingle();
        Container.BindInterfacesAndSelfTo<CoroutineHelper>().AsSingle().NonLazy();
    }

    private void BindStopMove()
    {
        Container.BindInterfacesAndSelfTo<StopMovements>().AsSingle().NonLazy();
    }
    
    private void BindInputInterface()
    {
        Container.BindInterfacesAndSelfTo<NewInputSystem>().AsSingle().NonLazy();
    }

    private void BindInput()
    {
        Container.BindInterfacesAndSelfTo<CharacterInputController>().FromInstance(characterController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<InputSystemPC>().FromInstance(inputSystemPC).AsCached().NonLazy();
    }

    private void BindPlayer()
    {
        Container.BindInterfacesAndSelfTo<Player>().FromInstance(Player).AsCached().NonLazy();
    }
}
