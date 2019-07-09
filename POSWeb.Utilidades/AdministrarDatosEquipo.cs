using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace POSWeb.Utilidades
{
    public static class AdministrarDatosEquipo
    {
        public static string ObtenerDireccionIP()
        {
            string direccionIp = string.Empty;

            if (NetworkInterface.GetIsNetworkAvailable())
            {
                IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
                direccionIp = host.AddressList.FirstOrDefault(i =>
                  i.AddressFamily ==
                  AddressFamily.InterNetwork).ToString();
            }

            return direccionIp;
        }

    }
}
