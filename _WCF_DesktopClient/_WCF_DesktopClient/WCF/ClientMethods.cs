using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using _Wcf_Library_dll;
using System.Windows.Forms;

namespace _WCF_DesktopClient.WCF
{
  public class ClientMethods : IClientMethods
  {

 

    ServerServicesToClient proxy;

    public void CallBackFunction(string str)
    {
      MessageBox.Show(str);
    }


    
    public void ConnectToServer()
    {


      InstanceContext context = new InstanceContext(this);

      var binding = new NetTcpBinding(SecurityMode.None);

      string endPointAddressStr = @"net.tcp://10.0.0.70/TestNetTcp/library_service.svc";
      var endPointAddress = new EndpointAddress(endPointAddressStr);


      proxy = new ServerServicesToClient(context, binding, endPointAddress);
      proxy.Connect();

    }


  }
}
