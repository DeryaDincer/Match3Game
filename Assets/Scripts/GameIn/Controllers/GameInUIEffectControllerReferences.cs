using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class GameInUIEffectControllerReferences
{
    [BHeader("References")]
    [SerializeField] private RectTransform onGridUnderUiLayerParent;
    [SerializeField] private RectTransform overEverythingLayerParent;
    [BHeader("Point References")]
    [SerializeField] private List<RectTransform> pointReferenceRects;



    public RectTransform OnGridUnderUiLayerParent => onGridUnderUiLayerParent;
    public RectTransform OverEverythingLayerParent => overEverythingLayerParent;

   public List<RectTransform> PointReferenceRects => pointReferenceRects;
}
