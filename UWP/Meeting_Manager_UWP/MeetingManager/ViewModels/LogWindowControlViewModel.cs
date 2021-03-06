﻿using MeetingManager.Models;
using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.ApplicationModel.DataTransfer;
using Windows.UI.Core;
using Windows.UI.Xaml;

namespace MeetingManager.ViewModels
{
    class LogWindowControlViewModel : ViewModel
    {
        private CoreDispatcher _dispatcher;
        private static int _jsonWindowId;

        public LogWindowControlViewModel()
        {
            if (Windows.ApplicationModel.DesignMode.DesignModeEnabled) return;

            _dispatcher = CoreWindow.GetForCurrentThread().Dispatcher;

            GetEvent<LoginEvent>().Subscribe(LogLoginEvent);
            GetEvent<HttpEvent>().Subscribe(LogHttpEvent);

            LogEntries = new ObservableCollection<EventData>();

            ItemToggleCommand = new DelegateCommand(ToggleItem);
            CopySelectedCommand = new DelegateCommand(CopySelectedItem);
            CopyAllCommand = new DelegateCommand(CopyAllItems);
            ShowJsonCommand = new DelegateCommand(ShowJson);

            IsMainWindow = _dispatcher.Equals((Application.Current as App).MainDispatcher);
        }

        public DelegateCommand ItemToggleCommand { get; }
        public DelegateCommand CopySelectedCommand { get; }
        public DelegateCommand CopyAllCommand { get; }
        public DelegateCommand ShowJsonCommand { get; }

        public ObservableCollection<EventData> LogEntries { get; }
        public EventData SelectedItem { get; set; }

        public bool IsEnabled { get; private set; }

        public bool IsMainWindow { get; }

        public bool HasAnyItems => LogEntries.Any();

        private async void LogLoginEvent(LoginEventData data)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                string loginNavigation = $"Navigated to {data.Url}";
                string authCode = $"Received authorization code: {data.AuthCode}";

                LogEntries.Add(new EventData { StaticText = loginNavigation, IsRequest = true });
                LogEntries.Add(new EventData { StaticText = authCode, IsRequest = false });

                NotifyLogWindow();
            });
        }

        private async void LogHttpEvent(HttpEventData data)
        {
            await _dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
            {
                if (data != null)
                {
                    LogEntries.Add(new EventData { Data = data, IsRequest = true });
                    LogEntries.Add(new EventData { Data = data, IsRequest = false });

                    NotifyLogWindow();
                }
            });
        }

        private void NotifyLogWindow()
        {
            IsEnabled = false;
            OnPropertyChanged(() => IsEnabled);
            IsEnabled = true;
            OnPropertyChanged(() => IsEnabled);

            OnPropertyChanged(() => HasAnyItems);
        }

        private void CopySelectedItem()
        {
            CopyToClipboard(SelectedItem.Text);
        }

        private void CopyAllItems()
        {
            var builder = new StringBuilder();

            foreach (var item in LogEntries)
            {
                builder.AppendLine(item.Text);
            }

            CopyToClipboard(builder.ToString());
        }

        private void CopyToClipboard(string text)
        {
            var dataPackage = new DataPackage();

            dataPackage.RequestedOperation = DataPackageOperation.Copy;

            dataPackage.SetText(text);
            Clipboard.SetContent(dataPackage);
        }

        private void ToggleItem()
        {
            var item = SelectedItem;

            if (item != null)
            {
                item.IsOpened = !item.IsOpened;
                item.NotifyPropertyChanged("Text");
            }
        }

        private async void ShowJson()
        {
            var item = SelectedItem;

            _jsonWindowId = await NewWindow.Create(typeof(Views.JsonPage), "JSON", _jsonWindowId);

            if (item.Data != null)
            {
                GetEvent<ShowJsonEvent>().Publish(item.IsRequest ? item.Data.RequestBody : item.Data.ResponseBody);
            }
            else
            {
                GetEvent<ShowJsonEvent>().Publish(item.Text);
            }
        }

        public class EventData : INotifyPropertyChanged
        {
            public bool IsOpened { get; set; }
            public bool IsRequest { get; set; }

            public string Text
            {
                get
                {
                    if (Data == null)
                    {
                        return StaticText;
                    }
                    else if (IsRequest)
                    {
                        return IsOpened ? GetOpenedRequest() : GetClosedRequest();
                    }
                    else
                    {
                        return IsOpened ? GetOpenedResponse() : GetClosedResponse();
                    }
                }
            }

            public string StaticText { get; set; }

            public HttpEventData Data { get; set; }

            public event PropertyChangedEventHandler PropertyChanged;

            public void NotifyPropertyChanged(string propertyName)
            {
                this.Notify(PropertyChanged, propertyName);
            }

            private string GetOpenedRequest()
            {
                var body = string.Format(" Body = <{0}>\r\n Headers = {1}", Data.RequestBody, Data.RequestHeaders);

                return string.Format("{0}\r\n{1}", GetClosedRequest(), body);
            }

            private string GetClosedRequest()
            {
                return string.Format("{0:HH:mm:ss}  {1} {2}", Data.TimeStamp, Data.Method, Data.Uri);
            }

            private string GetOpenedResponse()
            {
                var body = string.Format(" Body = <{0}>\r\n Headers = {1}", Data.ResponseBody, Data.ResponseHeaders);

                return string.Format("{0}\r\n{1}", GetClosedResponse(), body);
            }

            private string GetClosedResponse()
            {
                return Data.StatusCode;
            }
        }
    }
}
