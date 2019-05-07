using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SensiplexAPI.Hub
{
    public class NotificationHub : Microsoft.AspNetCore.SignalR.Hub
    {
       
        public  void RunPlate(List<string> wells)
        {
            Clients.All.SendAsync("PlateNotification", "Plate Run Intiated");
            IntiateRequestandSendNotification(wells);
        }

        private async void IntiateRequestandSendNotification(List<string> wells)
        {
            System.Threading.Thread.Sleep(1000);
            //Call Firmware
            //To Do 
            //Get and Send Notification to client for each well
            int completion = 0;
            foreach (var item in wells)
            {
                for (int i = 1; i <= 10; i++)
                {
                    System.Threading.Thread.Sleep(500);
                    completion += 10;                   
                    await Clients.All.SendAsync("WellNotification", item, completion);
                    if (i == 10)
                        completion = 0;
                }
               
            }
           
        }

    }

}
