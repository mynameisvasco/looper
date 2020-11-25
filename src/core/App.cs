using System;
using WebWindows;
using Microsoft.Extensions.Configuration;
using System.Threading.Tasks;

namespace Looper.Core
{
    public class App
    {
        private readonly IConfiguration _config;
        private readonly Router _router;
        private readonly WebWindow _webview;

        public App(IConfiguration config, WebWindow webview, Router router)
        {
            _config = config;
            _router = router;
            _webview = webview;
            _webview.Title = config.GetValue<string>("Title");
            _webview.Width = config.GetValue<int>("Width");
            _webview.Height = config.GetValue<int>("Height");
        }

        public void Start()
        {
            var entryPointPath = AppDomain.CurrentDomain.BaseDirectory + _config.GetValue<string>("EntryPoint");
            _webview.OnWebMessageReceived += _router.OnRequest;
            _webview.NavigateToLocalFile(entryPointPath);
            _webview.WaitForExit();
        }
    }
}