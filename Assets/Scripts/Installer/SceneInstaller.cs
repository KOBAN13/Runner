using Character;
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
    public override void InstallBindings()
    {
        BindPlayer();
        BindInput();
        BindInputInterface();
        BindAllInterface();
        BindPhysics();
    }

    
    //как биндить интерфейсы
    //PlayerMovementController в бинд
    //Gravity v bind ++

    private void BindAllInterface()
    {
        Container.BindInterfacesAndSelfTo<PlayerMovementController>().FromInstance(movementController).AsCached().NonLazy();
        Container.BindInterfacesAndSelfTo<PlayerJumpController>().FromInstance(jumpController).AsCached().NonLazy();
    }

    private void BindPhysics()
    {
        Container.BindInterfacesAndSelfTo<Gravity>().AsSingle().NonLazy();
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
