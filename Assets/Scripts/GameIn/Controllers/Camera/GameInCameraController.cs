using Cinemachine;
using Cysharp.Threading.Tasks;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class GameInCameraController: IInitializable
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
    public void Initialize()
    {
        width = (int)LevelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Width;
        height = (int)LevelController.GetCurrentLevel().SettingsInfo.BoardSpawnControllerSettings.Height;
        this.vCam = references.VirtualCamera;
        SetCamera();
    }

    private void SetCamera()
    {
        int orthographicSize = Mathf.Max(width, height);
        vCam.m_Lens.OrthographicSize = orthographicSize + cameraSizeOffset;
    }
}
