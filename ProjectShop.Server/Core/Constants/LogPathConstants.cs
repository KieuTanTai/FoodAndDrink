using System;
using System.IO;

namespace ProjectShop.Server.Core.Constants
{
    public static class LogPathConstants
    {
        #region Base Path
        public static readonly string BaseLogDirectory = Path.Combine(
            Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory)?.Parent?.Parent?.Parent?.Parent?.FullName
                ?? AppDomain.CurrentDomain.BaseDirectory,
            "Others",
            "Logs"
        );

        #endregion

        #region Infrastructure Layer
        public static readonly string InfrastructureLogPath = Path.Combine(BaseLogDirectory, "Infrastructure");
        public static readonly string RepositoryLogPath = Path.Combine(InfrastructureLogPath, "Repositories");
        public static readonly string DatabaseLogPath = Path.Combine(InfrastructureLogPath, "Database");
        public static readonly string ExternalServiceLogPath = Path.Combine(InfrastructureLogPath, "ExternalServices");

        #endregion

        #region Application Layer (Business Logic)
        public static readonly string ApplicationLogPath = Path.Combine(BaseLogDirectory, "Application");
        public static readonly string ServiceLogPath = Path.Combine(ApplicationLogPath, "Services");
        public static readonly string ValidationLogPath = Path.Combine(ApplicationLogPath, "Validations");
        public static readonly string WorkflowLogPath = Path.Combine(ApplicationLogPath, "Workflows");

        #endregion

        #region WebAPI Layer (API Gateway/Controllers)
        public static readonly string WebAPILogPath = Path.Combine(BaseLogDirectory, "WebAPI");

        public static readonly string ControllerLogPath = Path.Combine(WebAPILogPath, "Controllers");
        public static readonly string MiddlewareLogPath = Path.Combine(WebAPILogPath, "Middlewares");
        public static readonly string RequestResponseLogPath = Path.Combine(WebAPILogPath, "RequestResponse");

        #endregion

        #region Initialization Helper
        public static void EnsureDirectoriesExist()
        {
            // Infrastructure
            Directory.CreateDirectory(RepositoryLogPath);
            Directory.CreateDirectory(DatabaseLogPath);
            Directory.CreateDirectory(ExternalServiceLogPath);

            // Application
            Directory.CreateDirectory(ServiceLogPath);
            Directory.CreateDirectory(ValidationLogPath);
            Directory.CreateDirectory(WorkflowLogPath);

            // WebAPI
            Directory.CreateDirectory(ControllerLogPath);
            Directory.CreateDirectory(MiddlewareLogPath);
            Directory.CreateDirectory(RequestResponseLogPath);
        }
        #endregion
    }
}
