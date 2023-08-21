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
    public void Consturct(LevelSettings settings, LevelSceneReferences references)
    {
        this.references = references.GameInCameraControllerReferences;
        width = (int)settings.BoardSpawnControllerSettings.Width;
        height = (int)settings.BoardSpawnControllerSettings.Height;
    }
    public async UniTask Initialized()
    {
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
