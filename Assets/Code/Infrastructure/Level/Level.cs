using UnityEngine;

namespace Code.Infrastructure.GameStates
{
    public class Level: ScriptableObject
    {
        [SerializeField] private int _number;
        [SerializeField] private LevelMap _map;
        [SerializeField] private Sprite _background;
    }
}