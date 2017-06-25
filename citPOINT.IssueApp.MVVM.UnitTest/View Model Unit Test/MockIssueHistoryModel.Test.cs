

#region → Usings   .
using System;
using System.Linq;
using System.Windows;
using citPOINT.eNeg.Common;

using GalaSoft.MvvmLight.Messaging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using citPOINT.IssueApp.ViewModel;
using citPOINT.IssueApp.Data.Web;
using citPOINT.IssueApp.Common;
#endregion

#region → History  .

/* Date         User            Change
 * 
 * 03.01.12    M.Wahab     creation & Test All methods
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
    /// Published Profile Details View Model Test
    /// </summary>
    [TestClass]
    public class IssueHistoryViewModel_Test
    {
        #region → Fields         .

        private IssueHistoryViewModel IssueHistoryvm;
        private string ErrorMessage;
        private string mScreenName = "";

        #endregion Fields

        #region → Properties     .

        /// <summary>
        /// View Model Object
        /// </summary>
        public IssueHistoryViewModel TheVM
        {
            get { return IssueHistoryvm; }
            set { IssueHistoryvm = value; }
        }

        #endregion Properties

        #region → Constructors   .

        /// <summary>
        /// First Method Like Constructor
        /// </summary>
        [TestInitialize]
        public void BuildUp()
        {
            TheVM = new IssueHistoryViewModel(new MockIssueHistoryModel());

            #region " Registeration for needed messages "

            // register for RaiseErrorMessage
            IssueAppMessanger.RaiseErrorMessage.Register(this, OnRaiseErrorMessage);


            #endregion

        }

        #endregion Constructor

        #region → Commands       .
        
        /// <summary>
        /// Searches the command by exist key word return A collection.
        /// </summary>
        [TestMethod]
        public void SearchCommand_ByExistKeyWord_ReturnACollection()
        {
            #region → Arrange .
            this.TheVM.CurrentKeyWord = "car";
            #endregion

            #region → Act     .
            TheVM.SearchCommand.Execute(null);
            #endregion

            #region → Assert  .

            Assert.IsTrue(string.IsNullOrEmpty(ErrorMessage), string.Concat("Error Message was recieved: ", ErrorMessage));

            Assert.IsTrue(TheVM.IssueHistorySource.Count > 0, "Failed to Get Issues By existing Key Word");

            Assert.IsTrue(TheVM.CounterpartCount > 0, "Failed to Get Issues By existing Key Word");

            Assert.IsTrue(TheVM.MatchedNegotiationCount > 0, "Failed to Get Issues By existing Key Word");

            #endregion


        }

        #endregion Command

        #region → Methods        .

        #region → Private        .

        #region → Raise Error Message  .

        /// <summary>
        /// Raise error message if there is any layer send RaiseErrorMessage
        /// </summary>
        /// <param name="ex">exception to raise</param>
        private void OnRaiseErrorMessage(Exception ex)
        {
            if (ex != null)
            {

                if (ex.InnerException != null)
                {
                    ErrorMessage = ex.Message + "\r\n" + ex.InnerException.Message;

                }
                else
                    ErrorMessage = ex.Message;

                //MessageBox.Show(ErrorMessage, "Error", MessageBoxButton.OK);
            }
        }

        #endregion

        #endregion

        #region → Public         .


        /// <summary>
        /// Tests the VM existance have instance.
        /// </summary>
        [TestMethod]
        public void TestVM_Existance_HaveInstance()
        {
            Assert.IsNotNull(IssueHistoryvm, "Failed to retrieve the View Model Via MEF");
        }

        /// <summary>
        /// Used To Clean All Resources
        /// </summary>
        [TestCleanup]
        public void CleanUp()
        {
            // call Cleanup on its ViewModel
            TheVM.Cleanup();

            // Cleanup itself
            Messenger.Default.Unregister(this);
        }

        #endregion Public

        #endregion Methods
    }
}
