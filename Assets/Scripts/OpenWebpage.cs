using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWebpage : MonoBehaviour
{
    public void OpenUrl(string url = "www.google.com")
    {
        Application.OpenURL(url);
    }
}
