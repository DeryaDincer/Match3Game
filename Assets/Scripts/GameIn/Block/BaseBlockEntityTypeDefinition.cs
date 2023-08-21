using UnityEngine;

public class BaseBlockEntityTypeDefinition : ScriptableObject, IBlockEntityTypeDefinition
{
    [BHeader("Base Block Entity Type Definition")]
    [SerializeField] protected Sprite defaultSprite;

    public Sprite DefaultEntitySprite => defaultSprite;

}