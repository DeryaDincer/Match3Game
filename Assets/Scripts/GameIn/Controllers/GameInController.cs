using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using Zenject;


public class GameInController : MonoBehaviour
{
    [Inject] private BoardSpawnController boardSpawnController;
    [Inject] private BoardController boardController;
    [Inject] private GameInCameraController gameInCameraController;
    [Inject] private BlockAnimationController blockAnimationController;
    [Inject] private BlockGoalController BlockGoalController;
    [Inject] private BlockMoveController BlockMoveController;

    private void Start()
    {
        InitialControllerAsync();
    }

    public async void InitialControllerAsync()
    {
        await PoolManager.Instance.IsLoading.Task;
        await UniTask.Yield();
        await boardSpawnController.SpawnRandomBoard();
        await gameInCameraController.Initialized();
        await blockAnimationController.Initialized();
        await BlockGoalController.Initialized();
        await BlockMoveController.Initialized();
        boardController.AddRegister();
    }
}