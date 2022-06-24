using System;

namespace HPIMS_Console_API
{
    internal class ConnectionQuery
    {
        public string GetConnectionString()
        {
            return "Data Source=HP-5CG1483NYQ\\SQLEXPRESS; Database = InfrastructureManager; Integrated Security = true";
        }
        public string GetListQuery()
        {
            return "SELECT [Server].[Id],[Server].[ServerName] FROM [InfrastructureManager].[dbo].[Server]";
        }
        public string ServerUpdateQuery()
        {
            return "SELECT [ID],[ServerId],[ServerName],[IsServerUp] FROM [InfrastructureManager].[dbo].[ServerUpdates]";
        }
        public string NotificationMappingQuery()
        {
            return "SELECT [Id],[ServerId],[ProjectId],[NotificationId],[ToAddress],[CCAddress],[BCCAddress],[IsActive] FROM [InfrastructureManager].[dbo].[NotificationMapping]";
        }
        public string DataUpdateQuery()
        {
            return "UPDATE [InfrastructureManager].[dbo].[Server] SET [Server].[IsServerUp] = @IsServerUp, [Server].[IsUserLoggedIn] = @IsUserLoggedIn, [Server].[DriveCUsage] = @CDrive, [Server].[DriveEUsage] = @EDrive, [Server].[DriveFUsage] = @FDrive WHERE [Server].[Id] = @Id";
        }
    }
}
