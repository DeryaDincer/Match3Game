using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInCameraController
{
    private CinemachineVirtualCamera vCam;
    private float cameraSizeOffset = .75f;
    private GameInCameraControllerReferences references;
    private int width;
    private int height;

    [Inject]
    public void Consturct(LevelSceneReferences references)
    {
        this.references = references.GameInCameraControllerReferences;
       
    }
    public async UniTask Initialized()
    {
        width = (int)LevelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Width;
        height = (int)LevelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Height;
        this.vCam = references.VirtualCamera;
        SetCamera();
        await UniTask.Yield();
    }

    private void SetCamera()
    {
        int orthographicSize = Mathf.Max(width, height);
        vCam.m_Lens.OrthographicSize = orthographicSize + cameraSizeOffset;
    }
}
