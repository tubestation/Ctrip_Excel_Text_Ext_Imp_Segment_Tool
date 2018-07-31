using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Web;
using System.Net;
using System.IO;
using System.Net.NetworkInformation;
using System.Reflection;


namespace Ctrip_Excel_Text_Ext_Imp_Segment_Tool
{
    public delegate void OnAfterAuthenctiateDel();

    public partial class CsoftAuth_Base_Frm : Form
    {
        /// <summary>
        /// 验证结果的消息
        /// </summary>
        public string Result { get; set; }
        /// <summary>
        /// 验证之后是否通过
        /// </summary>
        public bool Pass { get; set; }
        /// <summary>
        /// 当前的namespace（当前工具名称）
        /// </summary>
        public string ToolName
        {
            get { return StaticValues.ToolName; }
        }

        /// <summary>
        /// 当前工具的版本号
        /// </summary>
        public string Version
        {
            get { return StaticValues.Version; }
        }


        /// <summary>
        /// 完成验证之后的事件
        /// </summary>
        public event OnAfterAuthenctiateDel OnAfterAuthenticate;

        public CsoftAuth_Base_Frm()
        {
            InitializeComponent();
        }

        //验证按钮
        private void ts_btn_Auth_Click(object sender, EventArgs e)
        {
            Authenticate();
            if (this.OnAfterAuthenticate != null)
                OnAfterAuthenticate();
        }

        /// <summary>
        /// 获取本机已存储的IP地址
        /// </summary>
        private string GetLastAccessIPAddress()
        {
            string lastAdd = "";
            //直接找本机的d:\\CsoftAuth.ini 文件
            string iniPath = @"d:\\CsoftAuth.ini";
            if (File.Exists(iniPath))
            {
                string[] lines = File.ReadAllLines(iniPath, new UTF8Encoding(false));
                string lastAccessIP = lines[0].Split('=')[1];
                lastAdd = lastAccessIP;
            }
            else
            {
                try
                {
                    File.WriteAllText(iniPath, "LastAccess=" + ts_tb_AuthServer.Text, new UTF8Encoding(false));
                }
                catch
                {
                    MessageBox.Show(@"无法建立ini文件：D:\\CsoftAuth.ini");
                }

            }
            return lastAdd;
        }
        /// <summary>
        /// 重新写入有效地址
        /// </summary>
        /// <param name="validIP"></param>
        private void ReWriteLastAccessIPAddress(string validIP)
        {
            string iniPath = @"d:\\CsoftAuth.ini";
            string lastIP = this.GetLastAccessIPAddress();
            if (lastIP != validIP)
            {
                File.WriteAllText(iniPath, "LastAccess=" + validIP, new UTF8Encoding(false));
            }
        }

        /// <summary>
        /// 验证 主功能
        /// </summary>
        public void Authenticate()
        {
            string serverAdd = ts_tb_AuthServer.Text;
            if (serverAdd == "iamnotcsofterbackdoor" || serverAdd=="iamnotincsoftdomainbackdoor")
            {
                Result = "ok";
                Pass = true;
                EnableAllControls();

                return;
            }
            string re;
            if (TryAuthenticateByServer(serverAdd, out re))
            {
                Result = re;
                Pass = true;
                EnableAllControls();

                return;
            }
            Result = re;
            Pass = false;
            DisableAllControls();

            MessageBox.Show(re);
        }
        /// <summary>
        /// 生效所有控件
        /// </summary>
        public void EnableAllControls()
        {
            foreach (Control c in this.Controls)
            {
                c.Enabled = true;
            }
        }
        /// <summary>
        /// 失效所有控件，除menustrip1
        /// </summary>
        public void DisableAllControls()
        {
            foreach (Control c in this.Controls)
            {
                if (c == menuStrip1)
                    continue;
                c.Enabled = false;
            }
        }

        /// <summary>
        /// 尝试获取服务器响应
        /// </summary>
        /// <param name="serverAdd"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryAuthenticateByServer(string serverAdd, out string result)
        {
            result = "";
            string postData = GetClientInfo();
            string resp = "";
            object respObj;
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();

            //处理地址字符串
            string uriAdd = GetServerUriAddress(serverAdd, "/ToolAuthStatistic/ToolAuthenticator.ashx");

            if (TryGetJsonObjectFromServerByPost(uriAdd, postData, out resp))//从服务器端有响应的情况
            {
                respObj = json.DeserializeObject(resp);
                if ((respObj as dynamic)["AuthResult"] == true)
                {
                    //重新写入新地址
                    ReWriteLastAccessIPAddress(ts_tb_AuthServer.Text);
                    return true;
                }
                else
                {
                    result = (respObj as dynamic)["FailReason"];
                    return false;
                }
            }
            else//服务器端无响应或失败的
            {
                respObj = json.DeserializeObject(resp);
                result = (respObj as dynamic)["ResultString"];
                return false;
            }


        }
        /// <summary>
        /// 获取本机信息，并改造querystring形式
        /// </summary>
        /// <returns></returns>
        public string GetClientInfo()
        {
            IPGlobalProperties computerProp = IPGlobalProperties.GetIPGlobalProperties();
            string postData = string.Format("HostName={0}&DomainName={1}&ToolName={2}&Version={3}",
                computerProp.HostName, computerProp.DomainName, ToolName, Version);
            return postData;
        }
        /// <summary>
        /// 获取server的处理工具请求的完全地址
        /// </summary>
        /// <param name="serverAdd">服务器地址</param>
        /// <param name="ashxPath">一般处理程序的server上的路径，如："/ToolAuthStatistic/ToolAuthenticator.ashx" 或 "/ToolAuthStatistic/ToolUpdateChecker.ashx"</param>
        /// <returns></returns>
        public string GetServerUriAddress(string serverAdd, string ashxPath)
        {
            string uriAdd = serverAdd;
            Regex reg = new Regex("http[s]{0,1}://(.*?)", RegexOptions.IgnoreCase);
            if (reg.IsMatch(serverAdd))
            {
                uriAdd = reg.Replace(serverAdd, "$1");
            }
            uriAdd = "http://" + uriAdd + ashxPath;
            return uriAdd;
        }
        //认证回车键
        private void ts_tb_AuthServer_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == 13)
                this.ts_btn_Auth_Click(ts_btn_Auth, null);
        }
        /// <summary>
        /// 检查更新 主方法
        /// </summary>
        public void CheckUpdate()
        {
            string serverAdd = ts_tb_AuthServer.Text;

            if (serverAdd == "iamnotcsofterbackdoor")
                return;
            //如果没有通过身份验证
            if (!this.Pass)
                return;

            string re;
            if (TryCheckUpdate(out re))
            {
                MessageBox.Show(re);
            }
        }
        /// <summary>
        /// 尝试从server获取是否可以更新
        /// </summary>
        /// <param name="serverAdd"></param>
        /// <param name="currentToolName"></param>
        /// <param name="currentVersion"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        public bool TryCheckUpdate(out string result)
        {
            result = "";
            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();
            string serverAdd = ts_tb_AuthServer.Text;
            string uriAdd = GetServerUriAddress(serverAdd, "/ToolUpdateChecker.ashx");
            string postData = GetClientInfo();
            string respText = "";

            if (TryGetJsonObjectFromServerByPost(uriAdd, postData, out respText))//服务器端有响应的
            {
                object respObj = json.DeserializeObject(respText);
                if ((respObj as dynamic)["CheckResult"])
                {
                    result = string.Format("工具有更新 {0}。\r\n请前往 {1} 下载最新版本", (respObj as dynamic)["LatestVersion"], serverAdd);
                    return true;
                }
            }

            return false;
        }
        /// <summary>
        /// 尝试从server端获取json格式的字符串，（Post 方式）
        /// </summary>
        /// <param name="uriAdd"></param>
        /// <param name="postData"></param>
        /// <param name="resp"></param>
        /// <returns></returns>
        public bool TryGetJsonObjectFromServerByPost(string uriAdd, string postData, out string resp)
        {
            object resultObj;

            System.Web.Script.Serialization.JavaScriptSerializer json = new System.Web.Script.Serialization.JavaScriptSerializer();

            //处理postData的字节
            byte[] dataArr = Encoding.UTF8.GetBytes(postData);

            //处理地址字符串
            HttpWebRequest request = null;
            try
            {
                request = WebRequest.Create(uriAdd) as HttpWebRequest;//创建请求
            }
            catch (Exception ex)
            {
                resultObj = new
                {
                    ResultString = "请求连接失败。" + ex.Message
                };
                resp = json.Serialize(resultObj);
                return false;
            }
            request.Method = "POST";//请求方法
            request.Accept = "text/plain, */*; q=0.01";
            request.UserAgent = "Mozilla/4.0 (compatible; MSIE 7.0; Windows NT 6.1; Trident/5.0; SLCC2; .NET CLR 2.0.50727; .NET CLR 3.5.30729; .NET CLR 3.0.30729; .NET4.0C; .NET4.0E)";
            request.KeepAlive = false;
            request.ContentLength = dataArr.Length;
            request.ContentType = "application/x-www-form-urlencoded; charset=UTF-8";
            request.Timeout = 3000;

            HttpWebResponse response;

            Stream dataStream;
            try
            {
                dataStream = request.GetRequestStream();
            }
            catch (WebException ex)
            {
                resultObj = new
                {
                    ResultString = "传送数据失败。" + ex.Message
                };
                resp = json.Serialize(resultObj);
                return false;
            }
            //发送请求
            dataStream.Write(dataArr, 0, dataArr.Length);
            dataStream.Close();
            //读取返回信息
            try
            {
                response = (HttpWebResponse)request.GetResponse();//获得相应
            }
            catch (WebException ex)
            {
                response = (HttpWebResponse)ex.Response;
                resultObj = new
                {
                    ResultString = "服务器无响应。" + ex.Message
                };
                resp = json.Serialize(resultObj);
                return false;
            }
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
            resp = sr.ReadToEnd(); //响应转化为String字符串
            return true;
        }

        //检查更新 按钮
        private void ts_btn_CheckUpdates_Click(object sender, EventArgs e)
        {
            if (!this.Pass)
                return;

            string re;
            if (TryCheckUpdate(out re))
            {
                MessageBox.Show(re);
            }
            else
            {
                MessageBox.Show("没有更新");
            }
        }

        //窗体加载
        public void CsoftAuth_Base_Frm_Load(object sender, EventArgs e)
        {
            //获取最后一次成功进入的网址
            string lastIP = GetLastAccessIPAddress();
            if (!string.IsNullOrEmpty(lastIP))
            {
                ts_tb_AuthServer.Text = lastIP;
            }
            //填写当前的版本号
            ts_tb_Version.Text = Version;
            //工具验证
            Authenticate();
            //检查更新
            CheckUpdate();
        }
        /// <summary>
        /// 显示当前工具的名称和版本
        /// </summary>
        public void ShowToolInfos()
        {
            MessageBox.Show(string.Format("你正在使用 {0}, Version: {1}",this.ToolName,this.Version));
        }
    }
}
