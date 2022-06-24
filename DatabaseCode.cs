using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HPIMS_Console_API
{
    internal class DatabaseCode
    {
        static ConnectionQuery CQObject = new ConnectionQuery();
        public SqlConnection connection = new SqlConnection(CQObject.GetConnectionString());

        public List<ServerModel> ServerList()
        {
            List<ServerModel> list_index = new List<ServerModel>();
            using (SqlConnection connection = new SqlConnection(CQObject.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(CQObject.GetListQuery(), connection))
                {
                    DataTable datatable = new DataTable();
                    SqlDataAdapter adapter = new SqlDataAdapter(command);
                    adapter.Fill(datatable);
                    foreach (DataRow datarow in datatable.Rows)
                    {
                        list_index.Add(new ServerModel
                        {
                            Id = Convert.ToInt32(datarow[0]),
                            ServerName = datarow[1] != DBNull.Value ? Convert.ToString(datarow[1]) : ""
                        });
                    }
                }
                connection.Close();
            }
            return list_index;
        }
        public List<ServerUpdateModel> ServerUpdateList()
        {
            List<ServerUpdateModel> list_index = new List<ServerUpdateModel>();
            string query = CQObject.ServerUpdateQuery();
            SqlCommand command = new SqlCommand(query, connection);
            DataTable datatable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(datatable);
            foreach (DataRow datarow in datatable.Rows)
            {
                list_index.Add(new ServerUpdateModel
                {
                    ID = datarow[0] != DBNull.Value ? Convert.ToInt32(datarow[0]) : 0,
                    ServerId = datarow[1] != DBNull.Value ? Convert.ToInt32(datarow[1]) : 0,
                    ServerName = datarow[2] != DBNull.Value ? Convert.ToString(datarow[2]) : "",
                    IsServerUp = datarow[3] != DBNull.Value ? Convert.ToBoolean(datarow[3]) : false
                });
            }
            return list_index;
        }
        public bool ServerUpdateDelete(long Id)
        {
            int i;
            string query = "DELETE FROM [InfrastructureManager].[dbo].[ServerUpdates] WHERE [dbo].[ServerUpdates].ID =" + Convert.ToString(Id);
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            i = command.ExecuteNonQuery();
            connection.Close();
            if (i >= 1)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public void ServerUpdatesReset()
        {
            string query = "DBCC CHECKIDENT ('ServerUpdates', RESEED, 0)";
            SqlCommand command = new SqlCommand(query, connection);
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
        }
        public List<NotificationMappingModel> NotificationMappingList()
        {
            List<NotificationMappingModel> list_index = new List<NotificationMappingModel>();
            string query = CQObject.NotificationMappingQuery();
            SqlCommand command = new SqlCommand(query, connection);
            DataTable datatable = new DataTable();
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            adapter.Fill(datatable);
            foreach (DataRow datarow in datatable.Rows)
            {
                list_index.Add(new NotificationMappingModel
                {
                    Id = Convert.ToInt32(datarow[0]),
                    ServerId = datarow[1] != DBNull.Value ? Convert.ToInt32(datarow[1]) : Convert.ToInt32(null),
                    ProjectId = datarow[2] != DBNull.Value ? Convert.ToInt32(datarow[2]) : Convert.ToInt32(null),
                    NotificationId = datarow[3] != DBNull.Value ? Convert.ToInt32(datarow[3]) : Convert.ToInt32(null),
                    ToAddress = datarow[4] != DBNull.Value ? Convert.ToString(datarow[4]) : "",
                    CCAddress = datarow[5] != DBNull.Value ? Convert.ToString(datarow[5]) : "",
                    BCCAddress = datarow[6] != DBNull.Value ? Convert.ToString(datarow[6]) : "",
                    IsActive = datarow[7] != DBNull.Value ? Convert.ToBoolean(datarow[7]) : false
                });
            }
            return list_index;
        }
        public bool DataUpdate(string ServerStatus, string UserStatus, string CDrive, string EDrive, string FDrive, long Id)
        {
            using (SqlConnection connection = new SqlConnection(CQObject.GetConnectionString()))
            {
                connection.Open();
                using (SqlCommand command = new SqlCommand(CQObject.DataUpdateQuery(), connection))
                {
                    int i;
                    command.Parameters.AddWithValue("@Id", Id);
                    command.Parameters.AddWithValue("@IsServerUp", ServerStatus);
                    command.Parameters.AddWithValue("@IsUserLoggedIn", UserStatus);
                    command.Parameters.AddWithValue("@CDrive", CDrive);
                    command.Parameters.AddWithValue("@EDrive", EDrive);
                    command.Parameters.AddWithValue("@FDrive", FDrive);
                    i = command.ExecuteNonQuery();
                    connection.Close();
                    if (i >= 1)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
            }
        }
    }
}
