using TMPro;
using UnityEngine;

namespace Ui
{
    public class View : MonoBehaviour
    {
        [field: SerializeField] public TextMeshProUGUI TimeAfterStartGame { get; private set; }
        [field: SerializeField] public TextMeshProUGUI CountCoupons { get; private set; }
    }
}