using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace ChClient.Services
{
    public class FrameNavigationService : IFrameNavigationService
    {

        #region NavigatonService

        private readonly Dictionary<string, Uri> _pagesByKey;
        private string _currentPageKey;

        public object Parameter { get; private set; }

        public string CurrentPageKey {
            get {
                return _currentPageKey;
            }

            private set {
                if (_currentPageKey == value)
                {
                    return;
                }

                _currentPageKey = value;
                
            }
        }
        
        public void GoBack()
        {
            //üres, nincs kimondottan "vissza" navigálás, helyette más
        }

        public void NavigateTo(string pageKey)
        {
            NavigateTo(pageKey, null);
        }

        public void NavigateTo(string pageKey, object parameter)
        {
            if (!_pagesByKey.ContainsKey(pageKey))
            {
                throw new ArgumentException(string.Format("No such page: {0} ", pageKey), "pageKey");
            }

            var frame = GetDescendantFromName(Application.Current.MainWindow, "MainFrame") as Frame;

            if (frame != null)
            {
                frame.Source = _pagesByKey[pageKey];
            }
            Parameter = parameter;
            CurrentPageKey = pageKey;
        }

        private static FrameworkElement GetDescendantFromName(DependencyObject parent, string name)
        {
            var count = VisualTreeHelper.GetChildrenCount(parent);

            if (count < 1)
            {
                return null;
            }

            for (var i = 0; i < count; i++)
            {
                var frameworkElement = VisualTreeHelper.GetChild(parent, i) as FrameworkElement;
                if (frameworkElement != null)
                {
                    if (frameworkElement.Name == name)
                    {
                        return frameworkElement;
                    }

                    frameworkElement = GetDescendantFromName(frameworkElement, name);
                    if (frameworkElement != null)
                    {
                        return frameworkElement;
                    }
                }
            }
            return null;
        }

        public void Configure(string key, Uri pageType)
        {
            lock (_pagesByKey)
            {
                if (_pagesByKey.ContainsKey(key))
                {
                    _pagesByKey[key] = pageType;
                }
                else
                {
                    _pagesByKey.Add(key, pageType);
                }
            }
        }
        #endregion

        public FrameNavigationService()
        {
            _pagesByKey = new Dictionary<string, Uri>();
        }

    }
}
