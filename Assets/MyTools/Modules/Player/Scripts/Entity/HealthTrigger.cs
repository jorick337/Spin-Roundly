using MyTools.UI;
using UnityEngine;
using UnityEngine.Events;

public class HealthTrigger : MonoBehaviour
{
    public event UnityAction OnDeath;

    [SerializeField] private Collider2DTrigger collider2DTrigger;
    [SerializeField] private int _heath = 3;

    private int _currentHealh;

    private void Awake() => _currentHealh = _heath;
    private void OnEnable() => collider2DTrigger.OnTriggered += Reduce;
    private void OnDisable() => collider2DTrigger.OnTriggered -= Reduce;

    private void Reduce(Collider2D collider2D)
    {
        _currentHealh -= 1;

        if (_currentHealh == 0)
            InvokeOnDeath();
    }

    private void InvokeOnDeath() => OnDeath?.Invoke();
}