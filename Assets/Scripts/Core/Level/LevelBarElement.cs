using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelBarElement : MonoBehaviour
{
    //[SerializeField] private Button button;
    //[SerializeField] private TextMeshProUGUI buttonText;
    //[SerializeField] private TextMeshProUGUI text;
    //[SerializeField] GameObject[] StarArray;
    //[SerializeField] private Sprite playSprite;
    //[SerializeField] private Sprite lockedSprite;

    //[SerializeField] private GameObject progressBar;
    //[SerializeField] private Image progressBarFillImage;
    //[SerializeField] private TextMeshProUGUI progressBarStarText;
    //private bool disablePlayButton;

    //private const string playButtonString = "PLAY";
    //private const string lockedButtonString = "LOCKED";

    //public int StarCount;
    //[SerializeField]int levelID;
    //public void Init(Action onClick,int LevelID,bool isInteractable,int starCount, LevelManager.LevelData info)
    //{
    //    levelID = LevelID;
    //    string headerText = $"LEVEL {LevelID + 1}";
    //    text.text = headerText;

    //    StarCount = starCount;
    //    CheckProgressBar(levelID, info);

    //    if (isInteractable && !disablePlayButton)
    //    {
    //        OpenStar();
            
    //        button.onClick.AddListener(() => onClick?.Invoke());
    //        SetButtonProps(playSprite, playButtonString);
    //    }
    //    else
    //    {
    //        SetButtonProps(lockedSprite, lockedButtonString);
    //    }

    //}
    //private void CheckProgressBar(int ievelID, LevelManager.LevelData info)
    //{
    //    if ((ievelID + 1) % 5 != 0)
    //    {
    //        return;
    //    }

    //    int allStarCount = 0;
    //    int targetStarCount = levelID * 2;
    //    for (int i = 0; i < levelID; i++)
    //    {
    //        allStarCount += info.Levels[i].LevelStarCount;
    //    }
    //    bool enableProgressBar = false;
    //    if (allStarCount < targetStarCount)
    //    {
    //        DisableAllStar();
    //        enableProgressBar = true;
    //    }
    //    EnableDisableSlideBar(enableProgressBar, allStarCount, targetStarCount);
    //}
    //private void SetButtonProps(Sprite sprite, string text)
    //{
    //    button.image.sprite = sprite;
    //    buttonText.text = text;
    //}
    //public void SetInfo(LevelData levelData)
    //{

    //}
    //private void OnDisable()
    //{
    //    button.onClick.RemoveAllListeners();
    //}


    //private void OpenStar()
    //{
    //    for (int i = 0; i < StarArray.Length; i++)
    //    {
    //        if (i < StarCount)
    //        {
    //            StarArray[i].gameObject.SetActive(true);
    //        }
    //        else break;
            
    //    }
    //}
    //private void DisableAllStar()
    //{
    //    for (int i = 0; i < StarArray.Length; i++)
    //    {
    //        StarArray[i].gameObject.SetActive(false);
    //    }
    //}
    //private void EnableDisableSlideBar(bool active, int allStarCount, int targetStarCount)
    //{
    //    disablePlayButton = active;

    //    progressBar.SetActive(active);
    //    float fillAmount = (float)allStarCount / (float)targetStarCount;

    //    progressBarFillImage.fillAmount = fillAmount;

    //    progressBarStarText.text = allStarCount + " / " + targetStarCount;
    //}
}
