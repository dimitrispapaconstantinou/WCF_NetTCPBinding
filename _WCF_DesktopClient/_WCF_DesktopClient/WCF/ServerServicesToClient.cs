using _Wcf_Library_dll;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;

namespace _WCF_DesktopClient.WCF
{
  public class ServerServicesToClient : DuplexClientBase<IServerMethods>, IServerMethods
  {
    public ServerServicesToClient(InstanceContext c, NetTcpBinding b, EndpointAddress e)
      : base(c, b, e)
    {
    }

    public void Connect()
    {
      Channel.Connect();
    }

    public void Disconnect()
    {
      Channel.Disconnect();
    }
  }
}
