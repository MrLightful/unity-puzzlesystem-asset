#if UNITY_EDITOR

using UnityEngine;
using UnityEditor;

namespace PuzzleSystem.Editor
{

    public class PuzzleSystemEditor : EditorWindow
    {
        [SerializeField]
        private Texture2D logo = null;

        // Menu Controllers
        private int toolBarMain = 0;
        private int toolBarCreation = 0;


        // Styles
        private GUIStyle greyText;
        private GUIStyle linkText;
        private GUIStyle reviewBanner;
        private GUILayoutOption reviewOptions;

        private bool stylesLoaded = false;

        // Creation Tools
        private int triggerCount;

        // Creation from Scripts
        private MonoScript handler;
        private MonoScript logic;
        private MonoScript trigger;

        // Creation from Dropdown
        // ...


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

            greyText = new GUIStyle(EditorStyles.label)
            {
                fontSize = 12,
                normal = { textColor = Color.gray },
                margin = { left = linksMarginLeft }
            };


            linkText = new GUIStyle(EditorStyles.label)
            {
                fontSize = 16,
                normal = { textColor = new Color(0, 95f / 255, 249f / 255) },
                margin = { left = linksMarginLeft }
            };


            reviewBanner = new GUIStyle(EditorStyles.label)
            {
                normal = {
                    background = EditorGUIUtility.whiteTexture,
                    textColor = new Color(100f/255, 100f/255, 100f/255)
                },
                alignment = TextAnchor.MiddleCenter,
                richText = true
            };

            reviewOptions = GUILayout.Height(20);

            stylesLoaded = true;

        }
        
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
            Rect rt = GUILayoutUtility.GetRect(content, reviewBanner, reviewOptions);
            rt.width = EditorGUIUtility.currentViewWidth + 6;
            rt.position += Vector2.left*3;

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            // TODO: AssetStore URL
            if (GUI.Button(rt, content, reviewBanner))
                Application.OpenURL("https://www.assetstore.unity3d.com/");

        }

        private void DisplayCreationMenu()
        {

            EditorGUILayout.Space();
            
            //string[] menuOptions = new string[2];
            //menuOptions[0] = "Scripts";
            //menuOptions[1] = "Dropdown";

            //toolBarCreation = GUILayout.Toolbar(toolBarCreation, menuOptions);


            switch (toolBarCreation) 
            {
                case 0: 
                    DisplayCreatioToolFromScripts(); 
                    break;

                case 1:
                    DisplayCreationToolDropdown();
                    break;
            }


            EditorGUILayout.Space();

            triggerCount = EditorGUILayout.IntField("Number of Triggers: ", Mathf.Abs(triggerCount));

            GUILayout.Space(30);

            if ( GUILayout.Button("Create") ) InstantiateSystem();


        }

        private void DisplayCreatioToolFromScripts() 
        {

            EditorGUILayout.HelpBox("The following scripts must inherit the corresponding core classes. For example, Handler script must inherit CorePuzzleHandler.", MessageType.Info);

            EditorGUILayout.Space();


            handler = EditorGUILayout.ObjectField("Handler Component: ", handler, typeof(MonoScript), false) as MonoScript;
            logic = EditorGUILayout.ObjectField("Logic Component: ", logic, typeof(MonoScript), false) as MonoScript;
            trigger = EditorGUILayout.ObjectField("Trigger Component: ", trigger, typeof(MonoScript), false) as MonoScript;
        }

        private void DisplayCreationToolDropdown() 
        { 
        
        }

        private void DisplayAboutMenu()
        {
            GUILayout.Space(30);


            GUIContent content;
            Rect rt;

            content = new GUIContent("Forum Thread");
            rt = GUILayoutUtility.GetRect(content, linkText);

            // TODO: Forum Thread URL
            if (GUI.Button(rt, content, linkText))
                Application.OpenURL("https://forum.unity3d.com/");

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Discuss the asset & ask questions.", greyText);




            GUILayout.Space(40);



            content = new GUIContent("Documentation");
            rt = GUILayoutUtility.GetRect(content, linkText);

            if (GUI.Button(rt, content, linkText))
                Application.OpenURL("https://puzzlesystem.gitbook.io");

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Learn more about how to use the system.", greyText);



            GUILayout.Space(40);


            content = new GUIContent("Author");
            rt = GUILayoutUtility.GetRect(content, linkText);
            rt.width = EditorGUIUtility.currentViewWidth;

            if (GUI.Button(rt, content, linkText))
                Application.OpenURL("https://flist.me/u/rsirokov");

            EditorGUIUtility.AddCursorRect(rt, MouseCursor.Link);

            GUILayout.Label("Find out more about the author.", greyText);

            GUILayout.Space(30);

        }

        private void InstantiateSystem()
        {

            if (!handler || !logic || !trigger)
            {
                if (!EditorUtility.DisplayDialog("[PuzzleSystem] System Creation Warning",
                                                     "There is at least one empty field that is needed to create a proper puzzle structure. The system will most likely not work properly unless you add it manually. \n\nDo you want to continue the creation without the missing script(-s)?",
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
            if (trigger)
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

            GameObject temp;
            CorePuzzleTrigger[] triggers = new CorePuzzleTrigger[triggerCount];

            for (int i = 0; i < triggerCount; i++)
            {
                temp = new GameObject("Trigger");
                temp.transform.parent = parentObj.transform;

                if (includeCollider)
                    temp.AddComponent<BoxCollider>().isTrigger = true;

                if(trigger) triggers[i] = temp.AddComponent(trigger.GetClass()) as CorePuzzleTrigger;
            }

           if(logicComponent) logicComponent.SetTriggers(triggers);
        }


    }

}

#endif