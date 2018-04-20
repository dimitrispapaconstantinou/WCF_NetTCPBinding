using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace _Wcf_Library_dll
{
  [ServiceContract(CallbackContract = typeof(IClientMethods))]
  public interface IServerMethods
  {
    [OperationContract(IsOneWay = true)]
    void Connect();


    [OperationContract(IsOneWay = true)]
    void Disconnect();

  }
}
