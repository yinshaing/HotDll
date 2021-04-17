using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;
using UnityEngine.UI;

public class StartGame : MonoBehaviour {

	private AndroidJavaClass _jc;
	private string _packageName;
	private string _dllPath;
	public Text txt;
	// Use this for initialization
	void Start () {
		_jc = new AndroidJavaClass("com.test.hotdll.TestDll");
		_packageName = _jc.CallStatic<string>("GetPackageName", new object[0]);
		_dllPath = "/data/data/" + _packageName + "/Assembly-CSharp.dll";
		var sb = new StringBuilder();
		sb.AppendLine("初始化完成");
		sb.AppendLine(string.Format("当前包名:{0}", _packageName));
		sb.AppendLine(string.Format("DLL存储地址:{0}", _dllPath));
		sb.AppendLine(string.Format("加密后的热更"));
		Debug.Log(sb.ToString());
		txt.text = sb.ToString();
		Debug.Log(txt);
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void btnUpdateClick()
    {
		StartCoroutine(UpdateMethod());
    }

	public IEnumerator UpdateMethod()
    {
		using(var www = new WWW("http://192.168.21.37:13012/Assembly-CSharp.dll"))
        {
			yield return www;

			File.WriteAllBytes(_dllPath, www.bytes);
			txt.text = "下载DLL完成";
		}
    }

	public void btnDelete()
	{
		File.Delete(_dllPath);
		txt.text = "删除DLL完成";
	}

	public void btnReboot()
	{
		_jc.CallStatic("Reboot", new object[0]);
	}
}
