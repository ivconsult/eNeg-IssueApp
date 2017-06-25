#region → Usings   .
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Composition;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Windows.Controls;
using System.Windows.Input;
using citPOINT.eNeg.Common;
using citPOINT.IssueApp.Common;
using citPOINT.IssueApp.Data.Web;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.01.12     M.Wahab         • creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.IssueApp.ViewModel
{
    #region → Using  MEF to export Issue History View Model
    /// <summary>
    /// this class is to Message Template View Model.
    /// </summary>
    [Export(IssueAppViewModelTypes.IssueHistoryViewModel)]
    [PartCreationPolicy(CreationPolicy.NonShared)]
    #endregion
    public class IssueHistoryViewModel : ViewModelBase
    {
        #region → Fields         .

        private IIssueHistoryModel mIssueHistoryModel;

        private List<IssueHistory> mIssueHistorySource;
        [Required]
        private string mCurrentKeyWord;
        private int mMatchedNegotiationCount;
        private int mCounterpartCount;
        private bool mIsBusy;
        private bool mHasCounterpart=false;

        private RelayCommand mSearchCommand;
        private RelayCommand<KeyEventArgs> mSearchByEnterKeyDownCommand;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the issue history source.
        /// </summary>
        /// <value>The issue history source.</value>
        public List<IssueHistory> IssueHistorySource
        {
            get
            {
                return mIssueHistorySource;
            }
            set
            {
                mIssueHistorySource = value;
                this.RaisePropertyChanged("IssueHistorySource");
            }
        }

        /// <summary>
        /// Gets or sets the current key word.
        /// </summary>
        /// <value>The current key word.</value>
        [Required(AllowEmptyStrings = false)]
        public string CurrentKeyWord
        {
            get
            {
                return mCurrentKeyWord;
            }
            set
            {
                mCurrentKeyWord = value;
                this.RaisePropertyChanged("CurrentKeyWord");
            }
        }

        /// <summary>
        /// Gets or sets the matched negotiation count.
        /// </summary>
        /// <value>The matched negotiation count.</value>
        public int MatchedNegotiationCount
        {
            get
            {
                return mMatchedNegotiationCount;
            }
            set
            {
                mMatchedNegotiationCount = value;
                this.RaisePropertyChanged("MatchedNegotiationCount");
            }
        }

        /// <summary>
        /// Gets or sets the counterpart count.
        /// </summary>
        /// <value>The counterpart count.</value>
        public int CounterpartCount
        {
            get
            {
                return mCounterpartCount;
            }
            set
            {
                mCounterpartCount = value;
                
                this.HasCounterpart = this.CounterpartCount > 0;

                this.RaisePropertyChanged("CounterpartCount");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance has counterpart.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance has counterpart; otherwise, <c>false</c>.
        /// </value>
        public bool HasCounterpart
        {
            get { return mHasCounterpart; }
            set
            {
                mHasCounterpart = value;
                this.RaisePropertyChanged("HasCounterpart");
            }
        }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is busy.
        /// </summary>
        /// <value><c>true</c> if this instance is busy; otherwise, <c>false</c>.</value>
        public bool IsBusy
        {
            get
            {
                return mIsBusy;
            }
            set
            {
                mIsBusy = value;
                this.RaisePropertyChanged("IsBusy");
            }
        }

        #endregion

        #region → Constructors   .
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueHistoryViewModel"/> class.
        /// </summary>
        /// <param name="IssueHistoryModel">The Issue History Model.</param>
        [ImportingConstructor]
        public IssueHistoryViewModel(IIssueHistoryModel IssueHistoryModel)
        {
            this.mIssueHistoryModel = IssueHistoryModel;

            #region → Set up event handling       .

            this.mIssueHistoryModel.GetIssuesHistoryComplete += new EventHandler<eNegEntityResultArgs<IssueHistory>>(mIssueHistoryModel_GetIssuesHistoryComplete);
            this.mIssueHistoryModel.PropertyChanged += new System.ComponentModel.PropertyChangedEventHandler(mIssueHistoryModel_PropertyChanged);

            #endregion

            this.ApplyChanges();
        }

        #endregion

        #region → Event Handlers .

        /// <summary>
        /// Call back of issues history.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The e.</param>
        private void mIssueHistoryModel_GetIssuesHistoryComplete(object sender, eNegEntityResultArgs<IssueHistory> e)
        {
            if (!e.HasError)
            {
                this.IssueHistorySource = e.Results
                                           .Where(s => s.Rank != 0)
                                           .ToList();

                IssueHistory issueHistory = e.Results
                                             .Where(s => s.Rank == 0)
                                             .FirstOrDefault();

                if (issueHistory != null)
                {
                    this.MatchedNegotiationCount = issueHistory.TimesUsed;
                    this.CounterpartCount = (int)issueHistory.AverageScore;
                    this.CurrentKeyWord = issueHistory.IssueName;
                }
                else
                {
                    this.MatchedNegotiationCount = 0;
                    this.CounterpartCount = 0;
                }
            }
            else
            {
                IssueAppMessanger.RaiseErrorMessage.Send(e.Error);
            }

            IssueAppMessanger.ChangeScreenMessage.Send(IssueAppViewTypes.MainView);
        }

        /// <summary>
        /// Handles the PropertyChanged event of the mIssueHistoryModel control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.ComponentModel.PropertyChangedEventArgs"/> instance containing the event data.</param>
        private void mIssueHistoryModel_PropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            if (e.PropertyName.Equals("HasChanges") || e.PropertyName.Equals("IsBusy"))
            {
                this.IsBusy = this.mIssueHistoryModel.IsBusy;
            }
        }

        #endregion

        #region → Commands       .

        /// <summary>
        /// Gets the search by enter key down command.
        /// </summary>
        /// <value>The search by enter key down command.</value>
        public RelayCommand<KeyEventArgs> SearchByEnterKeyDownCommand
        {
            get
            {
                if (mSearchByEnterKeyDownCommand == null)
                {
                    mSearchByEnterKeyDownCommand = new RelayCommand<KeyEventArgs>((args) =>
                    {
                        try
                        {
                            if (args.Key == Key.Enter)
                            {

                                if (args.OriginalSource != null &&
                                    args.OriginalSource is TextBox)
                                {
                                    this.CurrentKeyWord = (args.OriginalSource as TextBox).Text;
                                }
                                SearchCommand.Execute(null);
                            }
                        }
                        catch (Exception ex)
                        {
                            IssueAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    });
                }
                return mSearchByEnterKeyDownCommand;
            }
        }

        /// <summary>
        /// Gets the search command.
        /// </summary>
        /// <value>The search command.</value>
        public RelayCommand SearchCommand
        {
            get
            {
                if (mSearchCommand == null)
                {
                    mSearchCommand = new RelayCommand(() =>
                    {
                        try
                        {
                            if (!string.IsNullOrEmpty(this.CurrentKeyWord))
                            {
                                this.GetIssuesHistoryAsync(this.CurrentKeyWord, IssueAppConfigurations.NegotiationParameter.NegotiationID);
                            }
                        }
                        catch (Exception ex)
                        {
                            // notify user if there is any error
                            IssueAppMessanger.RaiseErrorMessage.Send(ex);
                        }
                    }
                    , () => true);
                }
                return mSearchCommand;
            }
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        #endregion

        #region → Public         .

        /// <summary>
        /// Gets the issues history async.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID">The current negotiation ID.</param>
        public void GetIssuesHistoryAsync(string searchKeyWord, Guid currentNegotiationID)
        {
            mIssueHistoryModel.GetIssuesHistoryAsync(searchKeyWord, currentNegotiationID);
        }

        /// <summary>
        /// Applies the changes.
        /// </summary>
        public void ApplyChanges()
        {
            if (IssueAppConfigurations.NegotiationParameter != null)
            {
                if (this.CurrentKeyWord != IssueAppConfigurations.NegotiationParameter.NegotiationName)
                {
                    this.CurrentKeyWord = IssueAppConfigurations.NegotiationParameter.NegotiationName;
                    this.GetIssuesHistoryAsync(this.CurrentKeyWord, IssueAppConfigurations.NegotiationParameter.NegotiationID);
                }
                else
                {
                    IssueAppMessanger.ChangeScreenMessage.Send(IssueAppViewTypes.MainView);
                }
            }
        }

        #endregion

        #endregion


    }
}