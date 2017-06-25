
#region → Usings   .
using System;
using citPOINT.eNeg.Common;
using citPOINT.IssueApp.Data.Web;
using System.ComponentModel;

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

namespace citPOINT.IssueApp.Common
{

    /// <summary>
    /// IIssueHistory Model Interface for IssueHistoryModel
    /// </summary>
    public interface IIssueHistoryModel
    {
        #region → Properties     .
        /// <summary>
        /// True if either "IsLoading" or "IsSubmitting" is
        /// in progress; otherwise, false
        /// </summary>
        bool IsBusy { get; }
        #endregion

        #region → Events         .

        /// <summary>
        /// Occurs when [get issue history completed].
        /// </summary> 
        /// 
        event EventHandler<eNegEntityResultArgs<IssueHistory>> GetIssuesHistoryComplete;

        /// <summary>
        /// Occurs when [property changed].
        /// </summary> 
        /// 
        event PropertyChangedEventHandler PropertyChanged;

        #endregion
        
        #region → Methods        .

        /// <summary>
        /// Get issue history using keyword async.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID">The current negotiation ID.</param>
        void GetIssuesHistoryAsync(string searchKeyWord, Guid currentNegotiationID);

       #endregion
    }
}
