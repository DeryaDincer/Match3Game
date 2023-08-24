using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public interface IBlockEntityTypeDefinition
{
    public string DefaultEntitySpriteName { get; } 
    public SpriteAtlas EntitySpriteAtlas{ get; } 
}