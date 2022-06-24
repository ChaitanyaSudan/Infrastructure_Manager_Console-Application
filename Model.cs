using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace HPIMS_Console_API
{
    internal class ServerModel
    {
        public long Id { get; set; }
        public string ServerName { get; set; }
    }

    internal class ServerUpdateModel
    {
        public long ID { get; set; }
        public long ServerId { get; set; }
        public string ServerName { get; set; }
        public bool IsServerUp { get; set; }
    }
    internal class NotificationMappingModel
    {
        public long Id { get; set; }
        public long ServerId { get; set; }
        public long ProjectId { get; set; }
        public long NotificationId { get; set; }
        public string ToAddress { get; set; }
        public string CCAddress { get; set; }
        public string BCCAddress { get; set; }
        public bool IsActive { get; set; }
    }
}