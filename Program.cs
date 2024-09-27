using OSIsoft.AF.Asset;
using OSIsoft.AF.Data;
using OSIsoft.AF.PI;
using OSIsoft.AF.Time;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using OSIsoft.AF.Diagnostics;

class Program
{
    static void Main(string[] args)
    {
        PIServer myPIServer = null;
        try
        {
            // Print SDK version
            Console.WriteLine($"PI AF SDK Version: {typeof(PIServer).Assembly.GetName().Version}");

            string serverName = "MyPIServer";
            Console.WriteLine($"Connecting to PI Data Archive: {serverName}");

            PIServers servers = new PIServers();
            if (servers == null)
            {
                throw new Exception("Failed to initialize PIServers.");
            }

            myPIServer = servers[serverName];
            if (myPIServer == null)
            {
                throw new Exception($"Failed to find PI Server: {serverName}");
            }

            myPIServer.Connect();
            Console.WriteLine("Connected successfully!");

            // Create a PI point for the 'TestFutureForecast' tag
            PIPoint testPoint = PIPoint.FindPIPoint(myPIServer, "sinusoid");
            if (testPoint == null)
            {
                throw new Exception("Failed to find PI Point: sinusoid");
            }

            Console.WriteLine($"Found PI Point: {testPoint.Name}");

        }
        catch (Exception ex)
        {
            Console.WriteLine($"An error occurred: {ex.Message}");
            if (ex.InnerException != null)
            {
                Console.WriteLine($"Inner exception: {ex.InnerException.Message}");
            }
        }
        finally
        {
            if (myPIServer != null && myPIServer.ConnectionInfo.IsConnected)
            {
                myPIServer.Disconnect();
                Console.WriteLine("Disconnected from the server.");
            }
        }

        Console.WriteLine("Press any key to exit...");
        Console.ReadKey();
    }
}
