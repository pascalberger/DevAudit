﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

namespace DevAudit.AuditLibrary
{
    public class MVC5CodeProject : NetFxCodeProject
    {
        public MVC5CodeProject(Dictionary<string, object> project_options, EventHandler<EnvironmentEventArgs> message_handler) : 
            base(project_options, new Dictionary<string, string[]> { { "AppConfig", new string[] {"@", "Web.config" } } }, message_handler)
        {
           
        }

        #region Overriden methods
        protected override Application GetApplication()
        {
            Dictionary<string, object> application_options = new Dictionary<string, object>()
            {
                { "RootDirectory", this.ProjectDirectory.FullName },
                { "AppConfig", this.AppConfigurationFile.FullName }

            };

            try
            {
                this.Application = new MVC5Application(application_options, message_handler, this.PackageSource as NuGetPackageSource);
                this.ApplicationInitialised = true;
            }
            catch (Exception e)
            {
                this.AuditEnvironment.Error(e, "Error attempting to create application audit target");
                this.ApplicationInitialised = false;
                this.Application = null;
            }
            return this.Application;
        }
        #endregion

        #region Methods

        #endregion
    }
}
