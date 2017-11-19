using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuControl : MonoBehaviour {

    [SerializeField]
    GameObject MainMenu;

    [SerializeField]
    GameObject SettingsMenu;

    [SerializeField]
    GameObject AudioSettingsMenu;

    [SerializeField]
    GameObject VideoSettingsMenu;

    [SerializeField]
    GameObject ControlMenu;

    [SerializeField]
    Button MoveRightBtn;
    [SerializeField]
    Text MoveRightTxt;

    [SerializeField]
    Button MoveLeftBtn;
    [SerializeField]
    Text MoveLeftTxt;

    [SerializeField]
    Button JumpBtn;
    [SerializeField]
    Text JumpTxt;

    [SerializeField]
    Button FallBtn;
    [SerializeField]
    Text FallTxt;

    [SerializeField]
    Button BackFromControls;

    [SerializeField]
    Dropdown ResDrop;

    bool Full;

    bool Right;
    bool Left;
    bool Jump;
    bool Fall;

    void Start()
    {
        BackToMenu();

        if(PlayerPrefs.GetString("Move Right Key") == "")
        {
            PlayerPrefs.SetString("Move Right Key", "D");
        }

        if (PlayerPrefs.GetString("Move Left Key") == "")
        {
            PlayerPrefs.SetString("Move Left Key", "A");
        }

        if (PlayerPrefs.GetString("Jump Key") == "")
        {
            PlayerPrefs.SetString("Jump Key", "Space");
        }

        if (PlayerPrefs.GetString("Fall Key") == "")
        {
            PlayerPrefs.SetString("Fall Key", "LeftShift");
        }

        if (Screen.fullScreen == true)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
    }

    void Update()
    {
        MoveRightTxt.text = PlayerPrefs.GetString("Move Right Key");
        MoveLeftTxt.text = PlayerPrefs.GetString("Move Left Key");
        JumpTxt.text = PlayerPrefs.GetString("Jump Key");
        FallTxt.text = PlayerPrefs.GetString("Fall Key");
        if (Right)
        {
            MoveLeftBtn.interactable = false;
            JumpBtn.interactable = false;
            FallBtn.interactable = false;
            BackFromControls.interactable = false;
            foreach(KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(Key))
                {
                    print(Key);
                    PlayerPrefs.SetString("Move Right Key", Key.ToString().ToLower());

                    switch (Key)
                    {
                        case KeyCode.RightArrow:
                            PlayerPrefs.SetString("Move Right Key", "right");
                            break;

                        case KeyCode.LeftArrow:
                            PlayerPrefs.SetString("Move Right Key", "left");
                            break;

                        case KeyCode.UpArrow:
                            PlayerPrefs.SetString("Move Right Key", "up");
                            break;

                        case KeyCode.DownArrow:
                            PlayerPrefs.SetString("Move Right Key", "down");
                            break;

                        case KeyCode.PageUp:
                            PlayerPrefs.SetString("Move Right Key", "page up");
                            break;

                        case KeyCode.PageDown:
                            PlayerPrefs.SetString("Move Right Key", "page down");
                            break;

                        case KeyCode.CapsLock:
                            PlayerPrefs.SetString("Move Right Key", "caps lock");
                            break;

                        case KeyCode.ScrollLock:
                            PlayerPrefs.SetString("Move Right Key", "scroll lock");
                            break;

                        case KeyCode.RightShift:
                            PlayerPrefs.SetString("Move Right Key", "right shift");
                            break;

                        case KeyCode.LeftShift:
                            PlayerPrefs.SetString("Move Right Key", "left shift");
                            break;

                        case KeyCode.RightControl:
                            PlayerPrefs.SetString("Move Right Key", "right ctrl");
                            break;

                        case KeyCode.LeftControl:
                            PlayerPrefs.SetString("Move Right Key", "left ctrl");
                            break;

                        case KeyCode.RightAlt:
                            PlayerPrefs.SetString("Move Right Key", "right alt");
                            break;

                        case KeyCode.LeftAlt:
                            PlayerPrefs.SetString("Move Right Key", "left alt");
                            break;

                        default: PlayerPrefs.SetString("Move Right Key", PlayerPrefs.GetString("Move Right Key"));
                            break;
                    }
                    Right = false;
                }
            }
        }
        else if (Left)
        {
            MoveRightBtn.interactable = false;
            JumpBtn.interactable = false;
            FallBtn.interactable = false;
            BackFromControls.interactable = false;
            foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(Key))
                {
                    PlayerPrefs.SetString("Move Left Key", Key.ToString().ToLower());

                    switch (Key)
                    {
                        case KeyCode.RightArrow:
                            PlayerPrefs.SetString("Move Left Key", "right");
                            break;

                        case KeyCode.LeftArrow:
                            PlayerPrefs.SetString("Move Left Key", "left");
                            break;

                        case KeyCode.UpArrow:
                            PlayerPrefs.SetString("Move Left Key", "up");
                            break;

                        case KeyCode.DownArrow:
                            PlayerPrefs.SetString("Move Left Key", "down");
                            break;

                        case KeyCode.PageUp:
                            PlayerPrefs.SetString("Move Left Key", "page up");
                            break;

                        case KeyCode.PageDown:
                            PlayerPrefs.SetString("Move Left Key", "page down");
                            break;

                        case KeyCode.CapsLock:
                            PlayerPrefs.SetString("Move Left Key", "caps lock");
                            break;

                        case KeyCode.ScrollLock:
                            PlayerPrefs.SetString("Move Left Key", "scroll lock");
                            break;

                        case KeyCode.RightShift:
                            PlayerPrefs.SetString("Move Left Key", "right shift");
                            break;

                        case KeyCode.LeftShift:
                            PlayerPrefs.SetString("Move Left Key", "left shift");
                            break;

                        case KeyCode.RightControl:
                            PlayerPrefs.SetString("Move Left Key", "right ctrl");
                            break;

                        case KeyCode.LeftControl:
                            PlayerPrefs.SetString("Move Left Key", "left ctrl");
                            break;

                        case KeyCode.RightAlt:
                            PlayerPrefs.SetString("Move Left Key", "right alt");
                            break;

                        case KeyCode.LeftAlt:
                            PlayerPrefs.SetString("Move Left Key", "left alt");
                            break;

                        default:
                            PlayerPrefs.SetString("Move Left Key", PlayerPrefs.GetString("Move Left Key"));
                            break;
                    }
                            Left = false;
                }
            }
        }
        else if (Jump)
        {
            MoveRightBtn.interactable = false;
            MoveLeftBtn.interactable = false;
            FallBtn.interactable = false;
            BackFromControls.interactable = false;
            foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(Key))
                {
                    PlayerPrefs.SetString("Jump Key", Key.ToString().ToLower());

                    switch (Key)
                    {
                        case KeyCode.RightArrow:
                            PlayerPrefs.SetString("Jump Key", "right");
                            break;

                        case KeyCode.LeftArrow:
                            PlayerPrefs.SetString("Jump Key", "left");
                            break;

                        case KeyCode.UpArrow:
                            PlayerPrefs.SetString("Jump Key", "up");
                            break;

                        case KeyCode.DownArrow:
                            PlayerPrefs.SetString("Jump Key", "down");
                            break;

                        case KeyCode.PageUp:
                            PlayerPrefs.SetString("Jump Key", "page up");
                            break;

                        case KeyCode.PageDown:
                            PlayerPrefs.SetString("Jump Key", "page down");
                            break;

                        case KeyCode.CapsLock:
                            PlayerPrefs.SetString("Jump Key", "caps lock");
                            break;

                        case KeyCode.ScrollLock:
                            PlayerPrefs.SetString("Jump Key", "scroll lock");
                            break;

                        case KeyCode.RightShift:
                            PlayerPrefs.SetString("Jump Key", "right shift");
                            break;

                        case KeyCode.LeftShift:
                            PlayerPrefs.SetString("Jump Key", "left shift");
                            break;

                        case KeyCode.RightControl:
                            PlayerPrefs.SetString("Jump Key", "right ctrl");
                            break;

                        case KeyCode.LeftControl:
                            PlayerPrefs.SetString("Jump Key", "left ctrl");
                            break;

                        case KeyCode.RightAlt:
                            PlayerPrefs.SetString("Jump Key", "right alt");
                            break;

                        case KeyCode.LeftAlt:
                            PlayerPrefs.SetString("Jump Key", "left alt");
                            break;

                        default:
                            PlayerPrefs.SetString("Jump Key", PlayerPrefs.GetString("Jump Key"));
                            break;
                    }

                            Jump = false;
                }
            }
        }
        else if (Fall)
        {
            MoveRightBtn.interactable = false;
            MoveLeftBtn.interactable = false;
            JumpBtn.interactable = false;
            BackFromControls.interactable = false;
            foreach (KeyCode Key in System.Enum.GetValues(typeof(KeyCode)))
            {
                if (Input.GetKeyDown(Key))
                {
                    PlayerPrefs.SetString("Fall Key", Key.ToString().ToLower());

                    switch (Key)
                    {
                        case KeyCode.RightArrow:
                            PlayerPrefs.SetString("Fall Key", "right");
                            break;

                        case KeyCode.LeftArrow:
                            PlayerPrefs.SetString("Fall Key", "left");
                            break;

                        case KeyCode.UpArrow:
                            PlayerPrefs.SetString("Fall Key", "up");
                            break;

                        case KeyCode.DownArrow:
                            PlayerPrefs.SetString("Fall Key", "down");
                            break;

                        case KeyCode.PageUp:
                            PlayerPrefs.SetString("Fall Key", "page up");
                            break;

                        case KeyCode.PageDown:
                            PlayerPrefs.SetString("Fall Key", "page down");
                            break;

                        case KeyCode.CapsLock:
                            PlayerPrefs.SetString("Fall Key", "caps lock");
                            break;

                        case KeyCode.ScrollLock:
                            PlayerPrefs.SetString("Fall Key", "scroll lock");
                            break;

                        case KeyCode.RightShift:
                            PlayerPrefs.SetString("Fall Key", "right shift");
                            break;

                        case KeyCode.LeftShift:
                            PlayerPrefs.SetString("Fall Key", "left shift");
                            break;

                        case KeyCode.RightControl:
                            PlayerPrefs.SetString("Fall Key", "right ctrl");
                            break;

                        case KeyCode.LeftControl:
                            PlayerPrefs.SetString("Fall Key", "left ctrl");
                            break;

                        case KeyCode.RightAlt:
                            PlayerPrefs.SetString("Fall Key", "right alt");
                            break;

                        case KeyCode.LeftAlt:
                            PlayerPrefs.SetString("Fall Key", "left alt");
                            break;

                        default:
                            PlayerPrefs.SetString("Fall Key", PlayerPrefs.GetString("Fall Key"));
                            break;
                    }
                            Fall = false;
                }
            }
        }
        else
        {
            MoveRightBtn.interactable = true;
            MoveLeftBtn.interactable = true;
            JumpBtn.interactable = true;
            FallBtn.interactable = true;
            BackFromControls.interactable = true;
        }
    }
    
    public void NewGame()
    {
        PlayerPrefs.SetInt("Current Scene", 0);
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Scene") + 1);
        PlayerPrefs.SetInt("Current Scene", PlayerPrefs.GetInt("Current Scene") + 1);
    }

    public void Resume()
    {
        SceneManager.LoadScene(PlayerPrefs.GetInt("Current Scene"));
    }

    public void Settings()
    {
        MainMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void Audio()
    {
        SettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(true);
    }

    public void Video()
    {
        SettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(true);
    }

    public void Controls()
    {
        SettingsMenu.SetActive(false);
        ControlMenu.SetActive(true);
    }

    public void BackToMenu()
    {
        SettingsMenu.SetActive(false);
        AudioSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        ControlMenu.SetActive(false);
        MainMenu.SetActive(true);
    }

    public void BackToSettings()
    {
        AudioSettingsMenu.SetActive(false);
        VideoSettingsMenu.SetActive(false);
        ControlMenu.SetActive(false);
        SettingsMenu.SetActive(true);
    }

    public void ChangeResolution()
    {
        switch (ResDrop.value)
        {
            case 0:
                PlayerPrefs.SetInt("ScreenWidth", 1600);
                PlayerPrefs.SetInt("ScreenHeight", 675);
                break;

            case 1:
                PlayerPrefs.SetInt("ScreenWidth", 1920);
                PlayerPrefs.SetInt("ScreenHeight", 810);
                break;

            case 2:
                PlayerPrefs.SetInt("ScreenWidth", 1920);
                PlayerPrefs.SetInt("ScreenHeight", 1080);
                break;

            case 3:
                PlayerPrefs.SetInt("ScreenWidth", 1680);
                PlayerPrefs.SetInt("ScreenHeight", 1050);
                break;

            case 4:
                PlayerPrefs.SetInt("ScreenWidth", 1600);
                PlayerPrefs.SetInt("ScreenHeight", 900);
                break;

            case 5:
                PlayerPrefs.SetInt("ScreenWidth", 1440);
                PlayerPrefs.SetInt("ScreenHeight", 900);
                break;

            case 6:
                PlayerPrefs.SetInt("ScreenWidth", 1400);
                PlayerPrefs.SetInt("ScreenHeight", 1050);
                break;

            case 7:
                PlayerPrefs.SetInt("ScreenWidth", 1366);
                PlayerPrefs.SetInt("ScreenHeight", 768);
                break;

            case 8:
                PlayerPrefs.SetInt("ScreenWidth", 1360);
                PlayerPrefs.SetInt("ScreenHeight", 768);
                break;

            case 9:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 1024);
                break;

            case 10:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 960);
                break;

            case 11:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 768);
                break;

            case 12:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 800);
                break;

            case 13:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 720);
                break;

            case 14:
                PlayerPrefs.SetInt("ScreenWidth", 1152);
                PlayerPrefs.SetInt("ScreenHeight", 864);
                break;

            case 15:
                PlayerPrefs.SetInt("ScreenWidth", 1024);
                PlayerPrefs.SetInt("ScreenHeight", 768);
                break;

            case 16:
                PlayerPrefs.SetInt("ScreenWidth", 1280);
                PlayerPrefs.SetInt("ScreenHeight", 540);
                break;

            case 17:
                PlayerPrefs.SetInt("ScreenWidth", 800);
                PlayerPrefs.SetInt("ScreenHeight", 600);
                break;

            case 18:
                PlayerPrefs.SetInt("ScreenWidth", 640);
                PlayerPrefs.SetInt("ScreenHeight", 400);
                break;

            case 19:
                PlayerPrefs.SetInt("ScreenWidth", 512);
                PlayerPrefs.SetInt("ScreenHeight", 384);
                break;

            case 20:
                PlayerPrefs.SetInt("ScreenWidth", 640);
                PlayerPrefs.SetInt("ScreenHeight", 480);
                break;

            case 21:
                PlayerPrefs.SetInt("ScreenWidth", 400);
                PlayerPrefs.SetInt("ScreenHeight", 300);
                break;

            case 22:
                PlayerPrefs.SetInt("ScreenWidth", 320);
                PlayerPrefs.SetInt("ScreenHeight", 200);
                break;

            case 23:
                PlayerPrefs.SetInt("ScreenWidth", 320);
                PlayerPrefs.SetInt("ScreenHeight", 240);
                break;
            
            default:
                PlayerPrefs.SetInt("ScreenWidth", 1920);
                PlayerPrefs.SetInt("ScreenHeight", 1080);
                break;
        }
        if(PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            Full = false;
        }
        else
        {
            Full = true;
        }

        Screen.SetResolution(PlayerPrefs.GetInt("ScreenWidth"), PlayerPrefs.GetInt("ScreenHeight"), Full);

    }

    public void FullscreenIO()
    {
        if(PlayerPrefs.GetInt("Fullscreen") == 0)
        {
            PlayerPrefs.SetInt("Fullscreen", 1);
        }
        else
        {
            PlayerPrefs.SetInt("Fullscreen", 0);
        }
        ChangeResolution();
    }

    public void MoveRightChange()
    {
        Right = true;
    }

    public void MoveLeftChange()
    {
        Left = true;
    }

    public void JumpChange()
    {
        Jump = true;
    }

    public void FallChange()
    {
        Fall = true;
    }

    public void Quit()
    {
        Application.Quit();
    }
}
