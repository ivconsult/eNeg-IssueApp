
#region → Usings   .

using System;
using System.Collections.Generic;
using System.ComponentModel;
using citPOINT.eNeg.Common;
using citPOINT.IssueApp.Common;
using citPOINT.IssueApp.Data.Web;

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

namespace citPOINT.eNeg.MVVM.UnitTest
{
    /// <summary>
    /// Mock Issue History Model.
    /// </summary>
    class MockIssueHistoryModel : IIssueHistoryModel
    {
        #region → Fields         .

        private List<IssueHistory> mIssueHistorySource;

        #endregion

        #region → Properties     .

        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        /// <value></value>
        public bool IsBusy
        {
            get { return false; }
        }

        /// <summary>
        /// Gets the issue history source.
        /// </summary>
        /// <value>The issue history source.</value>
        public List<IssueHistory> IssueHistorySource
        {
            get
            {
                if (mIssueHistorySource == null)
                {
                    mIssueHistorySource = new List<IssueHistory>()
                       {
                           new IssueHistory ()
                            { 
                                 IssueName="Price",
                                 TimesUsed=281,
                                 AverageScore= 25,
                                 Rank=1
                            },
                            new IssueHistory ()
                            { 
                                 IssueName="Model",
                                 TimesUsed=160,
                                 AverageScore= 35,
                                 Rank=2
                            },
                            new IssueHistory ()
                            { 
                                 IssueName="Type",
                                 TimesUsed=121,
                                 AverageScore= 67,
                                 Rank=3
                            },
                            new IssueHistory ()
                            { 
                                 IssueName="Car", // Search Key Word
                                 TimesUsed=240,   // Negotiation Count
                                 AverageScore= 25,// Counter Part Count
                                 Rank=0           // Flag for Counts   
                            }
                       };
                }

                return mIssueHistorySource;
            }
        }

        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get issue history completed].
        /// </summary>
        public event EventHandler<eNegEntityResultArgs<IssueHistory>> GetIssuesHistoryComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region → Methods        .

        /// <summary>
        /// Get issue history using keyword async.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID">The current negotiation ID.</param>
        public void GetIssuesHistoryAsync(string searchKeyWord, Guid currentNegotiationID)
        {
            if (GetIssuesHistoryComplete != null)
            {
                GetIssuesHistoryComplete(this, new eNegEntityResultArgs<IssueHistory>(IssueHistorySource));
            }
        }

      
        #endregion


    }
}
