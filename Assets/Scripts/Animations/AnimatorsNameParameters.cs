using UnityEngine;

namespace Character
{
    [CreateAssetMenu(fileName = "Animation Configs", menuName = "Animation Configs / Animator Parameters")]
    public sealed class AnimatorsNameParameters : ScriptableObject
    {
        [field: SerializeField] public string TriggerEnter { get; private set; }
        [field: SerializeField] public string TriggerExit { get; private set; }
    }
}