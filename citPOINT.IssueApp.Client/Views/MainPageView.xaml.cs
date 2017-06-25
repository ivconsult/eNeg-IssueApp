#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using citPOINT.IssueApp.Common;
using citPOINT.IssueApp.Data.Web;
using Telerik.Windows.Controls.GridView;
using System.ComponentModel.Composition;
using citPOINT.IssueApp.ViewModel;
using citPOINT.eNeg.Infrastructure.ExceptionHandling;
using citPOINT.eNeg.Apps.Common.Interfaces;
using Telerik.Windows.Controls;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 07.12.11    Yousra         • creation
 * **********************************************
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.IssueApp.Client
{
    /// <summary>
    /// UI responsible for searching for certain issue over 
    /// the all histroy used before for all the users in the system
    /// </summary>
    [Export]
    public partial class MainPageView : UserControl, ICleanup, IObserverApp
    {
        #region → Fields         .

        private IssuesResultView mIssuesResultView;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the issues result view.
        /// </summary>
        /// <value>The issues result view.</value>
        public IssuesResultView IssuesResultView
        {
            get
            {
                if (mIssuesResultView == null)
                {
                    mIssuesResultView = new IssuesResultView();
                }
                return mIssuesResultView;
            }

        }
        /// <summary>
        /// Gets or sets the view model repository.
        /// </summary>
        /// <value>The view model repository.</value>
        private ViewModelRepository ViewModelRepository { get; set; }

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public string AppName
        {
            get { return IssueAppConfigurations.AppName; }
        }

        #endregion

        #region → Constructor    .
        /// <summary>
        /// Initializes a new instance of the <see cref="MainView"/> class.
        /// </summary>
        public MainPageView()
        {
            InitializeComponent();

            #region Registration for needed messages in IssueAppMessanger

            IssueAppMessanger.ChangeScreenMessage.Register(this, OnChangeScreen);
            IssueAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);

            #endregion

            try
            {
                this.ApplyChanges(false);

                IssueAppConfigurations.MainPlatformInfo.TrackChanges.AddObserverApp(this);
            }
            catch (Exception ex)
            {
                IssueAppConfigurations.MainPlatformInfo.HandleException
                    .HandleException(ex, IssueAppConfigurations.AppName);
            }

        }
        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex"></param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            IssueAppConfigurations.MainPlatformInfo.HandleException
                .HandleException(ex, IssueAppConfigurations.AppName);
        }

        /// <summary>
        /// Called when [change screen].
        /// </summary>
        /// <param name="PageName">Name of the page.</param>
        private void OnChangeScreen(string PageName)
        {
            switch (PageName)
            {
                case IssueAppViewTypes.MainView:
                    this.uxgrdLoading.Visibility = System.Windows.Visibility.Collapsed;
                    this.uxMainContent.Content = this.IssuesResultView;
                    this.IssuesResultView.Height = IssueAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height;
                    this.IssuesResultView.uxPhasesGridView.MaxHeight = IssueAppConfigurations.MainPlatformInfo.HostRegionSizeDetails.Height - 100;
                    break;
            }
        }
        #endregion

        #region → Public         .


        /// <summary>
        /// Applies the changes.
        /// </summary>
        /// <param name="isActive">if set to <c>true</c> [is active].</param>
        public void ApplyChanges(bool isActive)
        {
            if (isActive)
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;

                #region → Change Negotiation      .

                if (IssueAppConfigurations.MainPlatformInfo.CurrentNegotiation != null)
                {
                    IssueAppConfigurations.NegotiationParameter = IssueAppConfigurations.MainPlatformInfo.CurrentNegotiation;
                }
                else
                {
                    IssueAppConfigurations.NegotiationParameter = null;
                }

                #endregion

                #region → Change Conversation     .

                //if (IssueAppConfigurations.MainPlatformInfo.CurrentConversation != null)
                //{
                //    IssueAppConfigurations.ConversationIDParameter = IssueAppConfigurations.MainPlatformInfo.CurrentConversation.ConversationID;
                //}
                //else
                //{
                //    IssueAppConfigurations.ConversationIDParameter = Guid.Empty;
                //}

                #endregion

                #region → Change User             .

                //if (IssueAppConfigurations.CurrentLoginUser != null && IssueAppConfigurations.CurrentLoginUser.UserID != IssueAppConfigurations.MainPlatformInfo.UserInfo.UserID)
                //{
                //    if (this.ViewModelRepository != null)
                //    {
                //        this.ViewModelRepository.Cleanup();

                //        this.ViewModelRepository = null;
                //    }
                //}

                //IssueAppConfigurations.CurrentLoginUser = IssueAppConfigurations.MainPlatformInfo.UserInfo;

                #endregion

                #region → View Model Repository   .

                if (ViewModelRepository != null)
                {
                    ViewModelRepository.IssueHistoryViewModel.ApplyChanges();
                }
                else
                {
                    ViewModelRepository = new ViewModelRepository();
                }

                this.DataContext = ViewModelRepository.IssueHistoryViewModel;

                #endregion

            }
            else
            {
                this.uxgrdLoading.Visibility = System.Windows.Visibility.Visible;
            }
        }

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            Messenger.Default.Unregister(this);
        }

        #endregion  Public

        #endregion Methods


    }
}
