using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using Cysharp.Threading.Tasks;
using Zenject;

public class BlockAnimationController
{
    private BlockAnimationControllerSettings settings;

    [Inject]
    public void Construct(LevelSettings settings)
    {
        // Initialize settings
        this.settings = settings.BlockAnimationControllerSettings;
    }

    // Set swap animation for a block
    public async UniTask SetSwapAnimation(Block block, bool setOrderInLayer = false)
    {
        float animationDuration = settings.SwapAnimationDuration;
        float swapScaleFactor = settings.SwapScaleFactor;

        if (setOrderInLayer)
        {
            block.SetBlockOrderInLayer(1);
            // Scale up the block, then scale down with animation delay
            block.transform.DOScale(Vector3.one * swapScaleFactor, animationDuration / 2).SetEase(Ease.OutCubic);
            block.transform.DOScale(Vector3.one, animationDuration / 2).SetEase(Ease.InCubic).SetDelay(animationDuration / 2);
        }

        // Move the block to its target position with animation
        await block.transform.DOMove(block.TargetPosition, animationDuration).OnComplete(() => block.SetBlockOrderInLayer(0)).AsyncWaitForCompletion();
    }

    // Set target transform for a block (move to target position)
    public async UniTask SetTargetTransform(Block block, bool setNewStartPosition = false)
    {
        float movingSpeed = settings.MovingSpeed;

        if (setNewStartPosition)
            block.transform.position = block.TargetBoardSpawnPosition;

        // Move the block to its target position with specified moving speed
        await block.transform.DOMove(block.TargetPosition, movingSpeed).SetSpeedBased(true).AsyncWaitForCompletion();
    }

    // Set despawn animation for a block
    public async UniTask SetDespawnAnimation(Block block, Action onComplete = null)
    {
        float animationDuration = settings.DespawnAnimationDuration;
        float despawnScaleFactor = settings.DespawnScaleFactor;

        Sequence sequence = DOTween.Sequence();

        // Scale down the block, then fade out
        block.transform.DOScale(Vector3.one * despawnScaleFactor, animationDuration * 0.5f)
            .SetEase(Ease.OutCubic).OnComplete(() =>
            {
                block.transform.DOScale(Vector3.zero, animationDuration * 0.5f)
                .SetEase(Ease.InCubic);
            });

        float currentAlpha = 1;
        await DOTween.To(() => currentAlpha, x => currentAlpha = x, 0f, animationDuration * 0.4f)
            .OnUpdate(() =>
            {
                block.SetBlockAlpha(currentAlpha);
            }).SetDelay(animationDuration * 0.5f).AsyncWaitForCompletion();
    }
}
