using InputSystem;
using UnityEngine;

namespace Character.PlayerJumpController
{
    [RequireComponent(typeof(CharacterController))]
    public class PlayerMovementController : MonoBehaviour, IMovable, IPlayerSettings
    {
        [Header("Player Configs")]
        [SerializeField] private СlownPlayerSettings clownPlayerSettings;

        [field: Header("Player Settings")]
        [field: SerializeField] public float LineDistance { get; private set; }
        [field: SerializeField] public float FirstPosition { get; private set; }

        public float TargetDirectionY
        {
            get => _targetDirection.y;
            set => _targetDirection.y = value;
        }
        
        public CharacterController CharacterController { get; private set; }

        private Vector3 _targetDirection;
        private int _lineToMove = 1;

        public void Awake() => CharacterController = GetComponent<CharacterController>();

        public void Update() => MoveForward();

        public void Move(Swipe axis)
        {
            switch (axis)
            {
                case Swipe.Right:
                {
                    if (_lineToMove < 2)
                        _lineToMove++;
                    break;
                }
                case Swipe.Left:
                {
                    if (_lineToMove > 0)
                        _lineToMove--;
                    break;
                }
            }
        }
        
        private void MoveForward()
        {
            Debug.Log(TargetDirectionY);
            _targetDirection.x = clownPlayerSettings.MaxSpeed;
            CharacterController.Move(_targetDirection * Time.deltaTime);
            Vector3 newPosition = transform.position;
            newPosition.z = Mathf.Lerp(newPosition.z, FirstPosition + _lineToMove * LineDistance, Time.deltaTime * clownPlayerSettings.SliderSpeed);
            transform.position = newPosition;
        }
    }
}