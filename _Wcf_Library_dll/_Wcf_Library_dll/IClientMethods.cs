using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace _Wcf_Library_dll
{
  public interface IClientMethods
  {

    [OperationContract(IsOneWay = true)]
    void CallBackFunction(string str);

  }
}
