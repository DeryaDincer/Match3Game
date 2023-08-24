using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;

public class ApplicationController: IInitializable
{
    #region Injection
    private ScreenController screenController;
    private ApplicationControllerSettings applicationControllerSettings;
    private BaseScreen.Factory screenFactory;

    [Inject]
    public void Construct(ScreenController screenController, ApplicationControllerSettings applicationControllerSettings, BaseScreen.Factory screenFactory)
    {
        this.screenController = screenController;
        this.applicationControllerSettings = applicationControllerSettings;
        this.screenFactory = screenFactory;
    }
    #endregion

    public void Initialize()
    {
        screenController.ChangeState(ScreenState.MainMenu);
    }
  
}