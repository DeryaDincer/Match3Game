using UnityEngine;
using UnityEngine.U2D;

public class BaseBlockEntityTypeDefinition : ScriptableObject, IBlockEntityTypeDefinition
{
    [BHeader("Base Block Entity Type Definition")]
    [SerializeField] protected string defaultEntitySpriteName;
    [SerializeField] protected SpriteAtlas entitySpriteAtlas;

    public string DefaultEntitySpriteName => defaultEntitySpriteName;
    public SpriteAtlas EntitySpriteAtlas => entitySpriteAtlas;
}