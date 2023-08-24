using UnityEngine;

[CreateAssetMenu(fileName = nameof(BlockAnimationControllerSettings), menuName = AssetMenuName.SETTINGS + nameof(BlockAnimationControllerSettings))]
public class BlockAnimationControllerSettings : ScriptableObject
{
    [BHeader("Swap Animation")]
    [SerializeField] private float swapAnimationDuration = .3f;
    public float SwapAnimationDuration => swapAnimationDuration;

    [SerializeField] private float swapScaleFactor = 1.3f;
    public float SwapScaleFactor => swapScaleFactor;


    [BHeader("Set Position Animation")]
    [SerializeField] private float movingSpeed = 10;
    public float MovingSpeed => movingSpeed;


    [BHeader("Despawn Animation")]
    [SerializeField] private float despawnAnimationDuration = .4f;
    public float DespawnAnimationDuration => despawnAnimationDuration;

    [SerializeField] private float despawnScaleFactor = 1.15f;
    public float DespawnScaleFactor => despawnScaleFactor;


}
