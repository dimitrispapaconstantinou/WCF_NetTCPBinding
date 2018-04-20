using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Activation;
using System.Text;
using System.Web;

namespace _Wcf_Library_dll
{
  [AspNetCompatibilityRequirements(RequirementsMode = AspNetCompatibilityRequirementsMode.Allowed)]
  public class ServerMethods : IServerMethods
  {

    private List<IClientMethods> _callbackChannelsList = new List<IClientMethods>();
    private static readonly object _sycnRemotePrinter = new object();
    public object CachedChannelsList_temp;

    public void Connect()
    {
      using (StreamWriter sw = File.AppendText(@"c:\tmp\MessageFromWebSite.txt"))
      {
        sw.WriteLine(DateTime.Now.ToString() + " connected");
      }

      // get the channel
      IClientMethods callbackChannel = OperationContext.Current.GetCallbackChannel<IClientMethods>();

      lock (_sycnRemotePrinter)
      {
        CachedChannelsList_temp = HttpRuntime.Cache["callbackChannelsList"];
        if (CachedChannelsList_temp != null)
        {
          _callbackChannelsList = (List<IClientMethods>)CachedChannelsList_temp;
          if (!_callbackChannelsList.Contains(callbackChannel))
          {
            _callbackChannelsList.Add(callbackChannel);

            HttpRuntime.Cache.Insert("callbackChannelsList",
                                _callbackChannelsList,
                                null,
                                System.Web.Caching.Cache.NoAbsoluteExpiration,
                                System.Web.Caching.Cache.NoSlidingExpiration,
                                System.Web.Caching.CacheItemPriority.Default,
                                null);
            using (StreamWriter sw = File.AppendText(@"c:\tmp\MessageFromWebSite.txt"))
            {
              sw.WriteLine(DateTime.Now.ToString() + " " + "Cache is not null. Added  channel "
                + _callbackChannelsList.Count + ". Chanel ID :" + callbackChannel.ToString());
            }
          }
        }
        else // if there is no cached list channel. this occures on the first connection
        {
          _callbackChannelsList.Add(callbackChannel);
          HttpRuntime.Cache.Insert("callbackChannelsList",
                                _callbackChannelsList,
                                null,
                                System.Web.Caching.Cache.NoAbsoluteExpiration,
                                System.Web.Caching.Cache.NoSlidingExpiration,
                                System.Web.Caching.CacheItemPriority.Default,
                                null);
          using (StreamWriter sw = File.AppendText(@"c:\tmp\MessageFromWebSite.txt"))
          {
            sw.WriteLine(DateTime.Now.ToString() + " " + "Cache is null. Added first channel "
              + _callbackChannelsList.Count + ". Chanel ID :" + callbackChannel.ToString());
          }

        }





      }
    }



    public void Disconnect()
    {

      // get the channel
      IClientMethods callbackChannel = OperationContext.Current.GetCallbackChannel<IClientMethods>();

      try
      {
        lock (_sycnRemotePrinter)
        {
          CachedChannelsList_temp = HttpRuntime.Cache["callbackChannelsList"];
          if (CachedChannelsList_temp != null)
          {
            _callbackChannelsList = (List<IClientMethods>)CachedChannelsList_temp;

            //remove channel
            _callbackChannelsList.Remove(callbackChannel);

            // Update cache
            HttpRuntime.Cache.Insert("callbackChannelsList",
                                _callbackChannelsList,
                                null,
                                System.Web.Caching.Cache.NoAbsoluteExpiration,
                                System.Web.Caching.Cache.NoSlidingExpiration,
                                System.Web.Caching.CacheItemPriority.Default,
                                null);

            using (StreamWriter sw = File.AppendText(@"c:\tmp\MessageFromWebSite.txt"))
            {
              sw.WriteLine(DateTime.Now.ToString() + " " + "Disconnected "
                + _callbackChannelsList.Count + ". Chanel ID :" + callbackChannel.ToString());
            }

          }

        }
      }
      catch (Exception e)
      {
        // TODO: DONT LEAVE IT EMPTY
      }

    }
  }

}
