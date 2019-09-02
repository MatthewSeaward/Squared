//using Firebase.Auth;
//using System.Threading.Tasks;
//using UnityEngine;
//using Google;

//namespace Assets.Scripts
//{
//    class Authentication : MonoBehaviour
//    {
//        private FirebaseAuth auth;
//        private FirebaseUser FBuser;

//        void Start()
//        {
//            InitializeFirebase();
//        }

//        // Update is called once per frame
//        void Update()
//        {

//        }

//        void InitializeFirebase()
//        {
//            Debug.Log("Setting up Firebase Auth");
//            auth = FirebaseAuth.DefaultInstance;
//            auth.StateChanged += AuthStateChanged;
//            AuthStateChanged(this, null);
//        }

//        // Track state changes of the auth object.
//        void AuthStateChanged(object sender, System.EventArgs eventArgs)
//        {
//            if (auth.CurrentUser != null)
//            {
//                bool signedIn = FBuser != auth.CurrentUser && auth.CurrentUser != null;
//                if (!signedIn && FBuser != null)
//                {
//                    Debug.Log("Signed out " + FBuser.UserId);
//                }
//                FBuser = auth.CurrentUser;
//                if (signedIn)
//                {
//                    Debug.Log("Signed in " + FBuser.UserId);
//                }
//            }
//        }


//        public void googleSignInButton()
//        {
//            googleSignInButton.Configuration = new GoogleSignInConfiguration
//            {
//                RequestIdToken = true,
//                // Copy this value from the google-service.json file.
//                // oauth_client with type == 3
//                WebClientId = "318103883585-s7rfmu149ni9rhcrtt10ufrqi8oeshss.apps.googleusercontent.com"
//            };

//            Task<GoogleSignInUser> signIn = GoogleSignIn.DefaultInstance.SignIn();

//            TaskCompletionSource<FirebaseUser> signInCompleted = new TaskCompletionSource<FirebaseUser>();
//            signIn.ContinueWith(task =>
//            {
//                if (task.IsCanceled)
//                {
//                    signInCompleted.SetCanceled();
//                }
//                else if (task.IsFaulted)
//                {
//                    signInCompleted.SetException(task.Exception);
//                }
//                else
//                {

//                    Credential credential = GoogleAuthProvider.GetCredential(task.Result.IdToken, null);
//                    auth.SignInWithCredentialAsync(credential).ContinueWith(authTask =>
//                    {
//                        if (authTask.IsCanceled)
//                        {
//                            signInCompleted.SetCanceled();
//                        }
//                        else if (authTask.IsFaulted)
//                        {
//                            signInCompleted.SetException(authTask.Exception);
//                        }
//                        else
//                        {
//                            signInCompleted.SetResult(authTask.Result);

//                        }
//                    });
//                }
//            });
//        }
//    }
//}
