
#region → Usings   .
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.Objects.DataClasses;
using System.Runtime.Serialization;
using System.ComponentModel.DataAnnotations;
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
    /// <summary>
    /// Class reprsents the result from selecting Issues Statisticals
    /// </summary>
    /// 
    [Serializable()]
    [DataContractAttribute(IsReference = true)]
    public partial class IssueHistory : EntityObject
    {

        #region → Properties     .

        /// <summary>
        /// Gets or sets the rank.
        /// </summary>
        /// <value>The rank.</value>
        [DataMemberAttribute()]
        [Key]
        public int Rank { get; set; }

        /// <summary>
        /// Gets or sets the name of the issue.
        /// </summary>
        /// <value>The name of the issue.</value>
        [DataMemberAttribute()]
        public string IssueName { get; set; }

        /// <summary>
        /// Gets or sets the times used.
        /// </summary>
        /// <value>The times used.</value>
        [DataMemberAttribute()]
        public int TimesUsed { get; set; }

        /// </summary>
        /// Gets or sets the average saverage score       
        /// </summary>
        /// <value>The average saverage score
        [DataMemberAttribute()]
        public decimal AverageScore { get; set; }

        #endregion

    }
}
