using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Firebase.Unity.Editor;
using Firebase.Database;
using Firebase;
using Firebase.Auth;

public class FirebaseManager : MonoBehaviour {

    public static FirebaseManager instance = null;

    [SerializeField]
    InputField inputEmail;

    [SerializeField]
    InputField inputPassword;

    [SerializeField]
    InputField inputConfirmPassword;

    [SerializeField]
    InputField inputLoginEmail;

    [SerializeField]
    InputField inputLoginPassword;

    Firebase.Auth.FirebaseAuth auth;

    FirebaseApp firebaseApp;
    DatabaseReference databaseReference;

    [HideInInspector]
    public MainMenuManager mainMenuManager { set; private get; }

    private Firebase.Auth.FirebaseUser currentUser;
    public Firebase.Auth.FirebaseUser CurrentUser { set; get; }
 

    void Awake() {
        auth = Firebase.Auth.FirebaseAuth.DefaultInstance;

        firebaseApp = FirebaseDatabase.DefaultInstance.App;
        firebaseApp.SetEditorDatabaseUrl("https://water-bottle-a596e.firebaseio.com/");
        databaseReference = FirebaseDatabase.DefaultInstance.RootReference;
        FirebaseApp.DefaultInstance.SetEditorP12FileName("water-bottle-a596e-678064a735e9.p12");

        FirebaseApp.DefaultInstance.SetEditorP12Password("notasecret");

        if (instance == null) {
            instance = this;
        } else if (instance != null) {
            Destroy(gameObject);
        }
    }

    void Start() {
        mainMenuManager = GameObject.Find("MainMenuCanvas").GetComponent<MainMenuManager>();
    }

    public void Register() {
        if(inputEmail.text.Length != 0 && inputPassword.text.Length != 0 && inputConfirmPassword.text.Length != 0) {
            if(inputPassword.text != inputConfirmPassword.text) {
                mainMenuManager.ShowOKDialog("Password doesn't match to ConfirmPassword");
                return;
            }
            auth.CreateUserWithEmailAndPasswordAsync(inputEmail.text, inputPassword.text).ContinueWith(task => {
                if(!task.IsCanceled && !task.IsFaulted) {
                    mainMenuManager.HideLoadingDialog();
                    mainMenuManager.ShowOKDialog("Register Completed");
                    mainMenuManager.HideRegisterMenu();
                } else {
                    Debug.Log("Register Failed");
                    mainMenuManager.HideLoadingDialog();
                    mainMenuManager.ShowOKDialog(task.Exception.InnerExceptions[0].Message);
                }
            });
            mainMenuManager.ShowLoadingDialog();
        } else {
            mainMenuManager.ShowOKDialog("Fill in the blanks");
        }
        
    }

    public void Login() {
        if (inputLoginEmail.text.Length != 0 && inputLoginPassword.text.Length != 0) {
            auth.SignInWithEmailAndPasswordAsync(inputLoginEmail.text, inputLoginPassword.text).ContinueWith(task => {
                if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
                    Debug.Log("Login Success");
                    currentUser = task.Result;
                    ApplicationManager.instance.IsLoggedIn = true;
                    ApplicationManager.instance.UserId = currentUser.UserId;
                    BinaryDataManager.SaveLevelsInfoData(ApplicationManager.instance.LastLevel, ApplicationManager.instance.Stars,
                        ApplicationManager.instance.IsLoggedIn, ApplicationManager.instance.UserId);
                    mainMenuManager.HideLoginMenu();
                    mainMenuManager.HideLoadingDialog();
                    mainMenuManager.ShowSaveMenu();
                    
                } else {
                    Debug.Log("Login Failed");
                    mainMenuManager.HideLoadingDialog();
                    mainMenuManager.ShowOKDialog(task.Exception.InnerExceptions[0].Message);
                }
            });
            mainMenuManager.GetComponent<MainMenuManager>().ShowLoadingDialog();
        } else {
            mainMenuManager.ShowOKDialog("Fill in the blanks");
        }
    }

    public void Save() {
        LevelsInfo levelsInfo = new LevelsInfo(ApplicationManager.instance.LastLevel, ApplicationManager.instance.Stars);
        string json = JsonUtility.ToJson(levelsInfo);
        databaseReference.Child("levelsinfo").Child(ApplicationManager.instance.UserId).SetRawJsonValueAsync(json).ContinueWith(task => {
            if (task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
                Debug.Log("Save Success");
                mainMenuManager.HideLoadingDialog();
                mainMenuManager.ShowOKDialog("Save Completed");
            } else {
                Debug.Log("Save Failed");
            }
        });
    }

    public void Load() {
        databaseReference.Child("levelsinfo").Child(ApplicationManager.instance.UserId).GetValueAsync().ContinueWith(task => {
            if(task.IsCompleted && !task.IsCanceled && !task.IsFaulted) {
                DataSnapshot snapshot = task.Result;
                LevelsInfo levelsInfo = JsonUtility.FromJson<LevelsInfo>(snapshot.GetRawJsonValue());
                ApplicationManager.instance.LastLevel = int.Parse(levelsInfo.lastLevel);
                for(int i = 0; i < 30; i++) {
                    ApplicationManager.instance.Stars[i] = int.Parse(levelsInfo.stars[i]);
                }

                BinaryDataManager.SaveLevelsInfoData(ApplicationManager.instance.LastLevel, ApplicationManager.instance.Stars,
                        ApplicationManager.instance.IsLoggedIn, ApplicationManager.instance.UserId);
                mainMenuManager.LoadDatasAsync();
                mainMenuManager.HideLoadingDialog();
                mainMenuManager.ShowOKDialog("Load Completed");
            } else {
                Debug.Log("Load Failed");
            }
        });
    }
}
