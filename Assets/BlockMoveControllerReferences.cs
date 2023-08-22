using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[System.Serializable]
public class BlockMoveControllerReferences
{
    [SerializeField] private TextMeshProUGUI movesLeftText;
    public TextMeshProUGUI MovesLeftText => movesLeftText;
}
