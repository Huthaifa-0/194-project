using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
namespace ABKaspo.Assets.AURW.AURW_Editor
{
    public class AURW_MeduBarEditor : EditorWindow
    {
        [MenuItem("ABKaspo/About/Contact Us/YouTube Channel")]
        public static void OpenYoutubeChannel()
        {
            Application.OpenURL("https://www.youtube.com/@ABKaspo-Games");
        }
        [MenuItem("ABKaspo/About/Contact Us/Instagram")]
        public static void OpenInstagramAccount()
        {
            Application.OpenURL("https://www.instagram.com/abkaspo_games");
        }
        [MenuItem("ABKaspo/About/Contact Us/Send An E-mail")]
        public static void SendAnEmail()
        {
            Application.OpenURL("mailto:abkaspo@gmail.com");
        }
        [MenuItem("ABKaspo/About/Documentation")]
        public static void OpenDocumentation()
        {
            Application.OpenURL("https://sites.google.com/view/abkaspogames/aurw");
        }
        [MenuItem("ABKaspo/Report a Bug")]
        public static void ReportABug()
        {
            Application.OpenURL("https://sites.google.com/view/abkaspogames/client-support");
        }
    }
}