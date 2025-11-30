using UnityEngine;
using UnityEngine.Events;

[CreateAssetMenu(menuName = "Game/Event")]
public class GameEvent : ScriptableObject
{
    private UnityAction listeners;

    public void Raise() => listeners?.Invoke();

    public void Register(UnityAction listener) => listeners += listener;
    public void Unregister(UnityAction listener) => listeners -= listener;
}
