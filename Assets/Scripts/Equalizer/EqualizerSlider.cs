using UnityEngine;
using UnityEngine.UI;

public class EqualizerSlider : MonoBehaviour
{
    Slider slider;
    [SerializeField] int weaponId;
    void Start()
    {
        slider = GetComponent<Slider>();
    }

    private void Update()
    {

    }
}
