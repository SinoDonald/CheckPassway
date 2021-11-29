using System;
using System.Collections.Generic;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;


using Autodesk.Revit.DB;
using System.Text.RegularExpressions;
using Autodesk.Revit.UI;
using System.Net.Http;
using System.Net.Http.Headers;
using Newtonsoft.Json.Linq;

namespace CheckPassway
{
    public partial class LoginForm : System.Windows.Forms.Form
    {
        private RevitDocument m_connect = null;
        ExternalEvent m_CheckDimension_ExternalEvent;
        public static bool external = false;
        public static string external_username = "";
        public LoginForm(RevitDocument connect, ExternalEvent check_dimension_event)
        {
            m_CheckDimension_ExternalEvent = check_dimension_event;
            m_connect = connect;
            InitializeComponent();
        }

        private void LoginForm_Load(object sender, EventArgs e)
        {
            external = false;
            webBrowser1.ScriptErrorsSuppressed = true;
            webBrowser1.DocumentCompleted += new WebBrowserDocumentCompletedEventHandler(webBrowser1_DocumentCompleted);
            //string url = "http://127.0.0.1:8000/user/apilogin/";
            //string url = "https://bimdata.sinotech.com.tw/user/apilogin/";
            string url = "http://bimdata.secltd/user/apilogin/";
            webBrowser1.Url = new Uri(url);
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            string response = webBrowser1.getExternalLoginResponse();
            if (!String.IsNullOrWhiteSpace(response))
            {
                string user_id = JObject.Parse(response).SelectToken("user_id").ToString();
                string new_token = JObject.Parse(response).SelectToken("new_token").ToString();
                external_username = JObject.Parse(response).SelectToken("user_name").ToString();
                HttpClient client = new HttpClient();
                //client.BaseAddress = new Uri("http://127.0.0.1:8000/");
                //client.BaseAddress = new Uri("https://bimdata.sinotech.com.tw/");
                client.BaseAddress = new Uri("http://bimdata.secltd/");
                client.DefaultRequestHeaders.Accept.Clear();
                var headerValue = new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json");
                client.DefaultRequestHeaders.Accept.Add(headerValue);
                client.DefaultRequestHeaders.ConnectionClose = true;
                var multiForm = new MultipartFormDataContent();
                multiForm.Add(new StringContent("SinoStation-Passway"), "RevitAPI");
                Task.WaitAll(client.PostAsync($"/user/apilogin/success/"+user_id+"/"+new_token+"/", multiForm));
                PasswayWidth.client = client;
                external = true;
                //this.Close();
                this.Dispose();
                if (external == true)
                {
                    PasswayWidth passway = new PasswayWidth(m_connect, m_CheckDimension_ExternalEvent);
                    passway.Show();
                }
            }
        }
    }
}
