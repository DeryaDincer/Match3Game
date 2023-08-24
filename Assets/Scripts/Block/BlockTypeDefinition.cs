using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Block Type Definition")]
public class BlockTypeDefinition : BaseBlockEntityTypeDefinition
{
    [BHeader("Block Entity Type Definition")]
    [SerializeField] protected BlockType blockType;
    [SerializeField] protected GameObject destroyParticle;
  
    public BlockType BlockType => blockType;
    public GameObject DestroyParticle => destroyParticle;
}

public enum BlockType
{
    Block_B,
    Block_G,
    Block_R,
    Block_Y
}
