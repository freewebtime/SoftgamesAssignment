using UnityEngine;
using UnityEngine.UIElements;

namespace Assets.Scripts.App.UI
{
    public abstract class UIScreen : MonoBehaviour
    {
        public virtual void Show()
        {
            gameObject.SetActive(true);
        }

        public virtual void Hide()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// The UIDocument that renders a UI of this screen using UIToolkit
        /// </summary>
        [SerializeField]
        [Tooltip("The UIDocument that renders a UI of this screen using UIToolkit")]
        protected UIDocument UIDocument;

        /// <summary>
        /// Gets the root visual element of the UIDocument, or null if the UIDocument is not assigned
        /// </summary>
        /// <returns></returns>
        protected VisualElement TryGetRootElement()
        {
            if (UIDocument == null)
            {
                return default;
            }

            return UIDocument.rootVisualElement;
        }

        /// <summary>
        /// Sets the provided viewData as the data source for the root visual element of the UIDocument, if the UIDocument is assigned
        /// </summary>
        /// <param name="viewData"></param>
        protected void SetViewDataToUIDocument(object viewData)
        {
            var rootElement = TryGetRootElement();
            if (rootElement != null)
            {
                rootElement.dataSource = viewData;
            }
        }

        protected virtual void OnValidate()
        {
            if (UIDocument == null)
            {
                UIDocument = GetComponent<UIDocument>();
            }
        }
    }

    public abstract class UIScreen<TViewData> : UIScreen
    {
        [Tooltip("The object that contains a data to display in the UI")]
        [SerializeField]
        protected TViewData _viewData;

        /// <summary>
        /// The object that contains a data to display in the UI
        /// </summary>
        public TViewData ViewData
        {
            get => _viewData;
            set
            {
                if (Equals(value, ViewData))
                {
                    return;
                }

                _viewData = value;
                OnViewDataChanged();
            }
        }

        protected virtual void Start()
        {
            OnViewDataChanged();
        }

        public virtual void SetViewData(TViewData viewData)
        {
            ViewData = viewData;
        }

        /// <summary>
        /// Called after ViewData has been changed. 
        /// Sends the ViewData to a UI Document (if provided)
        /// </summary>
        protected virtual void OnViewDataChanged()
        {
            SetViewDataToUIDocument(_viewData);
        }
    }
}