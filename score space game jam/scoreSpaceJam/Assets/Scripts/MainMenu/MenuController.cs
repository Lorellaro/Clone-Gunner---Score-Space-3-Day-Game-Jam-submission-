using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;

public class MenuController : MonoBehaviour
{
    [SerializeField] TMP_FontAsset boldFont;
    [SerializeField] TMP_FontAsset standardFont;
    [SerializeField] List<GameObject> hoverBoxes;
    [SerializeField] List<TextMeshProUGUI> allTMPText;

    public GameObject mainMenu;
    public GameObject leaderboardMenu;
    public GameObject controlsMenu;
    public GameObject soundMenu;

    public void startGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void openLeaderboardMenu()
    {
        mainMenu.SetActive(false);
        leaderboardMenu.SetActive(true);
    }

    public void openControlsMenu()
    {
        mainMenu.SetActive(false);
        controlsMenu.SetActive(true);
    }

    public void backToMainMenu()
    {
        disableAllHoverBoxesAndBold();
        controlsMenu.SetActive(false);
        leaderboardMenu.SetActive(false);
        soundMenu.SetActive(false);
        mainMenu.SetActive(true);
    }
    public void openSoundMenu()
    {
        mainMenu.SetActive(false);
        soundMenu.SetActive(true);
    }

    private void disableAllHoverBoxesAndBold()//Prevents hover box from showing when u return to title screen
    {
        for (int i = 0; i < hoverBoxes.Count; i++)
        {
            hoverBoxes[i].SetActive(false);
        }

        for(int i = 0; i < allTMPText.Count; i++)
        {
            allTMPText[i].font = boldFont;
        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

    public void hoverOverButton(TextMeshProUGUI buttonTMP)//called in event listener
    {
        GameObject parentButton = buttonTMP.gameObject.transform.parent.gameObject;
        GameObject hoverBox = parentButton.transform.GetChild(1).gameObject;
        hoverBox.SetActive(true);

        buttonTMP.font = standardFont;

        //sfx
        parentButton.GetComponent<AudioSource>().Play();
    }

    public void hoverExitButton(TextMeshProUGUI buttonTMP)
    {
        GameObject hoverBox = buttonTMP.gameObject.transform.parent.GetChild(1).gameObject;
        hoverBox.SetActive(false);

        buttonTMP.font = boldFont;
    }

    public void buttonClicked()
    {
        //sfx
    }
}
