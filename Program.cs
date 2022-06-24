using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using HPIMS_Console_API.Email;
namespace HPIMS_Console_API
{
    internal class Program
    {
        static ConnectionQuery CQObject = new ConnectionQuery();
        static DatabaseCode DBObject = new DatabaseCode();
        static void Main(string[] args)
        {
            ServiceReference.ServiceClient serviceObject = new ServiceReference.ServiceClient();
            List<ServerModel> list_index = DBObject.ServerList();
            Console.WriteLine("---------------------------- SERVER STATUS ----------------------------\n");
            for (int i = 0; i < list_index.Count; i++)
            {
                string[] driveStatus;
                string statusOfServer, statusOfUser;

                // Fetching Server Status 
                Console.WriteLine("| REMOTE SERVER ADDRESS : {0} |", list_index[i].ServerName);
                statusOfServer = serviceObject.StatusOfServer(list_index[i].ServerName);
                if (statusOfServer == "True")
                {
                    Console.WriteLine("| CONNECTION SUCCESSFUL. SERVER IS ACTIVE AND RUNNING |");
                }
                else
                {
                    Console.Write("| CONNECTION FAILED. SERVER STATUS COULD NOT BE FETCHED |");
                    Console.WriteLine("| ERROR DETAIL - {0} |",statusOfServer);
                    statusOfServer = "False";
                }

                // Fetching User Detail
                statusOfUser = serviceObject.WhoIsLoggedIn(list_index[i].ServerName, "$vka001", "bot.pwd-12");
                if (statusOfUser == "True")
                {
                    Console.WriteLine("| STATUS OF USER : {0} |","ACTIVE");
                }
                else if(statusOfUser == "False")
                {
                    Console.WriteLine("| STATUS OF USER: {0} |", "INACTIVE");
                }
                else
                {
                    Console.WriteLine(statusOfUser);
                }

                // Fetching the drive status
                driveStatus = serviceObject.DriveStorage(list_index[i].ServerName, "$vka001", "bot.pwd-12");
                if(driveStatus[0] == "NA" && driveStatus[1] == "NA" && driveStatus[2] == "NA")
                {
                    Console.WriteLine("| ERROR IN FETCHING DRIVE USAGE |");
                }
                else if(driveStatus[0] == "NA" || driveStatus[1] == "NA" || driveStatus[2] == "NA")
                {
                    Console.WriteLine("| DRIVE DATA PARTIALLY FETCHED |");
                }
                else
                {
                    Console.WriteLine("| DRIVE DATA FETCHED SUCCESSFULLY |");
                    Console.WriteLine("| C DRIVE : {0}, E DRIVE : {1}, F DRIVE : {2} |",driveStatus[0],driveStatus[1],driveStatus[2]);
                }

                // Updating the database
                bool dataQueryResult = DBObject.DataUpdate(statusOfServer, statusOfUser, driveStatus[0], driveStatus[1], driveStatus[2], list_index[i].Id);

                if (dataQueryResult)
                {
                    Console.WriteLine("| DATABASE UPDATE FOR ROW : {0} |", list_index[i].Id);
                }
                else
                {
                    Console.WriteLine("| DATABASE UPDATE FAILED FOR ROW : {0} |", list_index[i].Id);
                }

                Console.WriteLine("-----------------------------------------------------------------------");
                Console.WriteLine();
            }

            Console.WriteLine("---------------------------- MAIL SERVICE -----------------------------\n");
            AutomatedEmailService emailService = new AutomatedEmailService();
            emailService.Command();
            Console.WriteLine("PROCESS ENDED. ENTER TO PROCEED");
            Console.WriteLine("\n-----------------------------------------------------------------------");
            Console.ReadKey();
        }
    }
}
