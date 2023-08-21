using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelButton : MonoBehaviour
{

    [SerializeField] private Button button;
    [SerializeField] private TextMeshProUGUI buttonText;
    [SerializeField] private TextMeshProUGUI text;
    [SerializeField] private Sprite playSprite;
    [SerializeField] private Sprite lockedSprite;
    [SerializeField] int levelID;
    private bool disablePlayButton;

    private const string playButtonString = "PLAY";
    private const string lockedButtonString = "LOCKED";

    public void Init(Action onClick, int LevelID, bool isInteractable, LevelController.LevelData info)
    {
        levelID = LevelID;
        string headerText = $"LEVEL {LevelID + 1}";
        text.text = headerText;

        if (isInteractable && !disablePlayButton)
        {

            button.onClick.AddListener(() => onClick?.Invoke());
            SetButtonProps(playSprite, playButtonString);
        }
        else
        {
            SetButtonProps(lockedSprite, lockedButtonString);
        }

    }

    private void SetButtonProps(Sprite sprite, string text)
    {
        button.image.sprite = sprite;
        buttonText.text = text;
    }

    private void OnDisable()
    {
        button.onClick.RemoveAllListeners();
    }

}