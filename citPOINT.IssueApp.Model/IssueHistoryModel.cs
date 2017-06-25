
#region → Usings   .
using System;
using System.ComponentModel;
using System.Linq;
using System.Collections.Generic;
using citPOINT.eNeg.Common;
using System.ServiceModel.DomainServices.Client;
using System.ComponentModel.Composition;
using System.Threading;
using citPOINT.IssueApp.Data.Web;
using citPOINT.IssueApp.Common;

#endregion

#region → History  .

/* 
 * Date         User            Change
 * *********************************************
 * 03.01.12     Yousra         • creation
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

namespace citPOINT.IssueApp.Model
{
    #region  Using MEF to export IssueHistoryModel
    /// <summary>
    /// Model Layer for Issue History Search.
    /// </summary>
    [Export(typeof(IIssueHistoryModel))]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class IssueHistoryModel : IIssueHistoryModel
    {
        #region → Fields         .
        private IssueAppContext mContext;
        private Boolean mIsBusy = false;
        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the context.
        /// </summary>
        /// <value>The context.</value>
        private IssueAppContext Context
        {
            get
            {
                if (mContext == null)
                {
                   // mContext = new IssueAppContext();
                    mContext = new IssueAppContext(IssueAppConfigurations.MainServiceUri);

                    mContext.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(ctx_PropertyChanged);
                }

                return mContext;
            }
        }

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        public Boolean IsBusy
        {
            get
            {
                return this.mIsBusy;
            }

            private set
            {
                if (this.mIsBusy != value)
                {
                    this.mIsBusy = value;
                    this.OnPropertyChanged("IsBusy");
                }
            }
        }

        #endregion Properties

        #region → Event Handlers .

        /// <summary>
        /// Private Event Handler that called after any change in 
        /// HasChanges, IsLoading, IsSubmitting properties
        /// </summary>
        /// <param name="sender">Value of Sender</param>
        /// <param name="e">Value of e</param>
        private void ctx_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case "IsLoading":
                    this.IsBusy = mContext.IsLoading;
                    break;
                case "IsSubmitting":
                    this.IsBusy = mContext.IsSubmitting;
                    break;
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Event Handler For Method PropertyChanged
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// Occurs when [get message type complete].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<IssueHistory>> GetIssuesHistoryComplete;

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Private Method used to perform query on certain entity of eNeg Entities
        /// </summary>
        /// <typeparam name="T">Value Of T</typeparam>
        /// <param name="qry">Value Of qry</param>
        /// <param name="evt">Value Of evt</param>
        private void PerformQuery<T>(EntityQuery<T> qry, EventHandler<eNegEntityResultArgs<T>> evt) where T : Entity
        {
            Context.Load<T>(qry, LoadBehavior.RefreshCurrent, r =>
            {
                if (evt != null)
                {
                    try
                    {
                        if (r.HasError)
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Error));
                            r.MarkErrorAsHandled();
                        }
                        else
                        {
                            evt(this, new eNegEntityResultArgs<T>(r.Entities));
                        }
                    }
                    catch (Exception ex)
                    {
                        evt(this, new eNegEntityResultArgs<T>(ex));
                    }
                }
            }, null);
        }

        #endregion

        #region → Protected      .

        #region INotifyPropertyChanged Interface implementation

        /// <summary>
        /// Handle changes in any Property
        /// </summary>
        /// <param name="propertyName">Property Name</param>
        protected virtual void OnPropertyChanged(string propertyName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }

        #endregion

        #endregion

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the issue history async.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID">The current negotiation ID.</param>
        public void GetIssuesHistoryAsync(string searchKeyWord, Guid currentNegotiationID)
        {
            PerformQuery<IssueHistory>(Context.GetIssuesHistoryQuery(searchKeyWord, currentNegotiationID),
                GetIssuesHistoryComplete);
        }

        #endregion
    }
}
