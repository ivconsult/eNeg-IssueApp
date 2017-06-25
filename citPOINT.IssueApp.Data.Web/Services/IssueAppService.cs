

#region → Usings   .
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Channels;
using System.ServiceModel.DomainServices.Hosting;
using System.ServiceModel.DomainServices.Server;
using citPOINT.IssueApp.Data.Web.PrefAppService;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 04.01.12     M.Wahab           • Creation
 */

# endregion

#region → ToDos    .

/*
 * Date         set by User     Description
 * 
 * 
*/

# endregion

namespace citPOINT.IssueApp.Data.Web
{ 
    // TODO: Create methods containing your application logic.
    /// <summary>
    /// Issue App Service.
    /// </summary>
    [EnableClientAccess()]
    public class IssueAppService : DomainService
    {

        #region → Fields         .

        PrefAppServiceSoapClient mLoader;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets the loader.
        /// </summary>
        /// <value>The loader.</value>
        public PrefAppServiceSoapClient Loader
        {
            get
            {
                if (mLoader == null)
                {
                    mLoader = new PrefAppServiceSoapClient();
                    InjectCredentials();
                }
                return mLoader;
            }
        }

        #endregion Properties
        
        #region → Methods        .

        #region → Public         .

        /// <summary>
        /// Gets the issues history.
        /// </summary>
        /// <param name="searchKeyWord">The search key word.</param>
        /// <param name="currentNegotiationID">The current negotiation ID.</param>
        /// <returns></returns>
        public IQueryable<IssueHistory> GetIssuesHistory(string searchKeyWord,Guid currentNegotiationID)
        {
            if (ServiceAuthentication.IsValid())
            {
                var lstResult = this.Loader.GetIssuesHistory(searchKeyWord, currentNegotiationID).RootResults;
                return GetReturnSet(lstResult).AsQueryable<IssueHistory>();
            }
            else
            {
                // throw fault exception to indicate the caller that the service need valid credentials                      
                throw new FaultException<InvalidOperationException>(new InvalidOperationException("Invalid credentials"), "Invalid credentials");
            }
        } 
        #endregion
        
        #region → Private        .

        /// <summary>
        /// Injects the credentials into message header.
        /// </summary>
        private void InjectCredentials()
        {
            OperationContextScope scope = new OperationContextScope((IContextChannel)Loader.InnerChannel);

            MessageHeaders messageHeadersElement = OperationContext.Current.OutgoingMessageHeaders;
            messageHeadersElement.Add(MessageHeader.CreateHeader("username", "http://tempori.org", ConfigurationManager.AppSettings["username"]));
            messageHeadersElement.Add(MessageHeader.CreateHeader("password", "http://tempori.org", ConfigurationManager.AppSettings["password"]));
        }

        /// <summary>
        /// Gets the return set.
        /// </summary>
        /// <param name="lstResult">The LST result.</param>
        /// <returns></returns>
        private static List<IssueHistory> GetReturnSet(IssueHistoryResult[] lstResult)
        {
            List<IssueHistory> lsReturnResult = new List<IssueHistory>();

            foreach (var result in lstResult)
            {
                lsReturnResult.Add(new IssueHistory()
                {
                    Rank = (int)result.Rank,
                    AverageScore = result.AverageScore.Value,
                    IssueName = result.IssueName,
                    TimesUsed = result.TimesUsed.Value,

                });
            }
            return lsReturnResult;
        }

        #endregion

        #endregion
    }
}


