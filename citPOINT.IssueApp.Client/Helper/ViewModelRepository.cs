
#region → Usings   .
using System.ComponentModel.Composition;
using GalaSoft.MvvmLight;
using citPOINT.IssueApp.ViewModel;
#endregion

#region → History  .

/* Date         User          Change
 * 
 * 23.04.12    M.Wahab         Creation
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
    /// View Model Repository.
    /// Shared View Models forcing that all view models are intialized.
    /// </summary>
    public class ViewModelRepository : ICleanup
    {
        #region → Properties     .

        /// <summary>
        /// Gets or sets the preference sets view model.
        /// </summary>
        /// <value>The preference sets view model.</value>
        [Import(IssueApp.Common.IssueAppViewModelTypes.IssueHistoryViewModel)]
        public IssueHistoryViewModel IssueHistoryViewModel { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewModelRepository"/> class.
        /// </summary>
        [ImportingConstructor]
        public ViewModelRepository()
        {
            if (!GalaSoft.MvvmLight.ViewModelBase.IsInDesignModeStatic)
            {
                IssueAppModule.Container.SatisfyImportsOnce(this);

            }
        }

        #endregion

        #region → Methods        .

        /// <summary>
        /// Cleanups this instance.
        /// </summary>
        public void Cleanup()
        {
            this.IssueHistoryViewModel.Cleanup();

            //Repository.Cleanup();
        }

        #endregion
    }
}
