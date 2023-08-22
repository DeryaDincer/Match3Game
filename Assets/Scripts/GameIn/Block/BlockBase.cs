using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class BlockBase : MonoBehaviour, IBlockEntity
{
    // Block properties
    public int BlockID => blockID;
    public bool Situated => situated;
    public Vector3 TargetPosition => targetPosition;
    public Vector3 TargetBoardSpawnPosition => targetBoardSpawnPosition;

    // Serialized fields
    [SerializeField] private int blockID = 0;
    [SerializeField] private bool situated = false;
    private Vector3 targetPosition;
    private Vector3 targetBoardSpawnPosition;

    [SerializeField] protected SpriteRenderer entityRenderer; 

    // Unity event for entity destruction
    public UnityEvent<IBlockEntityTypeDefinition> OnEntityDestroyed { get; private set; } = new UnityEvent<IBlockEntityTypeDefinition>();
    public IBlockEntityTypeDefinition EntityType { get; protected set; }

    // Invoked when the entity is destroyed
    public virtual void OnEntityDestroy()
    {
        OnEntityDestroyed.Invoke(EntityType);
    }

    // Setup the entity with the given block type
    public virtual void SetupEntity(IBlockEntityTypeDefinition blockType)
    {
        EntityType = blockType;
        SetBlockImage(blockType.DefaultEntitySprite);
    }

    // Set the entity as situated
    public virtual void SetSituated()
    {
        situated = true;
    }

    // Set the sprite image of the block
    public void SetBlockImage(Sprite sprite)
    {
        if (sprite == null || entityRenderer == null) return;

        entityRenderer.color = Color.white;
        entityRenderer.sprite = sprite;
    }

    // Set the alpha value of the block
    public void SetBlockAlpha(float alpha)
    {
        if (entityRenderer == null) return;

        Color currentColor = entityRenderer.color;
        currentColor.a = alpha;
        entityRenderer.color = currentColor;
    }

    // Set the block's ID
    public void SetBlockBaseId(int blockID)
    {
        this.blockID = blockID;
    }

    // Called when the object returns to the pool
    public void OnGoToPool()
    {
        OnEntityDestroyed.RemoveAllListeners();
    }

    // Called when the object is spawned from the pool
    public virtual void OnPoolSpawn()
    {
        EntityType = null;
    }

    // Deactivate the object
    //public override void OnDeactivate()
    //{
    //    gameObject.SetActive(false);
    //}

    // Called when the object is spawned
    //public override void OnSpawn()
    //{
    //    OnDeactivate();
    //}

    // Called when the object is created
    //public override void OnCreated()
    //{
    //    transform.localScale = Vector3.one;
    //    gameObject.SetActive(true);
    //}

   
    // Set the target position based on the BlockID
    public void SetTargetPositionToID(Board board)
    {
        int boardWidth = board.Width;
        int boardHeight = board.Height;

        int x = BlockID % boardWidth;
        int y = BlockID / boardWidth;

        float startX = -(boardWidth - 1) / 2f;
        float startY = +(boardHeight - 1) / 2f;

        Vector3 pos = Vector3.zero;
        pos.x = x + startX;
        pos.y = -y + startY;

        targetPosition = pos;

        pos.y = startY;
        targetBoardSpawnPosition = pos + Vector3.up;
    }

    // Set the position of the block
    public void SetPosition()
    {
        transform.position = targetPosition;
    }

    // Set the sorting order of the block in the rendering layer
    public void SetBlockOrderInLayer(int layer)
    {
        entityRenderer.sortingOrder = layer;
    }

    // Set the visibility of the renderer
    public void SetActiveRenderer(bool active)
    {
        entityRenderer.enabled = active;
    }

 
}
