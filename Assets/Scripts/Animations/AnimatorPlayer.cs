using UnityEngine;
using UnityEngine.Serialization;

namespace Character
{
    public sealed class AnimatorPlayer : MonoBehaviour
    {
        [field: SerializeField] private Animator animator;
        [field: SerializeField] private AnimatorsNameParameters animatorsNameParameters;
        
        public void SetTriggerEnter() => animator.SetTrigger(animatorsNameParameters.TriggerEnter);

        public void SetTriggerExit() => animator.SetTrigger(animatorsNameParameters.TriggerExit);
    }
}