using LinqToExcel;
using MimeKit;
using Quartz;
using System;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;

namespace RatesMarkettingMailAutomation
{
    public class RatesMarkettingJob: IJob
    {
       public void Execute(IJobExecutionContext context)
        {
            Console.WriteLine("Checking for uploaded rates excel sheet...");
            string dirPath = "C:\\Users\\abutala.FBTESTNET\\Rates";
            try
            {
                var files = Directory.GetFiles(dirPath);
                if (files.Length > 0)
                {
                    Console.WriteLine("Found " + files.Length + " files");
                    var file = files[0];
                    Console.WriteLine("Reading " + file + "...");
                    var sData = new ExcelQueryFactory(file);
                    //var connectionStr = ConfigurationManager.ConnectionStrings["FB_MarkettingDB"].ToString();
                    //SqlConnection con = new SqlConnection(connectionStr);
                    //SqlCommand sqlCommand = new SqlCommand("Select * from User_Subscription");
                    var userSubscribedProductRatesMapping = (from x in sData.Worksheet() select x);
                    foreach (var row in userSubscribedProductRatesMapping)
                    {
                        var message = new EmailModel ();
                        message.From.Add(new MailboxAddress("Aashna", "Aashna.Butala@gmail.com"));
                        message.To.Add(new MailboxAddress(row[0].ToString(), row[1]));
                        message.Subject = "How you doin?";
                        var builder = new BodyBuilder();
                        builder.TextBody = "Hello this is aashna . this is a test message from Fremont bank";
                        message.Body = builder.ToMessageBody();
                        EmailHelper.SendEmail(message);
                    }
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
            Console.ReadLine();
        }
  
    }
}
