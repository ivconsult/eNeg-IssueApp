//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.1
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace citPOINT.IssueApp.Data.Web
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;
    using System.Linq;
    using System.Runtime.Serialization;
    using System.ServiceModel;
    using System.ServiceModel.DomainServices;
    using System.ServiceModel.DomainServices.Client;
    using System.ServiceModel.DomainServices.Client.ApplicationServices;
    using System.ServiceModel.Web;
    
    
    /// <summary>
    /// The domain context corresponding to the 'IssueAppService' domain service.
    /// </summary>
    public sealed partial class IssueAppContext : DomainContext
    {
        
        #region Extensibility Method Definitions

        /// <summary>
        /// This method is invoked from the constructor once initialization is complete and
        /// can be used for further object setup.
        /// </summary>
        partial void OnCreated();

        #endregion
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueAppContext"/> class.
        /// </summary>
        public IssueAppContext() : 
                this(new WebDomainClient<IIssueAppServiceContract>(new Uri("citPOINT-IssueApp-Data-Web-IssueAppService.svc", UriKind.Relative)))
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueAppContext"/> class with the specified service URI.
        /// </summary>
        /// <param name="serviceUri">The IssueAppService service URI.</param>
        public IssueAppContext(Uri serviceUri) : 
                this(new WebDomainClient<IIssueAppServiceContract>(serviceUri))
        {
        }
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueAppContext"/> class with the specified <paramref name="domainClient"/>.
        /// </summary>
        /// <param name="domainClient">The DomainClient instance to use for this domain context.</param>
        public IssueAppContext(DomainClient domainClient) : 
                base(domainClient)
        {
            this.OnCreated();
        }
        
        /// <summary>
        /// Gets the set of <see cref="IssueHistory"/> entities that have been loaded into this <see cref="IssueAppContext"/> instance.
        /// </summary>
        public EntitySet<IssueHistory> IssueHistories
        {
            get
            {
                return base.EntityContainer.GetEntitySet<IssueHistory>();
            }
        }
        
        /// <summary>
        /// Gets an EntityQuery instance that can be used to load <see cref="IssueHistory"/> entities using the 'GetIssuesHistory' query.
        /// </summary>
        /// <param name="searchKeyWord">The value for the 'searchKeyWord' parameter of the query.</param>
        /// <param name="currentNegotiationID">The value for the 'currentNegotiationID' parameter of the query.</param>
        /// <returns>An EntityQuery that can be loaded to retrieve <see cref="IssueHistory"/> entities.</returns>
        public EntityQuery<IssueHistory> GetIssuesHistoryQuery(string searchKeyWord, Guid currentNegotiationID)
        {
            Dictionary<string, object> parameters = new Dictionary<string, object>();
            parameters.Add("searchKeyWord", searchKeyWord);
            parameters.Add("currentNegotiationID", currentNegotiationID);
            this.ValidateMethod("GetIssuesHistoryQuery", parameters);
            return base.CreateQuery<IssueHistory>("GetIssuesHistory", parameters, false, true);
        }
        
        /// <summary>
        /// Creates a new entity container for this domain context's entity sets.
        /// </summary>
        /// <returns>A new container instance.</returns>
        protected override EntityContainer CreateEntityContainer()
        {
            return new IssueAppContextEntityContainer();
        }
        
        /// <summary>
        /// Service contract for the 'IssueAppService' domain service.
        /// </summary>
        [ServiceContract()]
        public interface IIssueAppServiceContract
        {
            
            /// <summary>
            /// Asynchronously invokes the 'GetIssuesHistory' operation.
            /// </summary>
            /// <param name="searchKeyWord">The value for the 'searchKeyWord' parameter of this action.</param>
            /// <param name="currentNegotiationID">The value for the 'currentNegotiationID' parameter of this action.</param>
            /// <param name="callback">Callback to invoke on completion.</param>
            /// <param name="asyncState">Optional state object.</param>
            /// <returns>An IAsyncResult that can be used to monitor the request.</returns>
            [FaultContract(typeof(DomainServiceFault), Action="http://tempuri.org/IssueAppService/GetIssuesHistoryDomainServiceFault", Name="DomainServiceFault", Namespace="DomainServices")]
            [OperationContract(AsyncPattern=true, Action="http://tempuri.org/IssueAppService/GetIssuesHistory", ReplyAction="http://tempuri.org/IssueAppService/GetIssuesHistoryResponse")]
            [WebGet()]
            IAsyncResult BeginGetIssuesHistory(string searchKeyWord, Guid currentNegotiationID, AsyncCallback callback, object asyncState);
            
            /// <summary>
            /// Completes the asynchronous operation begun by 'BeginGetIssuesHistory'.
            /// </summary>
            /// <param name="result">The IAsyncResult returned from 'BeginGetIssuesHistory'.</param>
            /// <returns>The 'QueryResult' returned from the 'GetIssuesHistory' operation.</returns>
            QueryResult<IssueHistory> EndGetIssuesHistory(IAsyncResult result);
            
            /// <summary>
            /// Asynchronously invokes the 'SubmitChanges' operation.
            /// </summary>
            /// <param name="changeSet">The change-set to submit.</param>
            /// <param name="callback">Callback to invoke on completion.</param>
            /// <param name="asyncState">Optional state object.</param>
            /// <returns>An IAsyncResult that can be used to monitor the request.</returns>
            [FaultContract(typeof(DomainServiceFault), Action="http://tempuri.org/IssueAppService/SubmitChangesDomainServiceFault", Name="DomainServiceFault", Namespace="DomainServices")]
            [OperationContract(AsyncPattern=true, Action="http://tempuri.org/IssueAppService/SubmitChanges", ReplyAction="http://tempuri.org/IssueAppService/SubmitChangesResponse")]
            IAsyncResult BeginSubmitChanges(IEnumerable<ChangeSetEntry> changeSet, AsyncCallback callback, object asyncState);
            
            /// <summary>
            /// Completes the asynchronous operation begun by 'BeginSubmitChanges'.
            /// </summary>
            /// <param name="result">The IAsyncResult returned from 'BeginSubmitChanges'.</param>
            /// <returns>The collection of change-set entry elements returned from 'SubmitChanges'.</returns>
            IEnumerable<ChangeSetEntry> EndSubmitChanges(IAsyncResult result);
        }
        
        internal sealed class IssueAppContextEntityContainer : EntityContainer
        {
            
            public IssueAppContextEntityContainer()
            {
                this.CreateEntitySet<IssueHistory>(EntitySetOperations.None);
            }
        }
    }
    
    /// <summary>
    /// The 'IssueHistory' entity class.
    /// </summary>
    [DataContract(Namespace="http://schemas.datacontract.org/2004/07/citPOINT.IssueApp.Data.Web")]
    public sealed partial class IssueHistory : Entity
    {
        
        private decimal _averageScore;
        
        private string _issueName;
        
        private int _rank;
        
        private int _timesUsed;
        
        #region Extensibility Method Definitions

        /// <summary>
        /// This method is invoked from the constructor once initialization is complete and
        /// can be used for further object setup.
        /// </summary>
        partial void OnCreated();
        partial void OnAverageScoreChanging(decimal value);
        partial void OnAverageScoreChanged();
        partial void OnIssueNameChanging(string value);
        partial void OnIssueNameChanged();
        partial void OnRankChanging(int value);
        partial void OnRankChanged();
        partial void OnTimesUsedChanging(int value);
        partial void OnTimesUsedChanged();

        #endregion
        
        
        /// <summary>
        /// Initializes a new instance of the <see cref="IssueHistory"/> class.
        /// </summary>
        public IssueHistory()
        {
            this.OnCreated();
        }
        
        /// <summary>
        /// Gets or sets the 'AverageScore' value.
        /// </summary>
        [DataMember()]
        public decimal AverageScore
        {
            get
            {
                return this._averageScore;
            }
            set
            {
                if ((this._averageScore != value))
                {
                    this.OnAverageScoreChanging(value);
                    this.RaiseDataMemberChanging("AverageScore");
                    this.ValidateProperty("AverageScore", value);
                    this._averageScore = value;
                    this.RaiseDataMemberChanged("AverageScore");
                    this.OnAverageScoreChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the 'IssueName' value.
        /// </summary>
        [DataMember()]
        public string IssueName
        {
            get
            {
                return this._issueName;
            }
            set
            {
                if ((this._issueName != value))
                {
                    this.OnIssueNameChanging(value);
                    this.RaiseDataMemberChanging("IssueName");
                    this.ValidateProperty("IssueName", value);
                    this._issueName = value;
                    this.RaiseDataMemberChanged("IssueName");
                    this.OnIssueNameChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the 'Rank' value.
        /// </summary>
        [DataMember()]
        [Editable(false, AllowInitialValue=true)]
        [Key()]
        [RoundtripOriginal()]
        public int Rank
        {
            get
            {
                return this._rank;
            }
            set
            {
                if ((this._rank != value))
                {
                    this.OnRankChanging(value);
                    this.ValidateProperty("Rank", value);
                    this._rank = value;
                    this.RaisePropertyChanged("Rank");
                    this.OnRankChanged();
                }
            }
        }
        
        /// <summary>
        /// Gets or sets the 'TimesUsed' value.
        /// </summary>
        [DataMember()]
        public int TimesUsed
        {
            get
            {
                return this._timesUsed;
            }
            set
            {
                if ((this._timesUsed != value))
                {
                    this.OnTimesUsedChanging(value);
                    this.RaiseDataMemberChanging("TimesUsed");
                    this.ValidateProperty("TimesUsed", value);
                    this._timesUsed = value;
                    this.RaiseDataMemberChanged("TimesUsed");
                    this.OnTimesUsedChanged();
                }
            }
        }
        
        /// <summary>
        /// Computes a value from the key fields that uniquely identifies this entity instance.
        /// </summary>
        /// <returns>An object instance that uniquely identifies this entity instance.</returns>
        public override object GetIdentity()
        {
            return this._rank;
        }
    }
}