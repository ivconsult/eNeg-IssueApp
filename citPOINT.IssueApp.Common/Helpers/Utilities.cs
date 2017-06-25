#region → Usings   .
using System;
using System.Windows;
using System.Collections.Generic;
using System.ComponentModel;
using citPOINT.IssueApp.Data.Web;
using citPOINT.eNeg.Apps.Common.Interfaces;
#endregion

#region → History  .

/* Date         User              Change
 * 
 * 17.08.11     Yousra Reda       Creation
 * 17.08.11     Yousra Reda       Save current Login User
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
    /// Issue App Configurations.
    /// </summary>
    public class IssueAppConfigurations
    {
        #region → Properties     .

        #region Static

        /// <summary>
        /// Gets the name of the app.
        /// </summary>
        /// <value>The name of the app.</value>
        public static string AppName { get { return "Issue App"; } }

        /// <summary>
        /// Gets or sets the negotiatio parameter.
        /// </summary>
        /// <value>The negotiatio parameter.</value>
        public static INegotiation NegotiationParameter { get; set; }

        /// <summary>
        /// Gets or sets the main platform info.
        /// </summary>
        /// <value>The main platform info.</value>
        public static IMainPlatformInfo MainPlatformInfo { get; set; }

        /// <summary>
        /// Gets the main service URI.
        /// </summary>
        /// <value>The main service URI.</value>
        public static Uri MainServiceUri
        {
            get
            {
                if (IssueAppConfigurations.MainPlatformInfo != null)
                {

                    var app = IssueAppConfigurations
                                    .MainPlatformInfo
                                    .GetApplicationInfo(IssueAppConfigurations.AppName);

                    if (app != null && !string.IsNullOrEmpty(app.ApplicationMainServicePath))
                    {
                        return new Uri(app.ApplicationMainServicePath, UriKind.Absolute);
                    }
                }

                return new Uri(string.Empty, UriKind.Absolute);
            }
        }
        #endregion

        #endregion
    }
}

