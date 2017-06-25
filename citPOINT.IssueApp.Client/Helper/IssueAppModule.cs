#region → Usings   .

using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using citPOINT.eNeg.Apps.Common.Interfaces;
using citPOINT.IssueApp.Common;
using citPOINT.IssueApp.Model;
using citPOINT.IssueApp.ViewModel;
using Microsoft.Practices.Prism.MefExtensions.Modularity;
using Microsoft.Practices.Prism.Modularity;
using Microsoft.Practices.Prism.Regions;

#endregion

#region → History  .

/* Date         User              Change
 * 
 * 21.03.12     M.Wahab       Creation
 */

# endregion History

#region → ToDos    .
/*
 * Date         set by User     Description
 * 
 * 
*/
# endregion ToDos

namespace citPOINT.IssueApp.Client
{
    /// <summary>
    /// Preference App Module.
    /// </summary>
    [ModuleExport(typeof(IssueAppModule))]
    public class IssueAppModule : IModule
    {
        #region → Fields         .

        private readonly IRegionManager regionManager;

        #endregion

        #region → Properties     .

        /// <summary>
        /// Gets or sets the container.
        /// </summary>
        /// <value>The container.</value>
        public static CompositionContainer Container { get; set; }

        #endregion

        #region → Constructor    .

        /// <summary>
        /// Initializes a new instance of the <see cref="IssueAppModule"/> class.
        /// </summary>
        /// <param name="regionManager">The region manager.</param>
        /// <param name="MainPlatformInfo">The main platform info.</param>
        [ImportingConstructor()]
        public IssueAppModule(IRegionManager regionManager, IMainPlatformInfo MainPlatformInfo)
        {
            this.regionManager = regionManager;

            IssueAppConfigurations.MainPlatformInfo = MainPlatformInfo;

            //IssueAppConfigurations.ActionTypeParameter = IssueAppConfigurations.ActionTypes.Report.ToString();

            this.IntializeContainer();
        }

        #endregion

        #region → Methods        .

        #region → Private        .

        /// <summary>
        /// Intializes the container.
        /// </summary>
        private void IntializeContainer()
        {
            //An aggregate catalog that combines multiple catalogs
            var catalog = new AggregateCatalog();

            //Adds all the parts found in the same assembly as the Program class
            catalog.Catalogs.Add(new AssemblyCatalog(typeof(App).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IssueAppConfigurations).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IssueHistoryViewModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IssueHistoryModel).Assembly));

            catalog.Catalogs.Add(new AssemblyCatalog(typeof(IIssueHistoryModel).Assembly));

            //catalog.Catalogs.Add(new AssemblyCatalog(typeof(PreferenceSetNeg).Assembly));

            //Create the CompositionContainer with the parts in the catalog
            Container = new CompositionContainer(catalog);
        }

        #endregion

        #region → Public         .

        /// <summary>
        /// Notifies the module that it has be initialized.
        /// </summary>
        public void Initialize()
        {
            try
            {
                regionManager.RegisterViewWithRegion
                    (IssueAppConfigurations.AppName.Replace(" ", "") + "Region",
                     typeof(MainPageView));
            }
            catch (System.Exception ex)
            {
                IssueAppConfigurations.MainPlatformInfo.HandleException.HandleException(ex, IssueAppConfigurations.AppName);
            }

        }

        #endregion

        #endregion

    }
}
