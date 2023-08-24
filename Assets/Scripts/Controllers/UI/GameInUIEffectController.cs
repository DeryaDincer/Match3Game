using DG.Tweening;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

public class GameInUIEffectController : IInitializable
{
    public enum CanvasLayer
    {
        OverGridUnderUI,
        OverEverything,
    }

    private RectTransform onGridUnderUiLayerParent;
    private RectTransform overEverythingLayerParent;
    private List<RectTransform> pointReferenceRects;

    #region Injection
    private GameInUIEffectControllerReferences references;
    private FlyingSprite.Factory flyingSpriteFactory;
    private LevelSceneReferences levelSceneReferences;

    [Inject]
    public void Construct(LevelSceneReferences levelSceneReferences, FlyingSprite.Factory flyingSpriteFactory)
    {
        this.levelSceneReferences = levelSceneReferences;
        this.flyingSpriteFactory = flyingSpriteFactory;
    }
    #endregion


    public void Initialize()
    {
        references = levelSceneReferences.GameInUIEffectControllerReferences;
        pointReferenceRects = this.references.PointReferenceRects;
    }

    // Create a flying sprite effect with specified parameters.
    public void CreateCurvyFlyingSprite(Sprite sprite, Vector2 targetPos, Action onComplete = null)
    {
        FlyingSprite flyingObj = flyingSpriteFactory.Create();
        RectTransform rect = flyingObj.GetComponent<RectTransform>();
        flyingObj.transform.SetParent(references.OnGridUnderUiLayerParent);
        flyingObj.transform.localScale = Vector3.one;
        rect.anchoredPosition = new Vector2(0, 400);
        flyingObj.GetComponent<Image>().sprite = sprite;
        Tween flyingTween = TweenHelper.CurvingMoveTo(flyingObj.transform, targetPos, onComplete, .5f, .2f, Ease.InOutCubic, Ease.InBack);
        flyingTween.onComplete += () => flyingObj.Despawn();
    }

    // Get the parent RectTransform for a specified canvas layer.
    public RectTransform GetLayerParent(CanvasLayer layer)
    {
        switch (layer)
        {
            case CanvasLayer.OverGridUnderUI:
                return onGridUnderUiLayerParent;
            case CanvasLayer.OverEverything:
                return overEverythingLayerParent;
            default:
                Debug.LogWarning("Unknown CanvasLayer: " + layer);
                return null;
        }
    }

    // Get the world position of a reference point by its name.
    public Vector2 GetReferencePointByName(string referenceName)
    {
        foreach (RectTransform rect in pointReferenceRects)
        {
            if (rect.name != referenceName) continue;
            return rect.position;
        }
        Debug.LogError("Reference point with Name not found: " + referenceName);
        return Vector2.zero;
    }

    public void SetParent(RectTransform rectTransformParent)
    {
        onGridUnderUiLayerParent = rectTransformParent;
        overEverythingLayerParent = rectTransformParent;
    }
#if UNITY_EDITOR
    // Draw Gizmos in the Unity Editor to visualize reference points.
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        foreach (RectTransform rect in pointReferenceRects)
        {
            if (rect == null) continue;
            Gizmos.DrawSphere(rect.position, 50f);
            DrawString(rect.name, rect.position);
        }
    }

    // Draw a string at a specific world position in the Unity Editor.
    public void DrawString(string text, Vector3 worldPos, Color? textColor = null)
    {
        if (textColor.HasValue) UnityEditor.Handles.color = textColor.Value;
        UnityEditor.Handles.Label(worldPos, text);
    }
#endif
}
