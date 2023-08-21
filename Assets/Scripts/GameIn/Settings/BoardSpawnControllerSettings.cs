using UnityEngine;

[System.Serializable]
public class BoardSpawnControllerSettings
{

    [BHeader("Grid Start Layout")]
   public GridStartLayout gridStartLayout = new GridStartLayout(9, 9);
 //  public GridStartLayout gridStartLayout = new GridStartLayout((int)rowCount, (int)collumnCount);

    [BHeader("Block Entity Spawner Settings")]
    [SerializeField] private BaseBlockEntityTypeDefinition[] entityTypes;
    public BaseBlockEntityTypeDefinition[] EntityTypes => entityTypes;

    [SerializeField] private uint width = 4;
    [SerializeField] private uint height = 4;
    public uint Width => width;
    public uint Height => height;
   
}