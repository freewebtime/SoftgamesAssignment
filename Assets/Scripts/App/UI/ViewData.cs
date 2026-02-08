using System;
using UnityEngine.UIElements;

namespace Assets.Scripts.App.UI
{
    [Serializable]
    public class ViewData : IDataSourceViewHashProvider
    {
        private long _version;

        /// <summary>
        /// Increases the version of the view data, allowing UI to repaint
        /// </summary>
        public void CommitChanges()
        {
            _version++;
        }

        public long GetViewHashCode()
        {
            return _version;
        }
    }
}