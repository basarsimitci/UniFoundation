using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEditor.UIElements;
using UnityEngine;
using UnityEngine.UIElements;

namespace UniFoundation.AssetBundles.Editor
{
    public class AssetBundlesBuilder : EditorWindow
    {
        private const string BuildFolderBase = "Build/AssetBundles";
        private const string ConfigFileName = "assetBundlesConfig.json";
    
        //Templates
        private const string WindowTemplatePath = "Assets/Scripts/UniFoundation/AssetBundles/Editor/AssetBundlesBuilder.uxml";
        private VisualTreeAsset windowTemplate;
    
        //Styles
        private const string StyleSheetPath = "Assets/Scripts/UniFoundation/AssetBundles/Editor/AssetBundlesBuilder.uss";
    
        //
        private string buildFolder;
        private AssetBundlesConfig config;
        private BuildTarget targetPlatform;
        private string[] assetBundleNames;

        [MenuItem("Assets/Build Asset Bundles")]
        public static void ShowWindow()
        {
            AssetBundlesBuilder wnd = GetWindow<AssetBundlesBuilder>();
            wnd.titleContent = new GUIContent("Asset Bundles Builder");
        }

        public void OnEnable()
        {
            targetPlatform = EditorUserBuildSettings.activeBuildTarget;
            assetBundleNames = AssetDatabase.GetAllAssetBundleNames();
            SetBuildFolder(targetPlatform);
            LoadConfig(assetBundleNames);
            
            LoadTemplates();
            LoadStyles();

            rootVisualElement.name = "body";
                
            VisualElement window = windowTemplate.CloneTree();
            rootVisualElement.Add(window);
        
            VisualElement assetBundlesTable = rootVisualElement.Query<VisualElement>("assetBundlesTable").First();
            if (assetBundlesTable != null)
            {
                foreach (string assetBundleName in assetBundleNames)
                {
                    
                    
                    VisualElement row = new VisualElement();
                    row.AddToClassList("row");
                    row.AddToClassList("listItem");

                    Label assetBundleLabel = new Label(assetBundleName);
                    assetBundleLabel.AddToClassList("column200");
                    row.Add(assetBundleLabel);
                    
                    TextField assetBundleVersion = new TextField();
                    assetBundleVersion.AddToClassList("column100");
                    assetBundleVersion.name = assetBundleName + "Version";
                    assetBundleVersion.value = config.AssetBundleInfos
                        .FirstOrDefault(info => info.Name == assetBundleName)?.Version.ToString();
                    row.Add(assetBundleVersion);
                
                    assetBundlesTable.Add(row);
                }
            }

            EnumField platform = rootVisualElement.Query<EnumField>("platform");
            if (platform != null)
            {
                platform.value = EditorUserBuildSettings.activeBuildTarget;
                platform.RegisterValueChangedCallback(OnTargetPlatformChanged);
            }

            Button buildButton = new Button(OnBuildClicked) {name = "buildButton", text = "Build"};
            rootVisualElement.Add(buildButton);
        }

        private void SetBuildFolder(BuildTarget target)
        {
            buildFolder = Path.Combine(Application.dataPath.Replace("Assets", BuildFolderBase),
                target.ToString());
        }

        private void LoadConfig(string[] bundleNames)
        {
            try
            {
                string configJson = File.ReadAllText(Path.Combine(buildFolder, ConfigFileName));
                config = JsonUtility.FromJson<AssetBundlesConfig>(configJson);
                
                
            }
            catch (Exception)
            {
                config = new AssetBundlesConfig
                {
                    AssetBundleInfos = new AssetBundleInfo[bundleNames.Length]
                };
                for (int assetBundleIndex = 0; assetBundleIndex < bundleNames.Length; assetBundleIndex++)
                {
                    config.AssetBundleInfos[assetBundleIndex] = new AssetBundleInfo
                    {
                        Name = bundleNames[assetBundleIndex]
                    };
                }
            }
        }

        private void LoadTemplates()
        {
            windowTemplate = AssetDatabase.LoadAssetAtPath<VisualTreeAsset>(WindowTemplatePath);
        }
    
        private void LoadStyles()
        {
            StyleSheet styleSheet = AssetDatabase.LoadAssetAtPath<StyleSheet>(StyleSheetPath);
            if (styleSheet != null)
            {
                rootVisualElement.styleSheets.Add(styleSheet);
            }
        }

        private void OnTargetPlatformChanged(ChangeEvent<Enum> targetChangedEvent)
        {
            targetPlatform = (BuildTarget) targetChangedEvent.newValue;
        }

        private void OnBuildClicked()
        {
            if (Directory.Exists(buildFolder) == false)
            {
                Directory.CreateDirectory(buildFolder);
            }

            //BuildPipeline.BuildAssetBundles(buildFolder, BuildAssetBundleOptions.None, targetPlatform);
            
            GenerateAssetBundlesConfig();
        }

        private void GenerateAssetBundlesConfig()
        {
            if (config == null)
            {
                config = new AssetBundlesConfig
                {
                    AssetBundleInfos = new AssetBundleInfo[assetBundleNames.Length]
                };
            }

            for (int assetBundleIndex = 0; assetBundleIndex < assetBundleNames.Length; assetBundleIndex++)
            {
                string assetBundleName = assetBundleNames[assetBundleIndex];

                AssetBundleInfo info = new AssetBundleInfo {Name = assetBundleName};

                TextField baseUriInput = rootVisualElement.Query<TextField>("baseUri");
                string uri = baseUriInput != null ? baseUriInput.value : "";
                if (uri.EndsWith("/") == false)
                {
                    uri += "/";
                }
                uri += targetPlatform.ToString();
                uri += "/" + assetBundleName;
                info.Uri = uri;

                TextField versionInput = rootVisualElement.Query<TextField>(assetBundleName + "Version");
                uint.TryParse(versionInput.value, out info.Version);

                config.AssetBundleInfos[assetBundleIndex] = info;
            }
            
            File.WriteAllText(Path.Combine(buildFolder, "assetBundlesConfig.json"), JsonUtility.ToJson(config));
        }
    }
}