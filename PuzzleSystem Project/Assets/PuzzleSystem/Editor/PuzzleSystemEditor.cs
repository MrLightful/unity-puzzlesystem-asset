#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace PuzzleSystem.Editor
{

    public class PuzzleSystemEditor : EditorWindow
    {

        #region Variables

        [SerializeField]
        private Texture2D logo = null;

        // Menu Controllers
        private int toolBarMain = 0;
        private int toolBarTriggerType = 0;


        // Styles
        private GUIStyle greyTextStyle;
        private GUIStyle linkTextStyle;
        private GUIStyle reviewBannerStyle;
        private GUILayoutOption reviewBannerOptions;

        private bool stylesLoaded = false;

        // Creation Tool
        private int triggerCount;

        private MonoScript handler;
        private MonoScript logic;
        private MonoScript trigger;
        private GameObject triggerPrefab;

        // URLS
        private const string ASSET_URL = "https://assetstore.unity.com/";
        private const string FORUM_URL = "https://forum.unity.com/";
        private const string DOC_URL = "https://puzzlesystem.gitbook.io/";
        private const string AUTHOR_URL = "https://flist.me/u/rsirokov";

        #endregion


        #region Window Setup

        [MenuItem("Tools/Puzzle System")]
        private static void InstantiateWindow() 
        {
            PuzzleSystemEditor myWindow = (PuzzleSystemEditor)GetWindow(typeof(PuzzleSystemEditor), false, "PuzzleSystem Utility");
            myWindow.Show();
            myWindow.Focus();
            myWindow.Repaint();
        }

        private void OnEnable()
        {
            EditorApplication.hierarchyChanged += HierarchyChanged;
        }

        private void OnDisable()
        {
            EditorApplication.hierarchyChanged -= HierarchyChanged;
        }

        private void HierarchyChanged()
        {
            this.Repaint();
        }

        void LoadStyles()
        {

            int linksMarginLeft = 20;

            greyTextStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                normal = { textColor = Color.gray },
                margin = { left = linksMarginLeft }
            };


            linkTextStyle = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                normal = { textColor = new Color(0, 95f / 255, 249f / 255) },
                margin = { left = linksMarginLeft }
            };


            reviewBannerStyle = new GUIStyle(EditorStyles.label)
            {
                normal = {
                    background = EditorGUIUtility.whiteTexture,
                    textColor = new Color(100f/255, 100f/255, 100f/255)
                },
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            reviewBannerOptions = GUILayout.Height(20);

            stylesLoaded = true;

        }


        #endregion


        #region Window Display

        private void OnGUI()
        {
            if (!stylesLoaded) LoadStyles();

            GUILayout.Space(20);


            // Main Toolbar Section
            string[] menuOptions = new string[2];
            menuOptions[0] = "Create";
            menuOptions[1] = "Help";

            toolBarMain = GUILayout.Toolbar(toolBarMain, menuOptions);

            EditorGUILayout.Space();


            // Main Toolbar Content

            switch (toolBarMain) 
            {
                case 0: 
                    DisplayCreationMenu(); 
                    break;

                case 1:
                    DisplayAboutMenu();
                    break;
            
            }

            GUILayout.Space(30);


            // Footer

            GUILayout.FlexibleSpace();

            GUILayout.Label(logo, EditorStyles.centeredGreyMiniLabel);
            EditorGUILayout.Space();
            GUILayout.Label("Version 1.0.0", EditorStyles.centeredGreyMiniLabel);
            GUILayout.Space(30);

            GUIContent content = new GUIContent("<size=11>★   Please, consider leaving a review for the asset.   ★</size>");
            Rect rt = GUILayoutUtility.GetRect(content, reviewBannerStyle, reviewBannerOptions);
            rt.width = EditorGUIUtility.currentViewWidth + 6;
            rt.position += Vector2.left*3;

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            if (GUI.Button(rt, content, reviewBannerStyle))
                Application.OpenURL(ASSET_URL);

        }

        private void DisplayCreationMenu()
        {

            EditorGUILayout.Space();

            DisplayCreatioToolFromScripts();

            GUILayout.Space(30);

            if ( GUILayout.Button("Create") ) InstantiateSystem();


        }

        private void DisplayCreatioToolFromScripts() 
        {

            EditorGUILayout.HelpBox("The following scripts must inherit the corresponding core classes. For example, Handler script must inherit CorePuzzleHandler.", MessageType.Info);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("Parent Object: ", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            handler = EditorGUILayout.ObjectField(new GUIContent("Handler Component: ", "Specify the component that will define the role of the handler.\n\nThis script will be added to the parent object of the puzzle.\n\nNB! Must inherit CorePuzzleHandler."), handler, typeof(MonoScript), false) as MonoScript;
            logic = EditorGUILayout.ObjectField(new GUIContent("Logic Component: ", "Specify the component that will define the role of the logic.\n\nThis script will be added to the parent object of the puzzle.\n\nNB! Must inherit CorePuzzleLogic."), logic, typeof(MonoScript), false) as MonoScript;

            GUILayout.Space(30);

            EditorGUILayout.LabelField("Trigger Objects: ", EditorStyles.boldLabel);
            EditorGUILayout.Space();

            EditorGUILayout.BeginHorizontal();

            if (toolBarTriggerType == 1)
                trigger = EditorGUILayout.ObjectField(new GUIContent("Component: ", "Specify the component that will define the role of the triggers.\n\nThis script will be added to all trigger objects of the puzzle.\n\nNB! Must inherit CorePuzzleTrigger."), trigger, typeof(MonoScript), false) as MonoScript;
            else
                triggerPrefab = EditorGUILayout.ObjectField(new GUIContent("Prefab: ", "Specify the prefab that will be duplicated into the system.\n\nNB! Must own a component that inherit from CorePuzzleTrigger."), triggerPrefab, typeof(GameObject), false) as GameObject;

            toolBarTriggerType = GUILayout.Toolbar(toolBarTriggerType, new string[] { "Prefabs", "Script" }, GUILayout.Width(EditorGUIUtility.currentViewWidth / 4));

            EditorGUILayout.EndHorizontal();

            EditorGUILayout.Space();
            triggerCount = EditorGUILayout.IntField(new GUIContent("Number of Triggers: ", "Specify the number of triggers that you want to create for the puzzle."), Mathf.Abs(triggerCount));
        }


        private void DisplayAboutMenu()
        {
            GUILayout.Space(30);


            GUIContent content;
            Rect rt;

            content = new GUIContent("Forum Thread");
            rt = GUILayoutUtility.GetRect(content, linkTextStyle);

            // TODO: Forum Thread URL
            if (GUI.Button(rt, content, linkTextStyle))
                Application.OpenURL(FORUM_URL);

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Discuss the asset & ask questions.", greyTextStyle);




            GUILayout.Space(40);



            content = new GUIContent("Documentation");
            rt = GUILayoutUtility.GetRect(content, linkTextStyle);

            if (GUI.Button(rt, content, linkTextStyle))
                Application.OpenURL(DOC_URL);

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Learn more about how to use the system.", greyTextStyle);



            GUILayout.Space(40);


            content = new GUIContent("Author");
            rt = GUILayoutUtility.GetRect(content, linkTextStyle);
            rt.width = EditorGUIUtility.currentViewWidth;

            if (GUI.Button(rt, content, linkTextStyle))
                Application.OpenURL(AUTHOR_URL);

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Find out who is the author and his contacts.", greyTextStyle);

            GUILayout.Space(30);

        }

        #endregion


        #region System Instantiation

        private void InstantiateSystem()
        {

            if ( !handler || !logic || (!trigger && !triggerPrefab) )
            {
                if (!EditorUtility.DisplayDialog("[PuzzleSystem] System Creation Warning",

                                                 "There is at least one empty field passed into the . The system will most likely work improperly unless you add missing components by yourself later. \n\nDo you want to continue the creation without the missing component(-s)?",

                                                 "Continue", "Cancel"))
                {
                    return;
                }
            }


            if (handler)
            {

                if (!handler.GetClass().IsSubclassOf(typeof(CorePuzzleHandler)) && handler.GetClass() != typeof(CorePuzzleHandler))
                {
                    EditorUtility.DisplayDialog("[PuzzleSystem] System Creation Failed",

                                                "The creation of the puzzle system has failed becuase inputted Handler component does not inherit its core elements.\n\nPlease, make sure that the script you are choosing is a subclass of CorePuzzleHandler.",
                                               
                                                "Ok");
                    handler = null;
                    return;
                }

            }

            if (logic)
            {

                if (!logic.GetClass().IsSubclassOf(typeof(CorePuzzleLogic)) && logic.GetClass() != typeof(CorePuzzleLogic))
                {
                    EditorUtility.DisplayDialog("[PuzzleSystem] Failed System Creation",

                                                "The creation of the puzzle system has failed becuase inputted Logic component does not inherit its core elements.\n\nPlease, make sure that the script you are choosing is a subclass of CorePuzzleLogic.",

                                                "Ok");
                    logic = null;
                    return;
                }
            }

            bool includeCollider = false;
            if (trigger && toolBarTriggerType == 1)
            {

                if (!trigger.GetClass().IsSubclassOf(typeof(CorePuzzleTrigger))  && trigger.GetClass() != typeof(CorePuzzleTrigger))
                {
                    EditorUtility.DisplayDialog("[PuzzleSystem] Failed System Creation",

                                                "The creation of the puzzle system has failed becuase inputted Trigger component does not inherit its core elements.\n\nPlease, make sure that the script you are choosing is a subclass of CorePuzzleTrigger.",
                                               
                                                "Ok");
                    trigger = null;
                    return;
                }

                includeCollider = trigger.GetClass().IsSubclassOf(typeof(CoreColliderBasedPuzzleTrigger));

            }


            // PuzzleSystem Creation -- parent object & triggers

            GameObject parentObj = new GameObject("PuzzleSystem");
            if(handler) parentObj.AddComponent(handler.GetClass());
            CorePuzzleLogic logicComponent =  logic ? parentObj.AddComponent(logic.GetClass()) as CorePuzzleLogic : null;

            GameObject temp = null;
            CorePuzzleTrigger tempTriggerComponent = null;

            CorePuzzleTrigger[] triggers = new CorePuzzleTrigger[triggerCount];

            for (int i = 0; i < triggerCount; i++)
            {
                if (toolBarTriggerType == 1)
                {
                    temp = new GameObject("Trigger");

                    if (includeCollider)
                        temp.AddComponent<BoxCollider>().isTrigger = true;

                    tempTriggerComponent = temp.AddComponent(trigger.GetClass()) as CorePuzzleTrigger;

                } else if(triggerPrefab)
                {
                    temp = Instantiate(triggerPrefab);
                    tempTriggerComponent = temp.GetComponent<CorePuzzleTrigger>();
                }

                if(temp) temp.transform.parent = parentObj.transform;

                if (tempTriggerComponent) triggers[i] = tempTriggerComponent;

            }


            if (toolBarTriggerType == 0 && triggers.Length > 0 && !triggers[0])
            {
                EditorUtility.DisplayDialog("[PuzzleSystem] Warning System Creation",

                                                       "The prefab of the trigger, that was passed in for creating a system, does not contain any script that inherit CorePuzzleTrigger." +
                                                       "\n\nThis means that the instantiated objects will not play the role of the triggers in the created puzzle system.",

                                                       "Ok");
                return;
            }

            if (logicComponent) logicComponent.SetTriggers(triggers);
        }


    }

    #endregion

}

#endif